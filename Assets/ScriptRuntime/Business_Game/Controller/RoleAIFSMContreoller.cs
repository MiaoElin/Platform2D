using System;
using UnityEngine;

public static class RoleAIFSMController {

    public static void ApplyFSM(GameContext ctx, RoleEntity role, float dt) {
        var status = role.fsm.status;
        if (status == RoleStatus.Normal) {
            ApllyNormal(ctx, role, dt);
        }
    }

    private static void ApllyNormal(GameContext ctx, RoleEntity role, float dt) {
        var fsm = role.fsm;
        if (fsm.isEnterNormal) {
            fsm.isEnterNormal = false;
        }
        RoleDomain.AI_Move(ctx, role);
    }
}