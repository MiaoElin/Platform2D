using System;
using UnityEngine;
using System.Collections.Generic;

public static class RoleDomain {

    public static RoleEntity Spawn(GameContext ctx, int typeID, Vector2 pos, Vector3 rotation, Ally ally, Vector2[] path) {
        var role = GameFactory.Role_Spawn(ctx, typeID, pos, rotation, ally, path);
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

    #region  AI
    internal static void AI_MeetTOwner_Check(GameContext ctx, RoleEntity role) {
        // Vector2.SqrMagnitude(role.)
    }

    // 搜索距离Tick
    public static void AI_Monster_SerchRange_Tick(GameContext ctx, RoleEntity role) {
        // 目前只有Monster有这个需求（发现owner，要向owner移动），所以只Monster搜索
        var owner = ctx.GetOwner();
        var dir = owner.Pos() - role.Pos();
        bool isInSearchRange = false;
        if (role.ally == Ally.Monster) {
            if (role.aiType == AIType.Common) {
                // y轴在2m差距之内
                if (Mathf.Abs(dir.y) <= 2) {
                    isInSearchRange = Mathf.Abs(dir.x) <= role.searchRange;
                }
            } else if (role.aiType == AIType.Flyer || role.aiType == AIType.Elite) {
                isInSearchRange = PureFunction.IsInRange(owner.Pos(), role.Pos(), role.searchRange);
            }
        }

        if (isInSearchRange) {
            role.hasTarget = true;
        } else {
            role.hasTarget = false;
        }
    }

    internal static void AI_Move_Stop(GameContext ctx, RoleEntity role) {
        role.Move_AxisX_Stop();
        var dir = (ctx.GetOwner().Pos() - role.Pos()).normalized;
        role.SetForward(dir.x);
    }

    public static void Owner_Move_Stop(GameContext ctx) {
        ctx.GetOwner().Move_AxisX_Stop();
    }

    // 攻击距离Tick
    internal static bool AI_EnterAttakRange_Tick(GameContext ctx, RoleEntity role) {
        bool isInAttackRange = false;

        if (role.ally == Ally.Monster) {
            if (role.hasTarget) {
                var target = ctx.GetOwner().Pos();
                isInAttackRange = PureFunction.IsInRange(target, role.Pos(), role.skillCom.GetCurrentSkill().attackRange);
            }
        } else if (role.ally == Ally.Player) {
            isInAttackRange = FindNearlyEnemy(ctx, role, out var nearlyEnemy);
            if (isInAttackRange) {
                role.targeID = nearlyEnemy.id;
            }
        }
        return isInAttackRange;
    }

    internal static void AI_SetCurrentSkill(GameContext ctx, RoleEntity role) {
        var skillCom = role.skillCom;
        skillCom.SetCurrentKey(InputKeyEnum.SKill1);
    }
    #endregion 

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

    #region Collider
    private static void On_Owner_TriggerExitEvent(GameContext ctx, Collider2D other) {
        if (other.gameObject.tag == "Loot") {
            var loot = other.GetComponentInParent<LootEntity>();
            // Debug.Assert(loot != null);
            if (loot == null) {
                return;
            }
            if (loot.fsm.status == LootStatus.Normal && loot.needHints) {
                UIDomain.HUD_Hints_Hide(ctx, loot.GetTypeAndID());
            }
        }

        if (other.tag == "Prop") {
            var prop = other.GetComponentInParent<PropEntity>();
            if (!prop) {
                return;
            }
            if (prop.isAltar) {
                UIDomain.HUD_Hints_Hide(ctx, prop.GetTypeAddID());
            }
            if (prop.isHurtFire) {
                prop.fsm.EnterNormal();
            }
        }
    }

    private static void On_Owner_TriggerEnterEvent(GameContext ctx, Collider2D other) {
        if (other.tag == "Loot") {
            var loot = other.GetComponentInParent<LootEntity>();
            if (loot.fsm.status == LootStatus.Normal && loot.needHints) {
                UIDomain.HUD_Hints_ShowHIntIcon(ctx, loot.GetTypeAndID());
            }
        } else if (other.tag == "Prop") {
            var prop = other.GetComponentInParent<PropEntity>();
            if (prop.isHurtFire) {
                prop.fsm.EnterHurt();
            }
            if (prop.isAltar) {
                UIDomain.HUD_Hints_ShowHIntIcon(ctx, prop.GetTypeAddID());
            }
            if (prop.isTrampoline) {
                ctx.GetOwner().SetVelocityY(prop.jumpForce);
                ctx.GetOwner().fsm.EnterTrampoline();
            }
        }
    }

    private static void On_Owner_TriggerStayEvent(GameContext ctx, Collider2D other) {
        On_Owner_Trigger_LootStayEvent(ctx, other);
        On_Owner_Trigger_PropStayEvent(ctx, other);
    }

    private static void On_Owner_Trigger_PropStayEvent(GameContext ctx, Collider2D other) {
        if (other.tag != "Prop") {
            return;
        }
        var prop = other.GetComponentInParent<PropEntity>();
        if (prop.fsm.status != PropStatus.Normal) {
            return;
        }
        if (ctx.input.isInteractKeyDown) {
            ctx.input.isInteractKeyDown = false;
            if (prop.isAltar) {
                // 启动祭坛计时
                UIDomain.HUD_AltarBar_Open(ctx, prop.altarDuration, prop.setHintsPoint.position);
                UIDomain.HUD_Hints_Close(ctx, prop.GetTypeAddID());
                // boos 出场
                ctx.player.isEnterBossTime = true;
            }
        }

    }

    public static void On_Owner_Trigger_LootStayEvent(GameContext ctx, Collider2D other) {
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
                SFXDomain.Loot_Open_Play(ctx, loot.getLootClip, loot.volume);
                ctx.player.coinCount -= loot.price;
                if (loot.isDropLoot) {
                    bool has = ctx.asset.TryGetLootTMArray(out List<LootTM> allloot);
                    if (has) {
                        int index = UnityEngine.Random.Range(0, allloot.Count);
                        int typeID = allloot[index].typeID;
                        var newLoot = LootDomain.Spawn(ctx, typeID, loot.Pos() + Vector2.up * 3, Vector3.zero, Vector3.one);
                        newLoot.fsm.EnterEasingIn(loot.Pos(), loot.Pos() + Vector2.up * 4f);
                        // 关闭UI
                        UIDomain.HUD_Hints_Close(ctx, loot.GetTypeAndID());
                        // loot进入used状态
                        loot.fsm.EnterUsed();
                    }
                } else if (loot.isGetCoin) {
                    ctx.player.coinCount += loot.coinCount;
                    loot.fsm.EnterUsed();
                    UIDomain.HUD_Hints_Close(ctx, loot.GetTypeAndID());
                    // 掉落金币（表现）
                    // 随机方向
                    for (int i = 0; i < 5; i++) {
                        LootDomain.SpawnCoin(ctx, loot.coinTypeID, loot.Pos());
                    }

                } else if (loot.isGetRole) {
                    // 随机生成一种robot 
                    ctx.asset.TryGetRobotTMArray(out var allRobot);
                    int index = UnityEngine.Random.Range(0, allRobot.Count);
                    var role = RoleDomain.Spawn(ctx, allRobot[index].typeID, loot.Pos(), Vector3.zero, owner.ally, null);
                    // 将robot的速度跟owner设为一样
                    role.moveSpeed = owner.moveSpeed;
                    // 将robot的技能设为1技能
                    RoleDomain.AI_SetCurrentSkill(ctx, role);
                    // 打开LineR
                    role.skillCom.TryGet(InputKeyEnum.SKill1, out var skill);
                    if (skill.isCure) {
                        role.OpenLineR(owner.Pos());
                    }
                    owner.robotCom.Add(role.id);

                    loot.fsm.EnterUsed();
                    UIDomain.HUD_Hints_Close(ctx, loot.GetTypeAndID());
                }
            }

        } else if (loot.isGetBuff) {
            SFXDomain.Loot_Open_Play(ctx, loot.getLootClip, loot.volume);
            var buff = BuffDomain.Spawn(ctx, loot.buffTypeId);
            owner.buffCom.Add(buff);

            // Calculate: 叠加一次
            loot.fsm.EnterDestroy();
            // loot.isDead = true;
        }
    }
    #endregion

    #region  Move
    public static void Onwer_Move_ByAxiX(GameContext ctx, RoleEntity role) {
        role.MoveByAxisX(ctx.input.moveAxis.x);
        role.SetForward(ctx.input.moveAxis.x);
    }

    public static void Move_ByAxisY(GameContext ctx, RoleEntity role, float moveAxisY) {
        role.MoveByAxisY(moveAxisY);
    }

    public static void Owner_Move_InCasting(GameContext ctx, RoleEntity role) {
        role.MoveByAxisX(ctx.input.moveAxis.x);
        bool has = FindNearlyEnemy(ctx, role, out var nearlyEnemy);
        if (has) {
            // 只考虑竖向高于owner5m 或者低于owenr5m的角色(考虑角色高度差，y不能相等）、因为owner的技能都是左右发射的
            if (nearlyEnemy.Pos().y > role.Pos().y - 5 && nearlyEnemy.Pos().y < role.Pos().y + 5) {
                var dir = nearlyEnemy.Pos() - role.Pos();
                role.SetForward(dir.x);
            }
        }
    }

    public static void AI_Move(GameContext ctx, RoleEntity role, float dt) {
        var owner = ctx.GetOwner();
        var dir = (owner.Pos() - role.Pos()).normalized;

        if (role.aiType == AIType.Common) {
            // if (role.hasTarget) {
            //     // 在路径范围内追owner
            //     role.MoveByAxisX(dir.x);
            //     role.SetForward(dir.x);
            //     if (role.Pos().x > role.pathXMax) {
            //         role.SetPos(new Vector2(role.pathXMax, role.Pos().y));
            //     } else if (role.Pos().x < role.pathXMin) {
            //         role.SetPos(new Vector2(role.pathXMin, role.Pos().y));
            //     }
            // } else {
            //     role.MoveByPath(dt);
            // }
            bool isInGroundSide = CheckFoot_Front(role);
            bool isMeetWall_Short = CheckWall_Short(role);
            // normal 往前走，走到有墙 或者 脚前方没有东西 往返方向
            if (!isInGroundSide || isMeetWall_Short) {
                role.SetForward(-role.GetForWard().x);
                role.MoveByAxisX(role.GetForWard().x);
            } else {
                if (role.hasTarget) {
                    role.MoveByAxisX(dir.x);
                    role.SetForward(dir.x);
                } else {
                    role.MoveByAxisX(role.GetForWard().x);
                }
            }
            // }

        } else if (role.aiType == AIType.Elite) {
            bool isInGroundSide = CheckFoot_Front(role);
            bool isMeetWallShort = CheckWall_Short(role);
            // 有target，跟随目标 如果前面有墙（高墙停在原地，矮墙起跳）
            if (role.hasTarget) {
                if (isMeetWallShort) {
                    // 高墙
                    if (CheckWall_Hight(role)) {
                        dir = Vector2.zero;
                    } else {
                        // 矮墙
                        role.isJumpKeyDown = true;
                    }
                }
                // 爬梯
                // x轴距离想等，dir设为0；
                if (MathF.Abs(dir.x) < role.moveSpeed * dt) {
                    dir = Vector2.zero;
                }
                role.MoveByAxisX(dir.x);
                role.SetForward(dir.x);
            } else {
                // normal 往前走，走到有墙 或者 脚前方没有东西 往返方向
                if (!isInGroundSide || isMeetWallShort) {
                    role.SetForward(-role.GetForWard().x);
                }
                role.MoveByAxisX(role.GetForWard().x);
            }

        } else if (role.aiType == AIType.Flyer) {
            if (role.hasTarget) {
                role.MoveByTarget(owner.Pos(), dt);
            }
        } else if (role.aiType == AIType.Robot) {
            role.MoveByTarget(owner.robotCom.GetRobotPositon(role.id), dt);
            if (role.isCureRole) {
                role.LR_Tick(owner.Pos());
            }
        }
    }
    #endregion

    public static bool FindNearlyEnemy(GameContext ctx, RoleEntity role, out RoleEntity nearlyEnermy) {
        float nearlyDistance = Mathf.Pow(role.searchRange, 2);
        RoleEntity nearEnemy = null;
        ctx.roleRepo.Foreach(enemy => {
            if (enemy.fsm.status == RoleStatus.Destroy) {
                return;
            }
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

    #region  Jump
    public static void Jump(GameContext ctx, RoleEntity role) {
        role.Jump();
        role.isJumpKeyDown = false;
    }
    #endregion

    #region Falling
    public static void Falling(RoleEntity role, float dt) {
        role.Falling(dt);
    }
    #endregion

    #region  Check
    public static void CheckGround(GameContext ctx, RoleEntity role) {
        if (role.aiType != AIType.None && role.aiType != AIType.Elite) {
            return;
        }

        if (role.GetVelocityY() > 0) {
            return;
        }
        role.isOnGround = false;
        // Ground:3/Trampoline:6/Ladder:7
        var layerMask = 1 << 3 | 1 << 6;
        Collider2D[] hits = Physics2D.OverlapBoxAll(role.GetFoot(), new Vector2(0.9f, 0.1f), 0, layerMask);
        if (hits.Length == 0) {
        }
        foreach (var hit in hits) {
            if (hit.gameObject.layer == 3) {
                role.ReuseJumpTimes();
                // role.Anim_JumpEnd();
                role.isOnGround = true;
            }
        }
    }

    public static bool CheckFoot_Front(RoleEntity role) {
        LayerMask map = 1 << 3;
        RaycastHit2D ray = Physics2D.Raycast(role.GetFoot_Front(), Vector2.down, 1f, map);
        if (ray) {
            return true;
        } else {
            return false;
        }
    }
    public static bool CheckWall_Short(RoleEntity role) {
        LayerMask map = 1 << 3;
        Collider2D other = Physics2D.OverlapBox(role.GetBody_Center(), new Vector2(1f, 1f), 0, map);
        Debug.DrawRay(role.GetBody_Center(), -role.GetForWard() * 1f, Color.red);
        if (other) {
            return true;
        } else {
            return false;
        }
    }

    public static bool CheckWall_Hight(RoleEntity role) {
        LayerMask map = 1 << 3;
        RaycastHit2D hit = Physics2D.Raycast(role.GetHead_Front(), role.GetForWard(), 2f, map);
        if (hit) {
            return true;
        } else {
            return false;
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

        if (usableSkillKeys.Count > 0) {
            if (usableSkillKeys.Contains(InputKeyEnum.Skill4)) {
                skillCom.SetCurrentKey(InputKeyEnum.Skill4);
            } else if (usableSkillKeys.Contains(InputKeyEnum.SKill3)) {
                skillCom.SetCurrentKey(InputKeyEnum.SKill3);
            } else if (usableSkillKeys.Contains(InputKeyEnum.SKill2)) {
                skillCom.SetCurrentKey(InputKeyEnum.SKill2);
            } else if (usableSkillKeys.Contains(InputKeyEnum.SKill1)) {
                skillCom.SetCurrentKey(InputKeyEnum.SKill1);
            }
        }
    }

    internal static void Casting(GameContext ctx, RoleEntity role, float dt) {
        var skillCom = role.skillCom;
        InputKeyEnum key = skillCom.GetCurrentKey();
        if (key == InputKeyEnum.None) {
            return;
        }
        skillCom.TryGet(key, out var skill);
        // if (role.isOwner) {
        //     Debug.Log(key + "" + role.fsm.skillCastStage);
        // }

        if (role.fsm.isEnterCastStageReset) {
            role.fsm.isEnterCastStageReset = false;
            role.fsm.RestCastStage(skill);
        }

        ref var stage = ref role.fsm.skillCastStage;
        if (stage == SkillCastStage.PreCast) {
            role.anim_Attack();
            role.fsm.preCastTimer -= dt;
            if (role.fsm.preCastTimer <= 0) {

                role.fsm.preCastTimer = 0;
                stage = SkillCastStage.Casting;
                skill.cd = skill.cdMax;
                // sfx
                if (role.isOwner) {
                    SFXDomain.Onwer_SKill_Play(ctx, skill.castSfx, skill.volume);
                } else {
                    SFXDomain.Role_Skill_Play(ctx, skill.castSfx, skill.volume);
                }
            }

        } else if (stage == SkillCastStage.Casting) {
            role.fsm.castingIntervalTimer -= dt;
            if (role.fsm.castingIntervalTimer <= 0) {
                role.fsm.castingIntervalTimer = skill.castingIntervalSec;

                if (skill.isCastBullet) {
                    // todo发射技能
                    role.Anim_Shoot(ctx.input.moveAxis.x);

                    var bullet = BulletDomain.Spawn(ctx, skill.bulletTypeID, role.LaunchPoint(), role.ally, skill.stiffenSec);
                    if (bullet.moveType == MoveType.ByStatic) {
                        bullet.moveDir = role.GetForWard();
                    } else if (bullet.moveType == MoveType.ByTrack) {
                        bullet.targetID = role.targeID;
                    }

                }
                if (skill.isCastProp) {
                    var prop = PropDomain.Spawn(ctx, skill.propTypeID, role.LaunchPoint(), Vector3.zero, Vector3.one, false, Vector2.one, 0, role.ally);
                    prop.moveDir = role.GetForWard();
                    prop.SetForward();
                }
                if (skill.isCure) {
                    var owner = ctx.GetOwner();
                    // if (owner.hp < owner.hpMax) {
                    // todo缓动
                    //     role.LineREnable(true);
                    // } else {
                    //     role.LineREnable(false);
                    // }
                    owner.hp += skill.addHpMax;
                    if (owner.hp > owner.hpMax) {
                        owner.hp = owner.hpMax;
                    }
                }
                if (skill.isMelee) {
                    var owner = ctx.GetOwner();
                    // 扣血
                    int hurt = (int)(skill.meleeDamageRate * CommonConst.BASEDAMAGE);
                    Role_Hurt(ctx, owner, hurt);
                    // anim

                    // EnterSuffering
                    owner.fsm.EnterSuffering(skill.stiffenSec);
                }
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

    #region OwnerHurt

    public static int FindMinTimerShield(GameContext ctx, out int shieldValue) {
        var owner = ctx.GetOwner();
        int minShiledTypeID = default;
        float minTimer = float.MaxValue;
        int shield = 0;
        owner.buffCom.Foreach(buff => {
            if (!buff.isGetShield) {
                return;
            }

            if (buff.shieldCD > 0) {
                return;
            }

            if (buff.shieldTimer > 0) {
                if (buff.shieldTimer < minTimer) {
                    // 且护盾有值
                    owner.ShieldDicTryget(buff.typeID, out var value);
                    if (value > 0) {
                        minTimer = buff.shieldTimer;
                        minShiledTypeID = buff.typeID;
                        shield = value;
                    }
                }
            }
        });

        shieldValue = shield;

        if (minTimer != float.MaxValue) {
            return minShiledTypeID;
        } else {
            return minShiledTypeID;
        }
    }

    public static void Role_Hurt(GameContext ctx, RoleEntity role, int damage) {
        int shield = role.GetallShield();
        // 扣血的ui显示
        UIDomain.HUD_HurtInfo_Open(ctx, role.Pos() + Vector2.up * 2, damage);

        if (shield < damage) {
            role.BuffShieldUseAll();
            role.hp -= damage - shield;
            if (role.hp <= 0) {
                role.fsm.EnterDestroy();
                if (role.ally == Ally.Monster) {
                    ctx.player.coinCount += role.price;
                }
            }
            return;
        }
        int typeid = FindMinTimerShield(ctx, out var shieldValue);
        role.BuffShieldReduce(typeid, damage);
        while (shieldValue < damage) {
            damage -= shieldValue;
            FindMinTimerShield(ctx, out var shieldValueNext);
            role.BuffShieldReduce(typeid, damage);
            shieldValue = shieldValueNext;
        }
    }
    #endregion

    #region Buff
    public static void Owner_Buff_Tick(GameContext ctx, float dt) {
        var owner = ctx.GetOwner();
        var buffCom = owner.buffCom;
        buffCom.Foreach(buff => {
            if (!buff.isPermanent) {
                return;
            }

            if (buff.isAddHp) {
                owner.hpMax += buff.addHpMax;
                owner.hp += buff.addHpMax;
                owner.regenerationHpMax += buff.regenerationHpMax;
                buff.isPermanent = false;
            }

            if (buff.isGetShield) {
                ref var cd = ref buff.shieldCD;
                ref var timer = ref buff.shieldTimer;
                var duration = buff.shieldDuration;
                cd -= dt;
                if (cd <= 0) {
                    timer -= dt;
                    if (timer <= 0) {
                        buff.hasAdd = false;
                        timer = duration;
                        cd = buff.shieldCDMax;
                        RemoveShield(owner, buff);
                    } else {
                        if (!buff.hasAdd) {
                            buff.hasAdd = true;
                            GetShield(owner, buff);
                        }
                    }
                }
            }
        });

    }

    // Buff Attach:

    // Buff Tick Interval:

    // Buff Remove:

    public static void GetShield(RoleEntity Owner, BuffSubEntity buff) {
        int hpMax = Owner.hpMax;
        float value = hpMax * buff.shieldPersent + buff.shieldValue;
        value *= buff.count; // 完全叠加，后面有特殊的再处理
        Owner.BuffShieldAdd(buff.typeID, (int)value);
    }

    public static void RemoveShield(RoleEntity Owner, BuffSubEntity buff) {
        Owner.BuffShieldRemove(buff.typeID);
    }
    #endregion

    #region Ladder
    public static void EnterLadder(GameContext ctx, RoleEntity role) {
        ctx.propRepo.Foreach(prop => {
            if (!prop.isLadder) {
                return;
            }
            var pos = role.Pos();
            var moveAxis = Vector2.zero;
            if (role.isOwner) {
                moveAxis = ctx.input.moveAxis;
            } else if (role.aiType == AIType.Elite) {
                moveAxis = (ctx.GetOwner().Pos() - role.Pos()).normalized;
            }

            Vector2 lowPos = prop.Pos() + Vector2.down * (prop.srBaseSize.y / 2) + Vector2.left * prop.srBaseSize.x / 2;
            Vector2 hightPos = prop.Pos() + Vector2.up * (prop.srBaseSize.y / 2) + Vector2.right * prop.srBaseSize.x / 2;

            float head_Center_Offset = role.GetHead_Front().y - role.Pos().y;
            float foot_Center_Offset = role.Pos().y - role.GetFoot().y;         //素材的中心点不在角色身高的中心点导致的问题

            float lowestY = lowPos.y + head_Center_Offset + 2f;//head_Front localpos()
            float highestY = hightPos.y + foot_Center_Offset + 0.2f; // 0.2f作为编辑器的偏差量。角色爬到顶上高一点再落地，也会有种缓动的感觉

            // 限制x的范围
            if (pos.x > lowPos.x && pos.x < hightPos.x) {
                // 往上爬的Y范围
                if (pos.y + head_Center_Offset > lowPos.y && pos.y < hightPos.y) {
                    if (moveAxis.y > 0) {
                        role.fsm.EnterLadder(lowestY, highestY);
                    }
                    // 往下爬的Y的范围 
                } else if (pos.y > hightPos.y && pos.y < highestY) {   // 大于highest 到达顶部，collider变硬、 小于是开始下降
                    if (moveAxis.y < 0) {
                        role.fsm.EnterLadder(lowestY, highestY);
                    }
                }
            }
        });
    }
    #endregion
}