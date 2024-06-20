using UnityEngine;

public static class SFXDomain {

    public static void Onwer_SKill_Play(GameContext ctx, AudioClip clip, float volume) {
        if (clip != null) {
            ctx.soundCore.OwnerSkillPlay(clip, volume);
        }
    }

    public static void BGM_Play(GameContext ctx) {
        var map = ctx.GetCurrentMap();
        ctx.soundCore.BGM_Play(map.bgm, map.bgmVolume);
    }

    public static void Role_Skill_Play(GameContext ctx, AudioClip clip, float volume) {
        if (clip != null) {
            ctx.soundCore.RoleSkillPlay(clip, volume);
        }
    }

    public static void Loot_Open_Play(GameContext ctx, AudioClip clip, float volume) {
        if (clip != null) {
            ctx.soundCore.Loot_Open_Play(clip, volume);
        }
    }
}