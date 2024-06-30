using UnityEngine;

public static class GameBusiness_EnterNextStage {

    public static void Tick(GameContext ctx) {
        var fsm = ctx.game.fsm;
        if (fsm.isEnterNextStage) {
            fsm.isEnterNextStage = false;
            // 销毁场景所有东西
            ctx.roleRepo.Foreach(role => {
                if (role.ally == Ally.Player) {
                    return;
                }
                // 销毁UI
                UIDomain.HUD_HPBar_Close(ctx, role.id);
                // 销毁角色
                RoleDomain.Unspawn(ctx, role);
            });

            ctx.bulletRepo.Foreach(bullet => {
                BulletDomain.UnSpawn(ctx, bullet);
            });

            ctx.propRepo.Foreach(prop => {
                UIDomain.HUD_Hints_Close(ctx, prop.GetTypeAddID());
                UIDomain.HUD_AltarBar_Close(ctx);
                PropDomain.UnSpawn(ctx, prop);
            });

            ctx.lootRepo.Foreach(loot => {
                UIDomain.HUD_Hints_Close(ctx, loot.GetTypeAndID());
                LootDomain.UnSpawn(ctx, loot);
            });

            MapDomain.Unspawn(ctx);
            GameObject.Destroy(ctx.backScene.gameObject);

            ctx.currentStageID += 1;
            GameGameDomain.EnterStage(ctx, ctx.currentStageID);
        }
    }
}