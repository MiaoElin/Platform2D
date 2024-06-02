using System;
using UnityEngine;

public static class LootFSMController {

    public static void ApplyFsm(GameContext ctx, LootEntity loot, float dt) {
        var status = loot.fsm.status;
        if (status == LootStatus.Normal) {
            ApplyNormal(ctx, loot, dt);
        } else if (status == LootStatus.Used) {
            ApplyUsed(ctx, loot, dt);
        }
    }

    private static void ApplyNormal(GameContext ctx, LootEntity loot, float dt) {
        var fsm = loot.fsm;
        if (fsm.isEnterNormal) {
            fsm.isEnterNormal = false;
        }
        ctx.lootRepo.Foreach(loot => {
            // 遍历，判断是否在角色范围内
            // 如果在范围内打开UI 触发loot页
        });
    }

    private static void ApplyUsed(GameContext ctx, LootEntity loot, float dt) {
        var fsm = loot.fsm;
        if (fsm.isEnterUsed) {
            fsm.isEnterUsed = false;
            // Todo 播放used的动画
            loot.Anim_Used();
        }
    }
}