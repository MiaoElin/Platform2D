using UnityEngine;

public static class GameGameDomain {

    public static void ExitGame(GameContext ctx) {
        ctx.roleRepo.Foreach(role => {
            RoleDomain.Unspawn(ctx, role);
        });

        ctx.bulletRepo.Foreach(bullet => {
            BulletDomain.UnSpawn(ctx, bullet);
        });

        ctx.propRepo.Foreach(prop => {
            PropDomain.UnSpawn(ctx, prop);
        });

        ctx.lootRepo.Foreach(loot => {
            LootDomain.UnSpawn(ctx, loot);
        });

        MapDomain.Unspawn(ctx);
    }
}