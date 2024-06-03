using UnityEngine;

public static class UIDomain {

    public static void HUD_Hints_Open(GameContext ctx, int id, Vector2 pos, int price) {
        ctx.uIApp.HUD_Hints_Open(id, pos, price);
    }

    public static void HUD_Hints_Hide(GameContext ctx, int id) {
        ctx.uIApp.HUD_Hints_Hide(id);
    }

    public static void HUD_Hints_Close(GameContext ctx, int id) {
        ctx.uIApp.HUD_Hints_Close(id);
    }

    public static void HUD_Hints_ShowHIntIcon(GameContext ctx, int id) {
        ctx.uIApp.HUD_Hints_ShowHIntIcon(id);
    }

    public static void Panel_PlayerStatus_Open(GameContext ctx) {
        ctx.uIApp.Panel_PlayerStatus_Open(ctx.player.coinCount);
    }

    public static void Panel_PlayerStatus_Hide(GameContext ctx) {
        ctx.uIApp.Panel_PlayerStatus_Hide();
    }

    public static void Panel_PlayerStatus_Update(GameContext ctx) {
        ctx.uIApp.Panel_PlayerStatus_Update(ctx.player.coinCount, ctx.GetOwner().buffCom);
    }
}