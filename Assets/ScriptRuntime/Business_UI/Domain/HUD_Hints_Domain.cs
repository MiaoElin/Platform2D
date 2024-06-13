using UnityEngine;

public static class HUD_Hints_Domain {

    public static void Open(UIContext ctx, ulong typeAndID, Vector2 pos, int price) {
        ctx.hud_HintsRepo.TryGet(typeAndID, out var hud);
        if (hud == null) {
            ctx.asset.TryGet_UI_Prefab(typeof(HUD_Hints).Name, out var prefab);
            hud = GameObject.Instantiate(prefab, ctx.hudCanvas).GetComponent<HUD_Hints>();
            hud.Ctor(price);
            ctx.hud_HintsRepo.Add(typeAndID, hud);
        }
        hud.SetPos(pos);
        hud.HideHintIcon();
    }

    public static void ShowHIntIcon(UIContext ctx, ulong typeAndID) {
        ctx.hud_HintsRepo.TryGet(typeAndID, out var hud);
        if (hud != null) {
            hud.ShowHintsIcon();
        }
    }


    public static void Hide(UIContext ctx, ulong typeAndID) {
        ctx.hud_HintsRepo.TryGet(typeAndID, out var hud);
        hud?.HideHintIcon();
    }

    public static void Close(UIContext ctx, ulong typeAndID) {
        ctx.hud_HintsRepo.TryGet(typeAndID, out var hud);
        if (hud) {
            ctx.hud_HintsRepo.Remove(typeAndID);
            GameObject.Destroy(hud.gameObject);
        }
    }
}