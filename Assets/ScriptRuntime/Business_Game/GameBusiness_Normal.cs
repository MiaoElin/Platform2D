using UnityEngine;

public static class GameBusiness_Normal {

    public static void EnterStage(GameContext ctx) {
        MapDomain.Spawn(ctx, 1);

        var role = RoleDomain.Spawn(ctx, 100, new Vector2(0, 1.5f), Ally.Player);
        ctx.ownerID = role.id;
    }

    public static void Tick(GameContext ctx) {
        var owner = ctx.GetOwner();
        RoleDomain.Move(ctx, owner);
    }
}