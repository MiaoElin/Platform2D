using UnityEngine;

public class UIApp {

    UIContext ctx;


    public UIApp() {
        ctx = new UIContext();
    }

    public void Inject(Asset_Core asset, Canvas screenCanvas, Canvas hudCanvas) {
        ctx.Inject(asset, screenCanvas, hudCanvas);
    }

    public void HUD_Hints_Open(int id, Vector2 pos, int price) {
        HUD_Hints_Domain.Open(ctx, id, pos, price);
    }

    public void HUD_Hints_Hide(int id) {
        HUD_Hints_Domain.Hide(ctx, id);
    }
    
    public void HUD_Hints_Close(int id) {
        HUD_Hints_Domain.Close(ctx, id);
    }

    public void HUD_Hints_ShowHIntIcon(int id) {
        HUD_Hints_Domain.ShowHIntIcon(ctx, id);
    }

    public void Panel_PlayerStatus_Open(int coinCount) {
        Panel_PlayerStatus_Domain.Open(ctx, coinCount);
    }

    public void Panel_PlayerStatus_Hide() {
        Panel_PlayerStatus_Domain.Hide(ctx);
    }

    public void Panel_PlayerStatus_Update(int coinCount,BuffSlotComponent buffCom) {
        Panel_PlayerStatus_Domain.Update_Tick(ctx, coinCount,buffCom);
    }
}