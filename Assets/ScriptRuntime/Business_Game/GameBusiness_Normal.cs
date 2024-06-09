using System;
using UnityEngine;

public static class GameBusiness_Normal {

    public static void EnterStage(GameContext ctx) {

        // Map
        MapDomain.Spawn(ctx, 1);
        ctx.currentStageID = 1;

        // BackScene
        var backScene = GameFactory.BackScene_Create(ctx);
        ctx.backScene = backScene;

        // Owner
        var owner = RoleDomain.Spawn(ctx, 10, new Vector2(0, 15f), Ally.Player, null);
        owner.isOwner = true;
        ctx.ownerID = owner.id;

        // player
        ctx.player.coinCount = 500;

        // Camera
        ctx.camera.SetFollow(owner.transform);
        ctx.camera.SetLookAt(owner.transform);

        // UI
        UIDomain.Panel_PlayerStatus_Open(ctx);
        UIDomain.Panel_SkillSlot_Open(ctx);
    }

    public static void Tick(GameContext ctx, float dt) {
        PreTick(ctx, dt);

        ref var resetTime = ref ctx.resetTime;
        const float Interval = 0.01f;
        resetTime += dt;
        if (resetTime < Interval) {
            FixedTick(ctx, resetTime);
            resetTime = 0;
        } else {
            while (resetTime >= Interval) {
                FixedTick(ctx, Interval);
                resetTime -= Interval;
            }
        }

        LateTick(ctx, dt);
    }

    static void PreTick(GameContext ctx, float dt) {
        var owner = ctx.GetOwner();
        owner.isJumpKeyDown = ctx.input.isJumpKeyDown;
    }
    static void FixedTick(GameContext ctx, float dt) {
        var owner = ctx.GetOwner();
        ctx.backScene.SetPos(owner.Pos());

        // role logic
        ctx.roleRepo.Foreach(role => {
            if (role.aiType != AIType.None) {
                RoleAIFSMController.ApplyFSM(ctx, role, dt);
            }
        });

        RoleFSMConTroller.ApplyFsm(ctx, owner, dt);

        // Prop Logic
        ctx.propRepo.Foreach(prop => {
            PropFsmController.ApplyFsm(ctx, prop, dt);
        });

        // Loot Logic
        ctx.lootRepo.Foreach(loot => {
            LootFSMController.ApplyFsm(ctx, loot, dt);
        });

        // Bullet Logic
        ctx.bulletRepo.Foreach(bullet => {
            BulletDomain.Move(ctx, bullet, dt);
        });

        // Role TearDown
        int roleLen = ctx.roleRepo.TakeAll(out var allRole);
        for (int i = 0; i < roleLen; i++) {
            var role = allRole[i];
            if (role.isDead) {
                UIDomain.HUD_HPBar_Close(ctx, role.id);
                RoleDomain.Unspawn(ctx, role);
            }
        }

        // Loot TearDown
        int lootLen = ctx.lootRepo.TakeAll(out var allLoot);
        for (int i = 0; i < lootLen; i++) {
            var loot = allLoot[i];
            if (loot.isDead) {
                LootDomain.UnSpawn(ctx, loot);
            }
        }

        // Bullet TearDown
        int bulletLen = ctx.bulletRepo.TakeAll(out var allBullet);
        for (int i = 0; i < bulletLen; i++) {
            var bullet = allBullet[i];
            if (bullet.isTearDown) {
                BulletDomain.UnSpawn(ctx, bullet);
            }
        }

        // Prop TearDown
        int propLen = ctx.propRepo.TakeAll(out var allProp);
        for (int i = 0; i < propLen; i++) {
            var prop = allProp[i];
            if (prop.isTearDown) {
                PropDomain.UnSpawn(ctx, prop);
            }
        }

        Physics2D.Simulate(dt);
        RoleDomain.CheckGround(ctx, owner);
        ctx.bulletRepo.Foreach(bullet => {
            BulletDomain.HitCheck(ctx, bullet);
        });

    }

    static void LateTick(GameContext ctx, float dt) {

        UIDomain.Panel_PlayerStatus_Update(ctx);
        UIDomain.Panel_SkillSlot_CD_Tick(ctx);

        ctx.roleRepo.Foreach(role => {
            UIDomain.HUD_HPBar_UpdateTick(ctx, role);
        });

        var owner = ctx.GetOwner();
        if (owner.hp < owner.lastHp) {
            // UIDomain.HUD_HurtInfo_Open(ctx, owner.Pos() + Vector2.up * 2, owner.lastHp - owner.hp);
            owner.lastHp = owner.hp;
        }

        UIDomain.HUD_HurtInfo_Foreach(ctx, hud => {
            if (!hud.isTearDown) {
                hud.Easing(dt);
            }
        });

        UIDomain.HUD_HurtInfo_Foreach(ctx, hud => {
            if (hud.isTearDown) {
                UIDomain.HUD_HurtInfo_Close(ctx, hud);
            }
        });


    }
}