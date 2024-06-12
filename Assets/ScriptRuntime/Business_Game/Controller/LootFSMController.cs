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