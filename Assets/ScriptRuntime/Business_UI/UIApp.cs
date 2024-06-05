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

    public void HUD_HPBar_Open(int id, int hpMax) {
        HUD_HPBar_Domain.Open(ctx, id, hpMax);
    }

    public void HUD_HPBar_UpdateTick(int id, int hp, Vector2 pos) {
        HUD_HPBar_Domain.Update_Tick(ctx, id, hp, pos);
    }

    public void HUD_HPBar_Close(int id) {
        HUD_HPBar_Domain.Close(ctx, id);
    }

    public void Panel_PlayerStatus_Open() {
        Panel_PlayerStatus_Domain.Open(ctx);
    }

    public void Panel_PlayerStatus_Hide() {
        Panel_PlayerStatus_Domain.Hide(ctx);
    }

    public void Panel_PlayerStatus_Update(int hpMax, int hp, int coinCount, BuffSlotComponent buffCom) {
        Panel_PlayerStatus_Domain.Update_Tick(ctx, hpMax, coinCount, hp, buffCom);
    }

    public void Panel_SkillSlot_Open(float mask1TimeMax, float mask2TimeMax, float mask3TimeMax, float mask4TimeMax) {
        Panel_SkillSlot_Domain.Open(ctx, mask1TimeMax, mask2TimeMax, mask3TimeMax, mask4TimeMax);
    }

    public void Panel_SkillSlot_Hide() {
        Panel_SkillSlot_Domain.Hide(ctx);
    }

    public void Panel_SkillSlot_CD_Tick(float skill1Time, float skill2Time, float skill3Time, float skill4Time) {
        Panel_SkillSlot_Domain.CD_Tick(ctx, skill1Time, skill2Time, skill3Time, skill4Time);
    }
}