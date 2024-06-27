using UnityEngine;

public static class GameGameDomain {

    public static void ExitGame(GameContext ctx) {
        ctx.roleRepo.Foreach(role => {
            // 销毁UI
            UIDomain.HUD_HPBar_Close(ctx, role.id);
            // 销毁角色
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