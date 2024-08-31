using UnityEngine;

public static class Panel_Result_Domain {

    public static void Open(UIContext ctx, bool isWin) {
        var panel = ctx.uIRepo.TryGet<Panel_Result>();
        if (panel == null) {
            var name = typeof(Panel_Result).Name;
            ctx.asset.TryGet_UI_Prefab(name, out var prefab);
            panel = GameObject.Instantiate(prefab, ctx.screenCanvas).GetComponent<Panel_Result>();
            panel.Ctor();
            panel.OnNewGameClickHandle = () => { ctx.eventCenter.NewGame(); };
            panel.OnExitClickHandle = () => { ctx.eventCenter.ExitGame(); };
            ctx.uIRepo.Add(name, panel.gameObject);
        }
        panel.SetTitle(isWin);
    }

    public static void Close(UIContext ctx) {
        var panel = ctx.uIRepo.TryGet<Panel_Result>();
        ctx.uIRepo.Remove(typeof(Panel_Result).Name);
        if (panel) {
            GameObject.Destroy(panel.gameObject);
        }
    }
}