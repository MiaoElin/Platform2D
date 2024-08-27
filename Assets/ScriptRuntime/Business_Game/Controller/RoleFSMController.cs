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
        } else if (status == RoleStatus.Destroy) {
            ApllyDestroy(ctx, role);
        } else if (status == RoleStatus.Suffering) {
            ApllySuffering(ctx, role, dt);
        }

    }

    private static void ApplyAny(GameContext ctx, RoleEntity role, float dt) {
        RoleDomain.CD_Tick(ctx, role, dt);
        RoleDomain.Owner_Buff_Tick(ctx, dt);
        RoleDomain.Owner_Rehp_Tick(role, dt);
        RoleDomain.EnterLadder(ctx, role);
    }

    private static void ApplyNormal(GameContext ctx, RoleEntity role, float dt) {
        var fsm = role.fsm;
        if (fsm.isEnterNormal) {
            fsm.isEnterNormal = false;
            role.Anim_Idle();
        }
        // Execute
        RoleDomain.Onwer_Move_ByAxiX(ctx, role);
        RoleDomain.Jump(ctx, role);
        RoleDomain.Falling(role, dt);
        RoleDomain.Owner_CurrentSkill_Tick(ctx, role);

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
                // sfx 
                SFXDomain.Onwer_SKill_Play(ctx, skill.castSfx, skill.volume);
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
            role.ColliderEnAble(false);
        }

        RoleDomain.Move_ByAxisY(ctx, role, ctx.input.moveAxis.y);
        // Exit
        if (role.Pos().y <= fsm.lowestY || role.Pos().y > fsm.highestY) {
            fsm.EnterNormal();
            role.ColliderEnAble(true);
        }

        if (ctx.input.isJumpKeyDown && !role.isStayInGround) {
            Debug.Log("IN");
            ctx.input.isJumpKeyDown = false;
            fsm.EnterNormal();
            role.ColliderEnAble(true);
        }
    }

    private static void ApplyTrampoline(GameContext ctx, RoleEntity role, float dt) {
        var fsm = role.fsm;
        if (fsm.isEnterTrampoline) {
            fsm.isEnterTrampoline = false;
            role.isOnGround = false;
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
        RoleDomain.Owner_CurrentSkill_Tick(ctx, role);
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
                // sfx 
                SFXDomain.Onwer_SKill_Play(ctx, skill.castSfx, skill.volume);
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
            RaycastHit2D ray = Physics2D.Raycast(role.GetBelly(), role.GetForWard(), 5f, layerMask);
            if (ray) {
                // 有障碍物，移动的最远距离：肚子贴着障碍物
                role.SetPos(ray.point - (role.GetBelly() - role.Pos()));
            } else {
                // 闪现
                role.SetPos(role.Pos() + role.GetForWard() * 5f);
            }
        }
        fsm.EnterNormal();
    }


    private static void ApllySuffering(GameContext ctx, RoleEntity role, float dt) {
        var fsm = role.fsm;
        if (fsm.isEnterSuffering) {
            fsm.isEnterSuffering = false;
            RoleDomain.Owner_Move_Stop(ctx);
            // 可下落
            RoleDomain.Falling(role, dt);
        }
        ref var timer = ref fsm.sufferingTimer;
        if (timer <= 0) {
            fsm.EnterNormal();
            // Owner受伤会进入Suffering，Suffering结束会回到normal状态，这时候Collider可能是Trigger状态 后面可改成回到上次的status
            // 或者改成ladder上受到攻击回到normal（这时候将owner的Collider打开）角色会falling 到地面
        } else {
            timer -= dt;
        }
    }

    private static void ApllyDestroy(GameContext ctx, RoleEntity role) {
        var fsm = role.fsm;
        if (fsm.isEnterDestroy) {
            fsm.isEnterDestroy = false;
        }
        // 游戏失败
    }
}