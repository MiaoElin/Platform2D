using UnityEngine;

public static class GameBusiness_Pause {
    public static void Tick(GameContext ctx, float dt) {
        var fsm = ctx.game.fsm;
        if (fsm.isEnterPause) {
            fsm.isEnterPause = false;
            Time.timeScale = 0;
            UIDomain.Panel_Pause_Open(ctx);
        }

        if (ctx.input.isPasueKeyDown) {
            ctx.input.isPasueKeyDown = false;
            fsm.EnterNormal();
            Time.timeScale = 1;
            UIDomain.Panel_Pause_Hide(ctx);
        }
    }
}