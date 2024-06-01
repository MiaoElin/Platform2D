using UnityEngine;

public class UIApp {

    UIContext ctx;


    public UIApp() {
        ctx = new UIContext();
    }

    public void Inject(Asset_Core asset, Canvas screenCanvas,Canvas hudCanvas) {
        ctx.Inject(asset, screenCanvas,hudCanvas);
    }

    public void Panel_Hints_Open(Vector2 pos) {
        HUD_Hints_Domain.Open(ctx, pos);
    }

    public void Panel_Hints_Close() {
        HUD_Hints_Domain.Close(ctx);
    }
}