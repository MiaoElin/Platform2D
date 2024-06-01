using UnityEngine;

public static class UIDomain {

    public static void Panel_Hints_Open(GameContext ctx, Vector2 pos) {
        ctx.uIApp.Panel_Hints_Open(pos);
    }

    public static void Panel_Hints_Close(GameContext ctx) {
        ctx.uIApp.Panel_Hints_Close();
    }
}