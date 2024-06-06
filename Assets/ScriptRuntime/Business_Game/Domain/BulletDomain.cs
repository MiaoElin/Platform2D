using UnityEngine;

public static class BulletDomain {

    public static BulletEntity Spawn(GameContext ctx, int typeID, Vector2 pos, Ally ally) {
        var bullet = GameFactory.Bullet_Spawn(ctx, typeID, pos, ally);
        ctx.bulletRepo.Add(bullet);
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
}