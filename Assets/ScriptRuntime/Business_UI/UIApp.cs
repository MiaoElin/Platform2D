using UnityEngine;

public class UIApp {

    UIContext ctx;


    public UIApp() {
        ctx = new UIContext();
    }

    public void Inject(Asset_Core asset, Canvas screenCanvas) {
        ctx.Inject(asset, screenCanvas);
    }

    public void Panel_Hints_Open(Vector2 screenPos) {
        Panel_Hints_Domain.Open(ctx, screenPos);
    }

    public void Panel_Hints_Close() {
        Panel_Hints_Domain.Close(ctx);
    }
}