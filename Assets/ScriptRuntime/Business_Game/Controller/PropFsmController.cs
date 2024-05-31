using System;
using UnityEngine;

public static class PropFsmController {

    public static void ApplyFsm(GameContext ctx, PropEntity prop) {
        var fsm = prop.fsm;
        var status = fsm.propStatus;
        if (status == PropStatus.Normal) {
            ApplyNormal(ctx, prop);
        }
    }

    private static void ApplyNormal(GameContext ctx, PropEntity prop) {
        var fsm = prop.fsm;
        if (fsm.isEnterNormal) {
            fsm.isEnterNormal = false;
        }
        // 反射板
        if (prop.isTrampoline) {
            if (prop.isOwnerOnTrampoline) {
                prop.isOwnerOnTrampoline = false;
                ctx.GetOwner().SetVelocityY(prop.jumpForce);
            }
        }

        if (prop.isLadder) {
            if (prop.isOwnerOnLadder) {
                prop.isOwnerOnLadder = false;
            }
        }

    }
}