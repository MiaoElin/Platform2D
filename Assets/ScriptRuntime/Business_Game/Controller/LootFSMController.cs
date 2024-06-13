using System;
using UnityEngine;

public static class LootFSMController {

    public static void ApplyFsm(GameContext ctx, LootEntity loot, float dt) {
        var status = loot.fsm.status;
        if (status == LootStatus.EasingIn) {
            ApplyEasingIN(ctx, loot, dt);
        } else if (status == LootStatus.Normal) {
            ApplyNormal(ctx, loot, dt);
        } else if (status == LootStatus.Used) {
            ApplyUsed(ctx, loot, dt);
        } else if (status == LootStatus.Destroy) {
            ApplyDestroy(ctx, loot);
        } else if (status == LootStatus.MovetoOwner) {
            ApplyMoveToOwner(ctx, loot, dt);
        }
    }

    private static void ApplyMoveToOwner(GameContext ctx, LootEntity loot, float dt) {
        var fsm = loot.fsm;
        ref var timer = ref loot.coinFlyTimer;
        if (fsm.isEnterMoveToOwner) {
            fsm.isEnterMoveToOwner = false;
            timer = UnityEngine.Random.Range(0.8f, 1.5f);
        }
        if (timer <= 0) {
            timer = 0;
            LootDomain.Move(ctx, loot);
        } else {
            timer -= dt;
        }
    }

    private static void ApplyEasingIN(GameContext ctx, LootEntity loot, float dt) {
        var fsm = loot.fsm;
        if (fsm.isEnterEasingIn) {
            fsm.isEnterEasingIn = false;
        }
        loot.EasingIN(dt);
        if (!fsm.isEasingIn) {
            fsm.EnterNormal();
        }
    }

    private static void ApplyNormal(GameContext ctx, LootEntity loot, float dt) {
        var fsm = loot.fsm;
        if (fsm.isEnterNormal) {
            fsm.isEnterNormal = false;
        }

        if (loot.isCoin) {
            loot.Falling(dt);
            LootDomain.CheckGround(ctx, loot);
        }

    }

    private static void ApplyUsed(GameContext ctx, LootEntity loot, float dt) {
        var fsm = loot.fsm;
        if (fsm.isEnterUsed) {
            fsm.isEnterUsed = false;
            // Todo 播放used的动画
            loot.Anim_Used();
        }
    }

    private static void ApplyDestroy(GameContext ctx, LootEntity loot) {
        var fsm = loot.fsm;
        if (fsm.isEnterDestroy) {
            fsm.isEnterDestroy = false;
            return;
        }
        // LootDomain.UnSpawn(ctx, loot);

    }

}