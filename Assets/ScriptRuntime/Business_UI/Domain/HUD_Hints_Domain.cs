using UnityEngine;

public static class HUD_Hints_Domain {

    public static void Open(UIContext ctx, Vector2 pos) {
        var panel = ctx.uIRepo.TryGet<HUD_Hints>();
        if (panel == null) {
            ctx.asset.TryGet_UI_Prefab(typeof(HUD_Hints).Name, out var prefab);
            panel = GameObject.Instantiate(prefab, ctx.hudCanvas).GetComponent<HUD_Hints>();
            panel.Ctor();
            ctx.uIRepo.Add(typeof(HUD_Hints).Name, panel.gameObject);
        }
        panel.SetPos(pos);
        panel.gameObject.SetActive(true);
    }

    public static void Hide(UIContext ctx) {
        var panel = ctx.uIRepo.TryGet<HUD_Hints>();
        panel?.gameObject.SetActive(false);
    }
}