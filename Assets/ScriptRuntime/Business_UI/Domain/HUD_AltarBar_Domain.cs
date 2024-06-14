using UnityEngine;

public static class HUD_AltarBar_Domain {

    public static void Open(UIContext ctx, float duration,Vector2 pos) {
        var hud = ctx.uIRepo.TryGet<HUD_AltarBar>();
        if (hud == null) {
            ctx.asset.TryGet_UI_Prefab(typeof(HUD_AltarBar).Name, out var prefab);
            hud = GameObject.Instantiate(prefab, ctx.hudCanvas).GetComponent<HUD_AltarBar>();
            hud.Ctor(duration);
            hud.SetPos(pos);
            ctx.uIRepo.Add(typeof(HUD_AltarBar).Name, hud.gameObject);
        }
    }

    public static void Init(UIContext ctx, float dt) {
        var hud = ctx.uIRepo.TryGet<HUD_AltarBar>();
        hud?.Init(dt);
    }

    public static void Close(UIContext ctx) {
        var hud = ctx.uIRepo.TryGet<HUD_AltarBar>();
        if (hud) {
            ctx.uIRepo.Remove(typeof(HUD_AltarBar).Name);
            GameObject.Destroy(hud.gameObject);
        }
    }
}