using System;
using UnityEngine;

public static class RoleFSMConTroller {

    public static void ApplyFsm(GameContext ctx, RoleEntity role, float dt) {
        var status = role.fsm.status;
        ApplyAny(ctx, role, dt);
        Debug.Log(status);
        if (status == RoleStatus.Normal) {
            ApplyNormal(ctx, role, dt);
        } else if (status == RoleStatus.Ladder) {
            ApplyLadder(ctx, role, dt);
        } else if (status == RoleStatus.Casting) {
            ApplyCasting(ctx, role, dt);
        } else if (status == RoleStatus.Trampoline) {
            ApplyTrampoline(ctx, role, dt);
        } else if (status == RoleStatus.Flash) {
            ApplyFlash(ctx, role, dt);
        } else if (status == RoleStatus.Destroy) {
            ApllyDestroy(ctx, role);
        }

    }

    private static void ApplyAny(GameContext ctx, RoleEntity role, float dt) {
        RoleDomain.CD_Tick(ctx, role, dt);
        RoleDomain.Owner_Buff_Tick(ctx, dt);
        RoleDomain.Owner_Rehp_Tick(role, dt);
    }

    private static void ApplyNormal(GameContext ctx, RoleEntity role, float dt) {
        var fsm = role.fsm;
        if (fsm.isEnterNormal) {
            fsm.isEnterNormal = false;
            ctx.GetOwner().anim.CrossFade("Idle", 0);
        }
        // Execute
        RoleDomain.Onwer_Move_ByAxiX(ctx, role);
        RoleDomain.Jump(ctx, role);
        RoleDomain.Falling(role, dt);
        RoleDomain.CurrentSkill_Tick(ctx, role);

        // Exit
        var skillCom = role.skillCom;
        var usableSkillKeys = skillCom.usableSkillKeys;
        var currentSkillKey = skillCom.GetCurrentKey();
        if (usableSkillKeys.Count > 0) {
            if (currentSkillKey == InputKeyEnum.Skill4) {
                // 重置cd
                skillCom.TryGet(InputKeyEnum.Skill4, out var skill);
                skill.cd = skill.cdMax;
                // 进入flash状态
                role.fsm.EnterFlash();
                // 将当前技能使用
                skillCom.SetCurrentKey(InputKeyEnum.None);
            } else {
                role.fsm.EnterCasting();
            }
        }

    }

    private static void ApplyLadder(GameContext ctx, RoleEntity role, float dt) {
        var fsm = role.fsm;
        if (fsm.isEnterLadder) {
            fsm.isEnterLadder = false;
            // 将当前技能清空，防止按住技能键爬楼梯，落地后进不去casting状态
            role.skillCom.SetCurrentKey(InputKeyEnum.None);
            ctx.GetCurrentMap().SetGridTrigger();
        }

        RoleDomain.Move_InLadder(ctx, role);

        // Exit
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
        if (fsm.isEnterTrampoline) {
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

        // Enter
        if (fsm.isEnterCasting) {
            fsm.isEnterCasting = false;

        }

        // Execute
        RoleDomain.Casting(ctx, role, dt);
        RoleDomain.Owner_Move_InCasting(ctx, role);
        RoleDomain.Jump(ctx, role);
        RoleDomain.Falling(role, dt);
        RoleDomain.CurrentSkill_Tick(ctx, role);
        // Exit
        var usableSkillKeys = role.skillCom.usableSkillKeys;
        var skillCom = role.skillCom;
        if (usableSkillKeys.Count == 0) {
            skillCom.SetCurrentKey(InputKeyEnum.None);
            role.fsm.EnterNormal();
            return;
        } else {
            if (skillCom.GetCurrentKey() == InputKeyEnum.Skill4) {
                // 重置cd
                skillCom.TryGet(InputKeyEnum.Skill4, out var skill);
                skill.cd = skill.cdMax;
                // 进入flash状态
                role.fsm.EnterFlash();
                // 将当前技能使用
                skillCom.SetCurrentKey(InputKeyEnum.None);
            }
        }
    }

    private static void ApplyFlash(GameContext ctx, RoleEntity role, float dt) {
        var fsm = role.fsm;
        if (fsm.isEnterFlash) {
            fsm.isEnterFlash = false;
            // vfx
            ctx.asset.TryGet_RoleTM(role.typeID, out var tm);
            VFXDomain.Play(ctx, role.Pos(), tm.vfx_Flash);
            // move
            LayerMask layerMask = 1 << 3;
            RaycastHit2D ray = Physics2D.Raycast(role.GetHead_Front(), role.GetForWard(), 5f, layerMask);
            if (ray) {
                // 有障碍物，移动的最远距离：肚子贴着障碍物
                role.SetPos(ray.point - (role.GetHead_Front() - role.Pos()));
            } else {
                // 闪现
                role.SetPos(role.Pos() + role.GetForWard() * 5f);
            }
        }
        fsm.EnterNormal();
    }

    private static void ApllyDestroy(GameContext ctx, RoleEntity role) {
        var fsm = role.fsm;
        if (fsm.isEnterDestroy) {
            fsm.isEnterDestroy = false;
        }
        // 游戏失败
    }
}