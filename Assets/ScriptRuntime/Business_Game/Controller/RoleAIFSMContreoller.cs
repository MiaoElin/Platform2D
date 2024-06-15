using System;
using UnityEngine;

public static class RoleAIFSMController {

    public static void ApplyFSM(GameContext ctx, RoleEntity role, float dt) {
        var status = role.fsm.status;
        ApplyAny(ctx, role, dt);
        if (status == RoleStatus.Normal) {
            ApllyNormal(ctx, role, dt);
        } else if (status == RoleStatus.Casting) {
            ApplyCasting(ctx, role, dt);
        } else if (status == RoleStatus.Destroy) {
            ApllyDestroy(ctx, role);
        }
    }

    private static void ApplyAny(GameContext ctx, RoleEntity role, float dt) {
        RoleDomain.AI_Monster_SerchRange_Tick(ctx, role);
        RoleDomain.AI_EnterAttakRange_Tick(ctx, role);
        // RoleDomain.AI_MeetTOwner_Check(ctx, role);
    }

    private static void ApllyNormal(GameContext ctx, RoleEntity role, float dt) {
        var fsm = role.fsm;
        if (fsm.isEnterNormal) {
            fsm.isEnterNormal = false;
        }
        RoleDomain.AI_Move(ctx, role, dt);
    }

    private static void ApplyCasting(GameContext ctx, RoleEntity role, float dt) {
        var fsm = role.fsm;
        if (fsm.isEnterCasting) {
            fsm.isEnterCasting = false;
        }

        if (role.aiType == AIType.Robot) {
            RoleDomain.AI_Move(ctx, role, dt);
        } else {
            RoleDomain.Move_Stop(role);
        }
        RoleDomain.Casting(ctx, role, dt);
    }

    private static void ApllyDestroy(GameContext ctx, RoleEntity role) {
        var fsm = role.fsm;
        if (fsm.isEnterDestroy) {
            fsm.isEnterDestroy = false;
        }
        // 掉落金币
        for (int i = 0; i < 5; i++) {
            LootDomain.SpawnCoin(ctx, 120, role.Pos());
        }

        // 掉落物品

        // 销毁UI
        UIDomain.HUD_HPBar_Close(ctx, role.id);
        // 销毁角色
        RoleDomain.Unspawn(ctx, role);

    }
}