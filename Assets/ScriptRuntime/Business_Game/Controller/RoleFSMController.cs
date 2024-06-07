using System;
using UnityEngine;

public static class RoleFSMConTroller {

    public static void ApplyFsm(GameContext ctx, RoleEntity role, float dt) {
        var status = role.fsm.status;
        ApplyAny(ctx, role, dt);
        if (status == RoleStatus.Normal) {
            ApplyNormal(ctx, role, dt);
        } else if (status == RoleStatus.Ladder) {
            ApplyLadder(ctx, role, dt);
        } else if (status == RoleStatus.Casting) {
            ApplyCasting(ctx, role, dt);
        } else if (status == RoleStatus.Trampoline) {
            ApplyTrampoline(ctx, role, dt);
        }

    }

    private static void ApplyAny(GameContext ctx, RoleEntity role, float dt) {
        var status = role.fsm.status;
        if (status != RoleStatus.Ladder && status != RoleStatus.Trampoline) {
            RoleDomain.CurrentSkill_Tick(ctx, role);
        }
        RoleDomain.CD_Tick(ctx, role, dt);
        RoleDomain.Owner_Buff_Tick(ctx, dt);
        RoleDomain.Owner_Rehp_Tick(role, dt);
    }

    private static void ApplyNormal(GameContext ctx, RoleEntity role, float dt) {
        var fsm = role.fsm;
        if (fsm.isEnterNormal) {
            fsm.isEnterNormal = false;
        }
        RoleDomain.Onwer_Move_ByAxiX(ctx, role);
        RoleDomain.Jump(ctx, role);
        RoleDomain.Falling(role, dt);

    }

    private static void ApplyLadder(GameContext ctx, RoleEntity role, float dt) {
        var fsm = role.fsm;
        if (fsm.isEnterLadder) {
            fsm.isEnterLadder = false;
            ctx.GetCurrentMap().SetGridTrigger();
        }

        RoleDomain.Move_InLadder(ctx, role);
        if (role.Pos().y <= fsm.lowestY || role.Pos().y > fsm.highestY) {
            fsm.EnterNormal();
            ctx.GetCurrentMap().SetGridCollision();
        }

        if (ctx.input.isJumpKeyDown && !role.isStayInGround) {
            ctx.input.isJumpKeyDown = false;
            fsm.EnterNormal();
            ctx.GetCurrentMap().SetGridCollision();
        }
    }

    private static void ApplyTrampoline(GameContext ctx, RoleEntity role, float dt) {
        var fsm = role.fsm;
        if (fsm.isEnterLadder) {
            fsm.isEnterTrampoline = false;
        }
        RoleDomain.Onwer_Move_ByAxiX(ctx, role);
        RoleDomain.Falling(role, dt);
        if (role.isOnGround) {
            fsm.EnterNormal();
        }
    }

    private static void ApplyCasting(GameContext ctx, RoleEntity role, float dt) {
        var fsm = role.fsm;
        if (fsm.isEnterCasting) {
            fsm.isEnterCasting = false;
        }
        RoleDomain.Casting(ctx, role, dt);

        RoleDomain.Owner_Move_InCasting(ctx, role);
        RoleDomain.Jump(ctx, role);
        RoleDomain.Falling(role, dt);
    }


}