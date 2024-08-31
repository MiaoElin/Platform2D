using UnityEngine;

public static class Panel_Pause_Domain {

    public static void Open(UIContext ctx) {
        var label = typeof(Panel_Pause).Name;
        var panel = ctx.uIRepo.TryGet<Panel_Pause>();
        if (panel == null) {
            ctx.asset.TryGet_UI_Prefab(label, out var prefab);
            panel = GameObject.Instantiate(prefab, ctx.screenCanvas).GetComponent<Panel_Pause>();
            panel.OnClickRestartHandle = () => {
                ctx.eventCenter.RestartGame();
            };
            panel.OnClickExitHandle = () => { ctx.eventCenter.ExitGame(); };
            panel.Ctor();
            ctx.uIRepo.Add(label, panel.gameObject);
        }
        panel.gameObject.SetActive(true);
    }

    public static void Hide(UIContext ctx) {
        var panel = ctx.uIRepo.TryGet<Panel_Pause>();
        panel?.gameObject.SetActive(false);
    }

    public static void Close(UIContext ctx) {
        var panel = ctx.uIRepo.TryGet<Panel_Pause>();
        ctx.uIRepo.Remove(typeof(Panel_Pause).Name);
        GameObject.Destroy(panel.gameObject);
    }
}