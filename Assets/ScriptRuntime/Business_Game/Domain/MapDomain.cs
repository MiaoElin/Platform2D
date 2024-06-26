using UnityEngine;

public static class MapDomain {

    public static MapEntity Spawn(GameContext ctx, int stageID) {
        var map = GameFactory.Map_Create(ctx, stageID);
        ctx.mapRepo.Add(map);
        GFPathFinding.xCount = map.xCount;
        GFPathFinding.yCount = map.yCount;

        var propSpawnerTMs = map.propSpawnerTMs;
        {
            foreach (var tm in propSpawnerTMs) {
                var prop = PropDomain.Spawn(ctx, tm.propTypeID, tm.pos, tm.rotation, tm.localScale, tm.isModifySize, tm.sizeScale, tm.jumpForce, tm.ally);
                if (prop.isAltar) {
                    UIDomain.HUD_Hints_Open(ctx, prop.GetTypeAddID(), prop.setHintsPoint.position + Vector3.up, 0);
                }
            }
        }

        var lootSpawnerTMs = map.lootSpawnerTMs;
        {
            foreach (var tm in lootSpawnerTMs) {
                var loot = LootDomain.Spawn(ctx, tm.lootTypeID, tm.pos, tm.rotation, tm.localScale);
                UIDomain.HUD_Hints_Open(ctx, loot.GetTypeAndID(), loot.setHintsPoint.position, loot.price);
            }
        }

        var roleSpawnerTMs = map.roleSpawnerTMs;
        {
            foreach (var tm in roleSpawnerTMs) {
                tm.cd = 0;
            }
        }
        return map;
    }

    public static void Unspawn(GameContext ctx) {
        var map = ctx.GetCurrentMap();
        ctx.mapRepo.Remove(map);
        GameObject.Destroy(map.gameObject);
    }

    public static void WaveTick(GameContext ctx, int stageID, float dt) {
        var map = ctx.GetCurrentMap();
        var roleSpawnerTMs = map.roleSpawnerTMs;
        {
            foreach (var tm in roleSpawnerTMs) {
                if (tm.isBossWave) {
                    if (!ctx.player.isEnterBossTime) {
                        continue;
                    }
                }
                if (tm.cd <= 0) {
                    bool isInRange = PureFunction.IsInRange(ctx.GetOwner().Pos(), tm.pos, tm.searchRange); // 后面再改，Aitype.Common 检测x轴
                    if (!isInRange) {
                        continue;
                    }
                    var role = RoleDomain.Spawn(ctx, tm.roleTypeID, tm.pos, tm.rotation, Ally.Monster, tm.path);
                    RoleDomain.AI_SetCurrentSkill(ctx, role);
                    // 打开血量条
                    UIDomain.HUD_HPBar_Open(ctx, role.id, role.hpMax);
                    tm.cd = tm.cdMax;
                } else {
                    if (tm.isPermanent) {
                        tm.cd -= dt;
                    }
                }
            }
        }
    }

}