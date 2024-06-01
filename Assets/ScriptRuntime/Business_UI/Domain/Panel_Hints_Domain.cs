using UnityEngine;

public static class Panel_Hints_Domain {

    public static void Open(UIContext ctx, Vector2 screenPos) {
        var panel = ctx.uIRepo.TryGet<Panel_Hints>();
        if (panel == null) {
            ctx.asset.TryGet_UI_Prefab(typeof(Panel_Hints).Name, out var prefab);
            panel = GameObject.Instantiate(prefab, ctx.screenCanvas).GetComponent<Panel_Hints>();
            panel.Ctor();
            ctx.uIRepo.Add(typeof(Panel_Hints).Name, panel.gameObject);
        }
        panel.SetPos(screenPos);
        panel.gameObject.SetActive(true);
    }

    public static void Close(UIContext ctx) {
        var panel = ctx.uIRepo.TryGet<Panel_Hints>();
        panel?.gameObject.SetActive(false);
    }
}