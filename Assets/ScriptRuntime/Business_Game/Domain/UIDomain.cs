using UnityEngine;
using System;

public static class UIDomain {

    public static void HUD_Hints_Open(GameContext ctx, ulong typeAndID, Vector2 pos, int price) {
        ctx.uIApp.HUD_Hints_Open(typeAndID, pos, price);
    }

    public static void HUD_Hints_Hide(GameContext ctx, ulong typeAndID) {
        ctx.uIApp.HUD_Hints_Hide(typeAndID);
    }

    public static void HUD_Hints_Close(GameContext ctx, ulong typeAndID) {
        ctx.uIApp.HUD_Hints_Close(typeAndID);
    }

    public static void HUD_Hints_ShowHIntIcon(GameContext ctx, ulong typeAndID) {
        ctx.uIApp.HUD_Hints_ShowHIntIcon(typeAndID);
    }

    public static void HUD_HPBar_Open(GameContext ctx, int id, int hpMax) {
        ctx.uIApp.HUD_HPBar_Open(id, hpMax);
    }

    public static void HUD_HPBar_UpdateTick(GameContext ctx, RoleEntity role) {
        ctx.uIApp.HUD_HPBar_UpdateTick(role.id, role.hp, role.GetHead_Top() + Vector2.up * 2);
    }

    public static void HUD_HPBar_Close(GameContext ctx, int id) {
        ctx.uIApp.HUD_HPBar_Close(id);
    }

    public static void HUD_HurtInfo_Open(GameContext ctx, Vector2 pos, int hurtInfo) {
        ctx.uIApp.HUD_HurtInfo_Open(pos, hurtInfo);
    }

    public static void HUD_HurtInfo_Close(GameContext ctx, HUD_HurtInfo hud) {
        ctx.uIApp.HUD_HurtInfo_Close(hud);
    }

    public static void HUD_HurtInfo_Tick(GameContext ctx, float dt) {
        ctx.uIApp.HUD_HurtInfo_Tick(dt);
    }

    public static void HUD_AltarBar_Open(GameContext ctx, float duration, Vector2 pos) {
        ctx.uIApp.HUD_AltarBar_Open(duration, pos);
    }

    public static void HUD_AltarBar_Tick(GameContext ctx, float dt) {
        ctx.uIApp.HUD_AltarBar_Tick(dt);
    }

    public static void Panel_Login_Open(GameContext ctx) {
        ctx.uIApp.Panel_Login_Open();
    }

    public static void Panel_Login_Hide(GameContext ctx) {
        ctx.uIApp.Panel_Login_Hide();
    }

    public static void HUD_AltarBar_Close(GameContext ctx) {
        ctx.uIApp.HUD_AltarBar_Close();
    }

    public static void Panel_PlayerStatus_Open(GameContext ctx) {
        ctx.uIApp.Panel_PlayerStatus_Open();
    }

    public static void Panel_PlayerStatus_Hide(GameContext ctx) {
        ctx.uIApp.Panel_PlayerStatus_Hide();
    }

    public static void Panel_PlayerStatus_EnterBoss(GameContext ctx) {
        ctx.uIApp.Panel_PlayerStatus_EnterBoss();
    }

    public static void Panel_PlayerStatus_Update(GameContext ctx, float dt) {
        var owner = ctx.GetOwner();
        ctx.uIApp.Panel_PlayerStatus_Update(owner.hpMax, owner.GetallShield(), owner.hp, ctx.player.coinCount, owner.buffCom, dt);
    }

    public static void Panel_SkillSlot_Open(GameContext ctx) {
        ctx.uIApp.Panel_SkillSlot_Open(
            GetMaskTimeMask(ctx, InputKeyEnum.SKill1),
            GetMaskTimeMask(ctx, InputKeyEnum.SKill2),
            GetMaskTimeMask(ctx, InputKeyEnum.SKill3),
            GetMaskTimeMask(ctx, InputKeyEnum.Skill4)
        );
    }

    public static void Panel_SkillSlot_CD_Tick(GameContext ctx) {
        ctx.uIApp.Panel_SkillSlot_CD_Tick(
            GetMaskTime(ctx, InputKeyEnum.SKill1),
            GetMaskTime(ctx, InputKeyEnum.SKill2),
            GetMaskTime(ctx, InputKeyEnum.SKill3),
            GetMaskTime(ctx, InputKeyEnum.Skill4)
        );
    }

    public static float GetMaskTimeMask(GameContext ctx, InputKeyEnum key) {
        ctx.GetOwner().skillCom.TryGet(key, out var skill);
        return skill.cdMax;
    }

    public static float GetMaskTime(GameContext ctx, InputKeyEnum key) {
        ctx.GetOwner().skillCom.TryGet(key, out var skill);
        return skill.cd;
    }

    public static void Panel_SkillSlot_Hide(GameContext ctx) {
        ctx.uIApp.Panel_SkillSlot_Hide();
    }

    public static void Panel_Result_Open(GameContext ctx) {
        ctx.uIApp.Panel_Result_Open();
    }

    public static void Panel_Result_Close(GameContext ctx) {
        ctx.uIApp.Panel_Result_Close();
    }

}