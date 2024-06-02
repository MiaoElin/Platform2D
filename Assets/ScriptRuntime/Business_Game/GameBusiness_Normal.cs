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
        var owner = RoleDomain.Spawn(ctx, 100, new Vector2(0, 15f), Ally.Player);
        owner.isOwner = true;
        ctx.ownerID = owner.id;
        owner.fsm.EnterNormal();

        // player
        ctx.player.coinCount = 500;

        // Camera
        ctx.camera.SetFollow(owner.transform);
        ctx.camera.SetLookAt(owner.transform);

        // UI
        UIDomain.Panel_PlayerStatus_Open(ctx);
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

    }

    static void PreTick(GameContext ctx, float dt) {
        var owner = ctx.GetOwner();
        owner.isJumpKeyDown = ctx.input.isJumpKeyDown;
    }
    static void FixedTick(GameContext ctx, float dt) {
        var owner = ctx.GetOwner();
        ctx.backScene.SetPos(owner.Pos());
        ctx.propRepo.Foreach(prop => {
            PropFsmController.ApplyFsm(ctx, prop);
        });
        ctx.lootRepo.Foreach(loot => {
            LootFSMController.ApplyFsm(ctx, loot, dt);
        });
        RoleFSMConTroller.ApplyFsm(ctx, owner, dt);

        Physics2D.Simulate(dt);
        RoleDomain.CheckGround(ctx, owner);
    }
    static void LateTick(GameContext ctx, float dt) {
        throw new NotImplementedException();
    }
}