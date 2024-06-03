using UnityEngine;

public static class HUD_HPBar_Domain {

    public static void Open(UIContext ctx, int id, int hpMax) {
        ctx.hud_HPBarRepo.TryGet(id, out var hud);
        if (hud == null) {
            ctx.asset.TryGet_UI_Prefab(typeof(HUD_HPBar).Name, out var prefab);
            hud = GameObject.Instantiate(prefab, ctx.hudCanvas).GetComponent<HUD_HPBar>();
            hud.Ctor(hpMax);
            ctx.hud_HPBarRepo.Add(id, hud);
        }
    }

    public static void Update_Tick(UIContext ctx, int id, int hp, Vector2 pos) {
        ctx.hud_HPBarRepo.TryGet(id, out var hud);
        hud?.Update_Tick(hp, pos);
    }

    public static void Close(UIContext ctx, int id) {
        ctx.hud_HPBarRepo.TryGet(id, out var hud);
        if (hud) {
            GameObject.Destroy(hud.gameObject);
        }
    }
}