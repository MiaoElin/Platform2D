using UnityEngine;

public static class BulletDomain {

    public static BulletEntity Spawn(GameContext ctx, int typeID, Vector2 pos, Ally ally) {
        var bullet = GameFactory.Bullet_Spawn(ctx, typeID, pos, ally);
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

    public static void Move(BulletEntity bullet, float dt) {
        bullet.Move(dt);
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
            role.isDead = true;
        }
    }

    public static void HitCheck(GameContext ctx, BulletEntity bullet) {
        var layerMask = 1 << 8;
        RaycastHit2D[] all = Physics2D.RaycastAll(bullet.Pos(), bullet.moveDir, 0.1f, layerMask);
        foreach (var hit in all) {
            var role = hit.collider.GetComponentInParent<RoleEntity>();
            if (role.ally == bullet.ally) {
                continue;
            } else {
                bullet.isTearDown = true;
                role.hp -= (int)bullet.damgage;
                if (role.hp <= 0) {
                    if (role.shield > 0) {
                        role.shield -= Mathf.Abs(role.hp);
                    } else if (role.shield <= 0) {
                        role.isDead = true;
                    }
                }
            }
        }

    }
}