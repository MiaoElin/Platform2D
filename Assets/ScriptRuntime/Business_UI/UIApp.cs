using UnityEngine;

public class UIApp {

    UIContext ctx;


    public UIApp() {
        ctx = new UIContext();
    }

    public void Inject(Asset_Core asset, Canvas screenCanvas, Canvas hudCanvas) {
        ctx.Inject(asset, screenCanvas, hudCanvas);
    }

    public void Panel_Hints_Open(Vector2 pos) {
        HUD_Hints_Domain.Open(ctx, pos);
    }

    public void Panel_Hints_Hide() {
        HUD_Hints_Domain.Hide(ctx);
    }

    public void Panel_PlayerStatus_Open(int coinCount) {
        Panel_PlayerStatus_Domain.Open(ctx, coinCount);
    }

    public void Panel_PlayerStatus_Hide() {
        Panel_PlayerStatus_Domain.Hide(ctx);
    }

    public void Panel_PlayerStatus_Update(int coinCount) {
        Panel_PlayerStatus_Domain.Update_Tick(ctx, coinCount);
    }
}