using UnityEngine;
using System;

public class UIApp {

    UIContext ctx;


    public UIApp() {
        ctx = new UIContext();
    }

    public void TearDown() {

    }

    public void Inject(AssetCore asset, Canvas screenCanvas, Canvas hudCanvas, EventCenter eventCenter) {
        ctx.Inject(asset, screenCanvas, hudCanvas, eventCenter);
    }

    public void HUD_Hints_Open(ulong typeAndID, Vector2 pos, int price) {
        HUD_Hints_Domain.Open(ctx, typeAndID, pos, price);
    }

    public void HUD_Hints_Hide(ulong typeAndID) {
        HUD_Hints_Domain.Hide(ctx, typeAndID);
    }

    public void HUD_Hints_Close(ulong typeAndID) {
        HUD_Hints_Domain.Close(ctx, typeAndID);
    }

    public void HUD_Hints_ShowHIntIcon(ulong typeAndID) {
        HUD_Hints_Domain.ShowHIntIcon(ctx, typeAndID);
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

    public void HUD_AltarBar_Open(float duration, Vector2 pos, int id) {
        HUD_AltarBar_Domain.Open(ctx, duration, pos, id);
    }

    public void HUD_AltarBar_Tick(float dt) {
        HUD_AltarBar_Domain.Init(ctx, dt);
    }

    public void HUD_AltarBar_Close() {
        HUD_AltarBar_Domain.Close(ctx);
    }

    public void HUD_HurtInfo_Open(Vector2 pos, int hurtInfo) {
        HUD_HurtInfo_Domain.Open(ctx, pos, hurtInfo);
    }

    public void HUD_HurtInfo_Close(HUD_HurtInfo hud) {
        HUD_HurtInfo_Domain.Close(ctx, hud);
    }

    public void HUD_HurtInfo_Tick(float dt) {
        HUD_HurtInfo_Domain.Tick(ctx, dt);
    }

    public void Panel_Login_Open() {
        Panel_Login_Domain.Open(ctx);
    }

    public void Panel_Login_Hide() {
        Panel_Login_Domain.Hide(ctx);
    }

    public void Panel_PlayerStatus_Open() {
        Panel_PlayerStatus_Domain.Open(ctx);
    }

    public void Panel_PlayerStatus_Hide() {
        Panel_PlayerStatus_Domain.Hide(ctx);
    }

    public void Panel_PlayerStatus_Update(int hpMax, int shield, int hp, int coinCount, BuffSlotComponent buffCom, Vector2 ownerPos, float duration, float dt) {
        Panel_PlayerStatus_Domain.Update_Tick(ctx, hpMax, shield, coinCount, hp, buffCom, ownerPos, duration, dt);
    }

    internal void Panel_PlayerStatus_EnterBoss() {
        Panel_PlayerStatus_Domain.EnterBoss(ctx);
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

    public void Panel_Result_Open() {
        Panel_Result_Domain.Open(ctx);
    }

    public void Panel_Result_Close() {
        Panel_Result_Domain.Close(ctx);
    }

}