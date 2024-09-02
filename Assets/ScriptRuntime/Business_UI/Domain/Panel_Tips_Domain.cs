using UnityEngine;
using UnityEngine.EventSystems;

public static class Panel_Tips_Domain {

    public static void Open(UIContext ctx) {
        var label = typeof(Panel_Tips).Name;
        var panel = ctx.uIRepo.TryGet<Panel_Tips>();
        if (panel == null) {
            ctx.asset.TryGet_UI_Prefab(label, out var prefab);
            panel = GameObject.Instantiate(prefab, ctx.screenCanvas).GetComponent<Panel_Tips>();
            panel.Ctor();
        }
        panel.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(panel.btn_Close.gameObject);
    }

}