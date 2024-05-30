using UnityEngine;

public static class MapDomain {

    public static MapEntity Spawn(GameContext ctx, int stageID) {
        var map = GameFactory.Map_Create(ctx, stageID);
        ctx.mapRepo.Add(map);

        var propSpawnerTMs = map.propSpawnerTMs;
        {
            foreach (var tm in propSpawnerTMs) {
                Prop_Spawn(ctx, tm.propTypeID, tm.pos, tm.rotation, tm.localScale, tm.isModifySize, tm.sizeScale);
            }
        }

        return map;
    }

    public static PropEntity Prop_Spawn(GameContext ctx, int typeID, Vector2 pos, Vector3 rotaion, Vector3 localScale, bool isModifySize, Vector2 sizeScale) {
        var prop = GameFactory.Prop_Spawn(ctx, typeID, pos, rotaion, localScale, isModifySize, sizeScale);
        ctx.propRepo.Add(prop);
        return prop;
    }
}