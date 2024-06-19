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
        var owner = RoleDomain.Spawn(ctx, 10, new Vector2(0, 15f), Vector3.zero, Ally.Player, null);
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

        ctx.game.fsm.EnterNormal();
        SFXDomain.BGM_Play(ctx);
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

        if (owner.hp <= 0) {
            // todo 游戏失败
            Time.timeScale = 0;
            UIDomain.Panel_Result_Open(ctx);
            UIDomain.Panel_SkillSlot_Hide(ctx);
            UIDomain.Panel_PlayerStatus_Hide(ctx);
        }

        MapDomain.WaveTick(ctx, 1, dt);

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

        // Owner Logic
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

        // Bullet TearDown
        int bulletLen = ctx.bulletRepo.TakeAll(out var allBullet);
        for (int i = 0; i < bulletLen; i++) {
            var bullet = allBullet[i];
            if (bullet.isTearDown) {
                BulletDomain.UnSpawn(ctx, bullet);
            }
        }

        Physics2D.Simulate(dt);

        ctx.roleRepo.Foreach(role => {
            RoleDomain.CheckGround(ctx, role);
        });

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

        UIDomain.HUD_HurtInfo_Tick(ctx, dt);

        UIDomain.HUD_AltarBar_Tick(ctx, dt);

        for (int i = 0; i < ctx.vfxs.Count; i++) {
            var vfx = ctx.vfxs[i];
            VFXDomain.Tick(ctx, vfx, dt);
        }

    }

    public static void TearDown() {
        // todo
    }
}