using UnityEngine;

public static class BuffDomain {

    public static BuffSubEntity Spawn(GameContext ctx, int typeID) {
        var buff = GameFactory.Buff_Spawn(ctx, typeID);
        return buff;
    }
}