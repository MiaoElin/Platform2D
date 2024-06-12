using UnityEngine;
using System;

public static class UIDomain {

    public static void HUD_Hints_Open(GameContext ctx, int id, Vector2 pos, int price) {
        ctx.uIApp.HUD_Hints_Open(id, pos, price);
    }

    public static void HUD_Hints_Hide(GameContext ctx, int id) {
        ctx.uIApp.HUD_Hints_Hide(id);
    }

    public static void HUD_Hints_Close(GameContext ctx, int id) {
        ctx.uIApp.HUD_Hints_Close(id);
    }

    public static void HUD_Hints_ShowHIntIcon(GameContext ctx, int id) {
        ctx.uIApp.HUD_Hints_ShowHIntIcon(id);
    }

    public static void HUD_HPBar_Open(GameContext ctx, int id, int hpMax) {
        ctx.uIApp.HUD_HPBar_Open(id, hpMax);
    }

    public static void HUD_HPBar_UpdateTick(GameContext ctx, RoleEntity role) {
        ctx.uIApp.HUD_HPBar_UpdateTick(role.id, role.hp, role.Pos() + Vector2.up * (role.height / 2 + 1));
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


    public static void Panel_PlayerStatus_Open(GameContext ctx) {
        ctx.uIApp.Panel_PlayerStatus_Open();
    }

    public static void Panel_PlayerStatus_Hide(GameContext ctx) {
        ctx.uIApp.Panel_PlayerStatus_Hide();
    }

    public static void Panel_PlayerStatus_Update(GameContext ctx) {
        var owner = ctx.GetOwner();
        ctx.uIApp.Panel_PlayerStatus_Update(owner.hpMax, owner.GetallShield(), owner.hp, ctx.player.coinCount, owner.buffCom);
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

}