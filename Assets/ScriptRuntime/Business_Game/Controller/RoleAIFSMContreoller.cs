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
        }
    }

    private static void ApplyAny(GameContext ctx, RoleEntity role, float dt) {
        RoleDomain.AI_EnterAttakRange_Tick(ctx, role);
        RoleDomain.AI_MeetTOwner_Check(ctx, role);
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
        RoleDomain.AI_Move(ctx, role, dt);
        RoleDomain.Casting(ctx, role, dt);
    }
}