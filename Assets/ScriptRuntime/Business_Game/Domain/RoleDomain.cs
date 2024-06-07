using System;
using UnityEngine;
using System.Collections.Generic;

public static class RoleDomain {

    public static RoleEntity Spawn(GameContext ctx, int typeID, Vector2 pos, Ally ally, Vector2[] path) {
        var role = GameFactory.Role_Spawn(ctx, typeID, pos, ally, path);
        ctx.roleRepo.Add(role);
        role.OnTriggerEnterHandle = (Collider2D other) => {
            On_Owner_TriggerEnterEvent(ctx, other);
        };

        role.OnTriggerStayHandle = (Collider2D other) => {
            On_Owner_TriggerStayEvent(ctx, other);
        };

        role.OnTriggerExitHandle = (Collider2D other) => {
            On_Owner_TriggerExitEvent(ctx, other);
        };
        role.fsm.EnterNormal();
        return role;
    }

    internal static void AI_MeetTOwner_Check(GameContext ctx, RoleEntity role) {
        // Vector2.SqrMagnitude(role.)
    }

    internal static void AI_EnterAttakRange_Tick(GameContext ctx, RoleEntity role) {
        var target = ctx.GetOwner().Pos();
        bool isInRange = PureFunction.IsInRange(target, role.Pos(), role.attackRange);
        if (isInRange) {
            role.fsm.EnterCasting();
        } else {
            role.fsm.EnterNormal();
        }
    }

    internal static void AI_SetCurrentSkill(GameContext ctx, RoleEntity role) {
        var skillCom = role.skillCom;
        skillCom.SetCurrentKey(InputKeyEnum.SKill1);
    }

    public static void Unspawn(GameContext ctx, RoleEntity role) {
        ctx.roleRepo.Remove(role);
        role.Reuse();
        role.gameObject.SetActive(false);
        ctx.poolService.ReturnRole(role);
    }

    public static void Owner_Rehp_Tick(RoleEntity owner, float dt) {
        ref var timer = ref owner.regenerationTimer;
        timer -= dt;
        if (timer <= 0) {
            timer = owner.regenerationDuration;
            owner.hp += owner.regenerationHpMax;
            if (owner.hp > owner.hpMax) {
                owner.hp = owner.hpMax;
            }
        }
    }

    private static void On_Owner_TriggerExitEvent(GameContext ctx, Collider2D other) {
        if (other.tag == "Loot") {
            var loot = other.GetComponentInParent<LootEntity>();
            if (loot.fsm.status == LootStatus.Normal && loot.needHints) {
                UIDomain.HUD_Hints_Hide(ctx, loot.id);
            }
        }
    }

    private static void On_Owner_TriggerEnterEvent(GameContext ctx, Collider2D other) {
        var owner = ctx.GetOwner();
        if (other.tag == "Loot") {
            var loot = other.GetComponentInParent<LootEntity>();
            if (loot.fsm.status == LootStatus.Normal && loot.needHints) {
                var pos = loot.Pos() + Vector2.up * 3;
                UIDomain.HUD_Hints_ShowHIntIcon(ctx, loot.id);
            }
        }
    }

    private static void On_Owner_TriggerStayEvent(GameContext ctx, Collider2D other) {
        On_Owner_Trigger_LootEvent(ctx, other);
    }

    public static void On_Owner_Trigger_LootEvent(GameContext ctx, Collider2D other) {
        var owner = ctx.GetOwner();
        if (other.tag != "Loot") {
            return;
        }
        var loot = other.GetComponentInParent<LootEntity>();
        if (loot.fsm.status != LootStatus.Normal) {
            return;
        }
        if (ctx.input.isInteractKeyDown) {
            ctx.input.isInteractKeyDown = false;
            if (loot.needHints) {
                // 扣除loot的Price
                ctx.player.coinCount -= loot.price;
                if (loot.isDropLoot) {
                    bool has = ctx.asset.TryGetLootTMArray(out List<LootTM> allloot);
                    if (has) {
                        int index = UnityEngine.Random.Range(0, allloot.Count);
                        int typeID = allloot[index].typeID;
                        var newLoot = LootDomain.Spawn(ctx, typeID, loot.Pos() + Vector2.up * 3, Vector3.zero, Vector3.one);
                        newLoot.fsm.EnterEasingIn(loot.Pos());
                        // 关闭UI
                        UIDomain.HUD_Hints_Close(ctx, loot.id);
                        // loot进入used状态
                        loot.fsm.EnterUsed();
                    }
                } else if (loot.isGetCoin) {
                    ctx.player.coinCount += loot.coinCount;
                    loot.fsm.EnterUsed();
                    UIDomain.HUD_Hints_Close(ctx, loot.id);
                }
            }

        } else if (loot.isGetBuff) {
            var buff = BuffDomain.Spawn(ctx, loot.buffTypeId);
            owner.buffCom.Add(buff);
            loot.isDead = true;
        }
    }
    #region  Move
    public static void Onwer_Move_ByAxiX(GameContext ctx, RoleEntity role) {
        role.MoveByAxisX(ctx.input.moveAxis.x);
        role.SetForward(ctx.input.moveAxis.x);
    }

    public static void Move_InLadder(GameContext ctx, RoleEntity role) {
        role.MoveByAxisY(ctx.input.moveAxis.y);
    }

    public static void Owner_Move_InCasting(GameContext ctx, RoleEntity role) {
        role.MoveByAxisX(ctx.input.moveAxis.x);
        bool has = FindNearlyEnemy(ctx, role, out var nearlyEnemy);
        if (has) {
            var dir = nearlyEnemy.Pos() - role.Pos();
            role.SetForward(dir.x);
        }
    }

    public static void AI_Move(GameContext ctx, RoleEntity role, float dt) {
        if (role.aiType == AIType.ByPath) {
            role.MoveByPath(dt);
        } else if (role.aiType == AIType.ByTarget) {
            role.MoveByTarget(ctx.GetOwner().Pos());
        }
    }
    #endregion

    public static bool FindNearlyEnemy(GameContext ctx, RoleEntity role, out RoleEntity nearlyEnermy) {
        float nearlyDistance = Mathf.Pow(role.attackRange, 2);
        RoleEntity nearEnemy = null;
        ctx.roleRepo.Foreach(enemy => {
            if (role.ally == enemy.ally) {
                return;
            }
            float distance = Vector2.SqrMagnitude(enemy.Pos() - role.Pos());
            if (distance <= nearlyDistance) {
                nearlyDistance = distance;
                nearEnemy = enemy;
            }
        });

        if (nearEnemy == null) {
            nearlyEnermy = null;
            return false;
        } else {
            nearlyEnermy = nearEnemy;
            return true;
        }
    }

    public static void Jump(GameContext ctx, RoleEntity role) {
        role.Jump();
    }

    public static void Falling(RoleEntity role, float dt) {
        role.Falling(dt);
    }

    #region  CheckGround
    public static void CheckGround(GameContext ctx, RoleEntity role) {
        if (role.GetVelocityY() > 0) {
            return;
        }
        // Ground:3/Trampoline:6/Ladder:7
        var layerMask = 1 << 3 | 1 << 6;
        role.isOnGround = false;
        Collider2D[] hits = Physics2D.OverlapBoxAll(role.Pos() + Vector2.down * role.height / 2, new Vector2(0.98f, 0.1f), 0, layerMask);
        if (hits.Length == 0) {
        }
        foreach (var hit in hits) {
            if (hit.gameObject.layer == 3) {
                role.ReuseJumpTimes();
                role.Anim_JumpEnd();
                role.isOnGround = true;
            } else if (hit.gameObject.layer == 6) {
                var prop = hit.GetComponentInParent<PropEntity>();
                prop.isOwnerOnTrampoline = true;
            }
        }
    }
    #endregion

    #region Skill
    internal static void CD_Tick(GameContext ctx, RoleEntity role, float dt) {
        var skillCom = role.skillCom;
        skillCom.Foreach(skill => {
            skill.cd -= dt;
            if (skill.cd <= 0) {
                skill.cd = 0;
            }
        });
    }

    public static void CurrentSkill_Tick(GameContext ctx, RoleEntity role) {
        var skillCom = role.skillCom;
        if (skillCom.GetCurrentKey() != InputKeyEnum.None) {
            return;
        }
        var waitToCastKeys = ctx.input.waitToCastSkills;
        var usableSkillKeys = skillCom.usableSkillKeys;
        usableSkillKeys.Clear();
        foreach (var key in waitToCastKeys) {
            skillCom.TryGet(key, out var skill);
            if (skill.cd <= 0) {
                usableSkillKeys.Add(key);
            }
        }

        if (usableSkillKeys.Count == 0) {
            skillCom.SetCurrentKey(InputKeyEnum.None);
            role.anim.SetBool("B_StandShoot", false);
            role.fsm.EnterNormal();
            return;
        }

        if (usableSkillKeys.Contains(InputKeyEnum.Skill4)) {
            role.isFlashKeyDown = true;
        }

        if (usableSkillKeys.Contains(InputKeyEnum.SKill3)) {
            skillCom.SetCurrentKey(InputKeyEnum.SKill3);
        } else if (usableSkillKeys.Contains(InputKeyEnum.SKill2)) {
            skillCom.SetCurrentKey(InputKeyEnum.SKill2);
        } else if (usableSkillKeys.Contains(InputKeyEnum.SKill1)) {
            skillCom.SetCurrentKey(InputKeyEnum.SKill1);
        }

        role.fsm.EnterCasting();
        // Debug.Log(skillCom.waitToCastKeys.Count);
    }

    internal static void Casting(GameContext ctx, RoleEntity role, float dt) {
        var skillCom = role.skillCom;

        InputKeyEnum key = skillCom.GetCurrentKey();
        if (key == InputKeyEnum.None) {
            return;
        }

        skillCom.TryGet(key, out var skill);
        if (role.fsm.isEnterCastStageReset) {
            role.fsm.isEnterCastStageReset = false;
            role.fsm.RestCastStage(skill);
        }

        ref var stage = ref role.fsm.skillCastStage;
        if (stage == SkillCastStage.PreCast) {
            role.fsm.preCastTimer -= dt;
            if (role.fsm.preCastTimer <= 0) {
                stage = SkillCastStage.Casting;
                skill.cd = skill.cdMax;
            }
        } else if (stage == SkillCastStage.Casting) {
            role.fsm.castingIntervalTimer -= dt;
            if (role.fsm.castingIntervalTimer <= 0) {
                role.fsm.castingIntervalTimer = skill.castingIntervalSec;
                // todo发射技能
                role.Anim_Shoot(ctx.input.moveAxis.x);
                var bullet = BulletDomain.Spawn(ctx, skill.bulletTypeID, role.LaunchPoint(), role.ally, skill.damageRate);
                bullet.faceDir = role.GetForWard();
                bullet.SetForward();
            }
            role.fsm.castingMainTimer -= dt;
            if (role.fsm.castingMainTimer <= 0) {
                stage = SkillCastStage.EndCast;
            }
        } else if (stage == SkillCastStage.EndCast) {
            role.fsm.endCastTimer -= dt;
            if (role.fsm.endCastTimer <= 0) {
                role.fsm.isEnterCastStageReset = true;
                if (role.aiType == AIType.None) {
                    skillCom.SetCurrentKey(InputKeyEnum.None);
                }
            }
        }
    }
    #endregion

    #region Buff
    public static void Owner_Buff_Tick(GameContext ctx) {
        var owner = ctx.GetOwner();
        var buffCom = owner.buffCom;
        buffCom.Foreach(buff => {
            if (!buff.isPermanent) {
                return;
            }

            if (buff.isAddHp) {
                owner.hpMax += buff.addHpMax;
                owner.regenerationHpMax += buff.regenerationHpMax;
                buff.isPermanent = false;
            }
        });

    }
    #endregion
}