using UnityEngine;

public static class Panel_PlayerStatus_Domain {

    public static void Open(UIContext ctx, int coinCount) {
        var panel = ctx.uIRepo.TryGet<Panel_PlayerStatus>();
        if (panel == null) {
            ctx.asset.TryGet_UI_Prefab(typeof(Panel_PlayerStatus).Name, out var prefab);
            panel = GameObject.Instantiate(prefab, ctx.screenCanvas).GetComponent<Panel_PlayerStatus>();
        }
        panel.Init(coinCount);
        panel.gameObject.SetActive(true);
    }

    public static void Hide(UIContext ctx) {
        var panel = ctx.uIRepo.TryGet<Panel_PlayerStatus>();
        panel?.gameObject.SetActive(false);
    }

    public static void Update_Tick(UIContext ctx, int coinCount) {
        var panel = ctx.uIRepo.TryGet<Panel_PlayerStatus>();
        panel?.Init(coinCount);
    }
}