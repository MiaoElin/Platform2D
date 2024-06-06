using UnityEngine;

public static class MapDomain {

    public static MapEntity Spawn(GameContext ctx, int stageID) {
        var map = GameFactory.Map_Create(ctx, stageID);
        ctx.mapRepo.Add(map);

        var propSpawnerTMs = map.propSpawnerTMs;
        {
            foreach (var tm in propSpawnerTMs) {
                PropDomain.Spawn(ctx, tm.propTypeID, tm.pos, tm.rotation, tm.localScale, tm.isModifySize, tm.sizeScale, tm.jumpForce);
            }
        }

        var lootSpawnerTMs = map.lootSpawnerTMs;
        {
            foreach (var tm in lootSpawnerTMs) {
                var loot = LootDomain.Spawn(ctx, tm.lootTypeID, tm.pos, tm.rotation, tm.localScale);
                UIDomain.HUD_Hints_Open(ctx, loot.id, loot.Pos() + Vector2.up * 3, loot.price);
            }
        }

        var roleSpawnerTMs = map.roleSpawnerTMs;
        {
            foreach (var tm in roleSpawnerTMs) {
                var role = RoleDomain.Spawn(ctx, tm.roleTypeID, tm.pos, Ally.Monster, tm.path);
                // 打开血量条
                UIDomain.HUD_HPBar_Open(ctx, role.id, role.hpMax);
            }
        }
        return map;
    }

}