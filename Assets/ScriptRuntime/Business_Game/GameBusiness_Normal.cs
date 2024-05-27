using UnityEngine;

public static class GameBusiness_Normal {

    public static void EnterStage(GameContext ctx) {
        var role = RoleDomain.Spawn(ctx, 100, Vector2.zero, Ally.Player);
        ctx.ownerID = role.id;
    }
}