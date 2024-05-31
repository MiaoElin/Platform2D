using UnityEngine;

public static class MapDomain {

    public static MapEntity Spawn(GameContext ctx, int stageID) {
        var map = GameFactory.Map_Create(ctx, stageID);
        ctx.mapRepo.Add(map);

        var propSpawnerTMs = map.propSpawnerTMs;
        {
            foreach (var tm in propSpawnerTMs) {
                PropDomain.Spawn(ctx, tm.propTypeID, tm.pos, tm.rotation, tm.localScale, tm.isModifySize, tm.sizeScale);
            }
        }

        return map;
    }

}