using UnityEngine;

public static class UIDomain {

    public static void Panel_Hints_Open(GameContext ctx, Vector2 screenPos) {
        ctx.uIApp.Panel_Hints_Open(screenPos);
    }

    public static void Panel_Hints_Close(GameContext ctx) {
        ctx.uIApp.Panel_Hints_Close();
    }
}