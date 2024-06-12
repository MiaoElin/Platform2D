using UnityEngine;

public static class HUD_Hints_Domain {

    public static void Open(UIContext ctx, int id, Vector2 pos, int price) {
        ctx.hud_HintsRepo.TryGet(id, out var hud);
        if (hud == null) {
            ctx.asset.TryGet_UI_Prefab(typeof(HUD_Hints).Name, out var prefab);
            hud = GameObject.Instantiate(prefab, ctx.hudCanvas).GetComponent<HUD_Hints>();
            hud.Ctor(price);
            ctx.hud_HintsRepo.Add(id, hud);
        }
        hud.SetPos(pos);
        hud.HideHintIcon();
    }

    public static void ShowHIntIcon(UIContext ctx, int id) {
        ctx.hud_HintsRepo.TryGet(id, out var hud);
        if (hud != null) {
            hud.ShowHintsIcon();
        }
    }


    public static void Hide(UIContext ctx, int id) {
        ctx.hud_HintsRepo.TryGet(id, out var hud);
        hud?.HideHintIcon();
    }

    public static void Close(UIContext ctx, int id) {
        ctx.hud_HintsRepo.TryGet(id, out var hud);
        if (hud) {
            ctx.hud_HintsRepo.Remove(id);
            GameObject.Destroy(hud.gameObject);
        }
    }
}