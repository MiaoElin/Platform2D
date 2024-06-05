using UnityEngine;

public static class Panel_SkillSlot_Domain {

    public static void Open(UIContext ctx) {
        var panel = ctx.uIRepo.TryGet<Panel_SkillSlot>();
        if (panel == null) {
            ctx.asset.TryGet_UI_Prefab(typeof(Panel_SkillSlot).Name, out var prefab);
            panel = GameObject.Instantiate(prefab, ctx.screenCanvas).GetComponent<Panel_SkillSlot>();
            panel.Ctor();
        }
        panel.gameObject.SetActive(true);
    }

    public static void Hide(UIContext ctx) {
        var paenl = ctx.uIRepo.TryGet<Panel_SkillSlot>();
        paenl?.gameObject.SetActive(false);
    }
}