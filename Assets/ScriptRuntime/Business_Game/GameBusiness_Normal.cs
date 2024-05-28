using System;
using UnityEngine;

public static class GameBusiness_Normal {

    public static void EnterStage(GameContext ctx) {
        MapDomain.Spawn(ctx, 1);

        var role = RoleDomain.Spawn(ctx, 100, new Vector2(0, 1.5f), Ally.Player);
        ctx.ownerID = role.id;
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
        RoleDomain.Move(ctx, owner);
        RoleDomain.Jump(ctx, owner);

        Physics.Simulate(dt);
    }
    static void LateTick(GameContext ctx, float dt) {
        throw new NotImplementedException();
    }
}