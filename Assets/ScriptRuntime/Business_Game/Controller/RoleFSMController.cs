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
        } else if (status == RoleStatus.Flash) {
            ApplyFlash(ctx, role, dt);
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
        RoleDomain.Onwer_Move_ByAxiX(ctx, role);
        RoleDomain.Jump(ctx, role);
        RoleDomain.Falling(role, dt);

        // 切换状态
        var skillCom = role.skillCom;
        var waitToCastKeys = ctx.input.waitToCastSkills;
        var usableSkillKeys = skillCom.usableSkillKeys;
        usableSkillKeys.Clear();
        foreach (var key in waitToCastKeys) {
            skillCom.TryGet(key, out var skill);
            if (skill.cd <= 0) {
                usableSkillKeys.Add(key);
            }
        }

        if (usableSkillKeys.Count > 0) {
            if (usableSkillKeys.Contains(InputKeyEnum.Skill4)) {
                skillCom.TryGet(InputKeyEnum.Skill4, out var skill);
                skill.cd = skill.cdMax;
                role.fsm.EnterFlash();
                return;
            }

            if (usableSkillKeys.Contains(InputKeyEnum.SKill3)) {
                skillCom.SetCurrentKey(InputKeyEnum.SKill3);
            } else if (usableSkillKeys.Contains(InputKeyEnum.SKill2)) {
                skillCom.SetCurrentKey(InputKeyEnum.SKill2);
            } else if (usableSkillKeys.Contains(InputKeyEnum.SKill1)) {
                skillCom.SetCurrentKey(InputKeyEnum.SKill1);
            }

            role.fsm.EnterCasting();
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

        // 切换状态
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
                skillCom.TryGet(InputKeyEnum.Skill4, out var skill);
                skill.cd = skill.cdMax;
                role.fsm.EnterFlash();
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
            RaycastHit2D ray = Physics2D.Raycast(role.GetAbdomen(), role.GetForWard(), 5f, layerMask);
            if (ray) {
                role.SetPos(ray.point - role.GetAbdomen() + role.Pos());
            } else {
                role.SetPos(role.Pos() + role.GetForWard() * 5f);
            }

        }
        fsm.EnterNormal();
    }

}