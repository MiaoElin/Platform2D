using System;
using UnityEngine;

public static class SFXDomain {

    public static void Onwer_SKill_Play(GameContext ctx, AudioClip clip, float volume) {
        if (clip != null) {
            ctx.soundCore.OwnerSkillPlay(clip, volume);
        }
    }

    public static void BGM_Play(GameContext ctx) {
        var map = ctx.GetCurrentMap();
        if (map != null) {
            ctx.soundCore.BGM_Play(map.bgm, map.bgmVolume);
        }
    }

    public static void Login_BGM_Play(GameContext ctx) {
        var configTM = ctx.asset.configTM;
        if (configTM != null) {
            ctx.soundCore.BGM_Play(configTM.sfx_BGM_Login, configTM.volume_Sfx_Login);
        }
    }

    public static void Role_Skill_Play(GameContext ctx, AudioClip clip, float volume) {
        if (clip != null) {
            ctx.soundCore.RoleSkillPlay(clip, volume);
        }
    }

    internal static void BTN_Interact_Play(GameContext ctx) {
        var configTM = ctx.asset.configTM;
        if (configTM) {
            ctx.soundCore.Btn_Interact_Play(configTM.sfx_btn_Interact, configTM.sfx_btn_Interact_Voluem);
        }
    }

    public static void Loot_Open_Play(GameContext ctx, AudioClip clip, float volume) {
        if (clip != null) {
            ctx.soundCore.Loot_Open_Play(clip, volume);
        }
    }

    internal static void RoleDeadPlay(GameContext ctx, AudioClip clip, float volume) {
        if (clip != null) {
            ctx.soundCore.Role_Dead_Play(clip, volume);
        }
    }
}