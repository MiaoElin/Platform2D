using UnityEngine;
using UnityEngine.EventSystems;

public static class Panel_Login_Domain {
    public static void Open(UIContext ctx) {
        var panel = ctx.uIRepo.TryGet<Panel_Login>();
        if (panel == null) {
            var name = typeof(Panel_Login).Name;
            ctx.asset.TryGet_UI_Prefab(name, out var prefab);
            panel = GameObject.Instantiate(prefab, ctx.screenCanvas).GetComponent<Panel_Login>();
            panel.Ctor();
            panel.OnStarClickHandle = () => { ctx.eventCenter.StartGame(); };
            panel.OnExitHandle = () => { ctx.eventCenter.ExitGame(); };
            ctx.uIRepo.Add(name, panel.gameObject);
        }
        panel.Show();
        EventSystem.current.SetSelectedGameObject(panel.btn_Start.gameObject);
    }

    public static void Hide(UIContext ctx) {
        var panel = ctx.uIRepo.TryGet<Panel_Login>();
        panel?.Hide();
    }

}