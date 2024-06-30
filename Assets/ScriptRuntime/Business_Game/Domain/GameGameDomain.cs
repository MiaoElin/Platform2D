using UnityEngine;

public static class GameGameDomain {

    public static void EnterStage(GameContext ctx, int stageID) {

        // Map
        var map = MapDomain.Spawn(ctx, stageID);
        ctx.currentStageID = stageID;

        // BackScene
        var backScene = GameFactory.BackScene_Create(ctx);
        backScene.Ctor(map.backSceneBG, map.backSceneMid, map.backSceneFront);
        ctx.backScene = backScene;

        // Owner
        var owner = ctx.GetOwner();
        if (owner == null) {
            owner = RoleDomain.Spawn(ctx, 10, map.ownerEnterPos, Vector3.zero, Ally.Player, null);
            owner.isOwner = true;
            ctx.ownerID = owner.id;
        } else {
            owner.SetPos(map.ownerEnterPos);
            ctx.roleRepo.Foreach(role => {
                if (role.aiType == AIType.Robot) {
                    role.SetPos(map.ownerEnterPos + Vector2.up * 5);
                }
            });
        }

        // player
        ctx.player.coinCount = 500;

        // Camera
        ctx.camera.SetFollow(owner.transform);
        ctx.camera.SetLookAt(owner.transform);
        ctx.camera.SetConfiner(map.cameraPolygonCollider);

        // UI
        UIDomain.Panel_PlayerStatus_Open(ctx);
        UIDomain.Panel_SkillSlot_Open(ctx);

        ctx.game.fsm.EnterNormal();
        SFXDomain.BGM_Play(ctx);
    }

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