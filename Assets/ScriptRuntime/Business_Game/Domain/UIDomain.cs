using UnityEngine;

public static class UIDomain {

    public static void Panel_Hints_Open(GameContext ctx, Vector2 pos) {
        ctx.uIApp.Panel_Hints_Open(pos);
    }

    public static void Panel_Hints_Hide(GameContext ctx) {
        ctx.uIApp.Panel_Hints_Hide();
    }

    public static void Panel_PlayerStatus_Open(GameContext ctx) {
        ctx.uIApp.Panel_PlayerStatus_Open(ctx.player.coinCount);
    }

    public static void Panel_PlayerStatus_Hide(GameContext ctx) {
        ctx.uIApp.Panel_PlayerStatus_Hide();
    }

    public static void Panel_PlayerStatus_Update(GameContext ctx) {
        ctx.uIApp.Panel_PlayerStatus_Update(ctx.player.coinCount);
    }
}