using UnityEngine;

public static class BulletDomain {

    public static BulletEntity Spawn(GameContext ctx, int typeID, Vector2 pos, Ally ally, float stiffenSec) {
        var bullet = GameFactory.Bullet_Spawn(ctx, typeID, pos, ally, stiffenSec);
        ctx.bulletRepo.Add(bullet);
        // bullet.onTriggerEnterHandle += (Collider2D other) => {
        //     On_Trigger_EnterRoleEvent(ctx, bullet, other);
        // };
        return bullet;
    }

    public static void UnSpawn(GameContext ctx, BulletEntity bullet) {
        ctx.bulletRepo.Remove(bullet);
        bullet.Reuse();
        bullet.gameObject.SetActive(false);
        ctx.poolService.ReturnBullet(bullet);
    }

    public static void Move(GameContext ctx, BulletEntity bullet, float dt) {
        if (bullet.moveType == MoveType.ByTrack) {
            if (bullet.ally == Ally.Player) {
                // 找最近的敌人
                bool has = ctx.roleRepo.TryGet(bullet.targetID, out var targetRole);
                if (has) {
                    if (targetRole.fsm.status == RoleStatus.Destroy) {
                        if (!bullet.hasSetMoveDir) {
                            bullet.hasSetMoveDir = true;
                            bullet.moveDir = targetRole.GetBody_Center() - bullet.Pos();
                        }
                        bullet.Move(bullet.moveDir, dt);
                        return;
                    }
                    bullet.MoveByTarget(targetRole.Pos());
                }
            } else if (bullet.ally == Ally.Monster) {
                bullet.MoveByTarget(ctx.GetOwner().Pos());
            }
        } else {
            bullet.Move(bullet.moveDir, dt);
        }
        // anim
        bullet.Anim_Shoot();
    }

    public static void On_Trigger_EnterRoleEvent(GameContext ctx, BulletEntity bullet, Collider2D other) {
        RoleEntity role = other.GetComponentInParent<RoleEntity>();
        if (role.ally == bullet.ally) {
            return;
        }
        bullet.isTearDown = true;

        role.hp -= (int)bullet.damgage;
        if (role.hp <= 0) {
            role.fsm.EnterDestroy();
        }
    }

    public static void HitCheck(GameContext ctx, BulletEntity bullet) {
        var layerMask = 1 << 8 | 1 << 3;
        RaycastHit2D[] all = Physics2D.RaycastAll(bullet.Pos(), bullet.moveDir, 0.1f, layerMask);
        foreach (var hit in all) {
            if (hit.collider.tag == "Role") {
                var role = hit.collider.GetComponentInParent<RoleEntity>();
                if (role.fsm.status == RoleStatus.Destroy) {
                    continue;
                }
                if (role.ally == bullet.ally) {
                    continue;
                } else {
                    bullet.isTearDown = true;
                    int damgage = (int)bullet.damgage;
                    RoleDomain.Role_Hurt(ctx, role, damgage);
                    if (role.hp > 0) {
                        // EnterSuffering
                        role.fsm.EnterSuffering(bullet.stiffenSec, role.fsm.lastStatus);
                    }
                }
            }
            if (hit.collider.tag == "Ground") {
                bullet.isTearDown = true;
            }

        }
    }
}