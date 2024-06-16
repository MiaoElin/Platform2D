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
        } else if (status == RoleStatus.Ladder) {
            ApplyLadder(ctx, role);
        }
    }

    private static void ApplyAny(GameContext ctx, RoleEntity role, float dt) {
        RoleDomain.AI_Monster_SerchRange_Tick(ctx, role);
        if (role.aiType == AIType.Elite) {
            RoleDomain.EnterLadder(ctx, role);
        }
    }

    private static void ApllyNormal(GameContext ctx, RoleEntity role, float dt) {
        var fsm = role.fsm;
        if (fsm.isEnterNormal) {
            fsm.isEnterNormal = false;
        }
        RoleDomain.AI_Move(ctx, role, dt);
        if (role.aiType == AIType.Elite) {
            RoleDomain.Jump(ctx, role);
            RoleDomain.Falling(role, dt);
        }
        // Exit
        bool isInAttackRange = RoleDomain.AI_EnterAttakRange_Tick(ctx, role);
        if (isInAttackRange) {
            role.fsm.EnterCasting();
        }
    }

    private static void ApplyCasting(GameContext ctx, RoleEntity role, float dt) {
        var fsm = role.fsm;
        if (fsm.isEnterCasting) {
            fsm.isEnterCasting = false;
        }

        if (role.aiType == AIType.Robot) {
            RoleDomain.AI_Move(ctx, role, dt);
        } else if (role.aiType == AIType.Elite) {
            RoleDomain.AI_Move_Stop(ctx, role);
            RoleDomain.Jump(ctx, role);
            RoleDomain.Falling(role, dt);
        } else {
            RoleDomain.AI_Move_Stop(ctx, role);
        }
        RoleDomain.Casting(ctx, role, dt);
        // Exit
        bool isInAttackRange = RoleDomain.AI_EnterAttakRange_Tick(ctx, role);
        if (!isInAttackRange) {
            role.fsm.EnterNormal();
        }
    }

    private static void ApplyLadder(GameContext ctx, RoleEntity role) {
        var fsm = role.fsm;
        if (fsm.isEnterLadder) {
            fsm.isEnterLadder = false;
            // ctx.GetCurrentMap().SetGridTrigger();
        }
        var dir = ctx.GetOwner().GetHead_Front() - role.Pos();
        if (dir.y > 0) {
            dir = Vector2.up;
        } else if (dir.y < 0) {
            dir = Vector2.down;
        }

        RoleDomain.Move_ByAxisY(ctx, role, dir.y);
        Debug.Log(role.Pos().y + " " + fsm.lowestY);
        // Exit
        if (role.Pos().y <= fsm.lowestY || role.Pos().y > fsm.highestY) {
            fsm.EnterNormal();
            // ctx.GetCurrentMap().SetGridCollision();
        }

        // if (ctx.input.isJumpKeyDown && !role.isStayInGround) {
        //     ctx.input.isJumpKeyDown = false;
        //     fsm.EnterNormal();
        //     // ctx.GetCurrentMap().SetGridCollision();
        // }
        // Exit
        bool isInAttackRange = RoleDomain.AI_EnterAttakRange_Tick(ctx, role);
        if (isInAttackRange) {
            role.fsm.EnterCasting();
        }
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