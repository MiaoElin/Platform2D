using UnityEngine;

public static class Panel_SkillSlot_Domain {

    public static void Open(UIContext ctx, float mask1TimeMax, float mask2TimeMax, float mask3TimeMax, float mask4TimeMax) {
        var panel = ctx.uIRepo.TryGet<Panel_SkillSlot>();
        if (panel == null) {
            ctx.asset.TryGet_UI_Prefab(typeof(Panel_SkillSlot).Name, out var prefab);
            panel = GameObject.Instantiate(prefab, ctx.screenCanvas).GetComponent<Panel_SkillSlot>();
            panel.Ctor(mask1TimeMax, mask2TimeMax, mask3TimeMax, mask4TimeMax);
            ctx.uIRepo.Add(typeof(Panel_SkillSlot).Name, panel.gameObject);
        }
        panel.gameObject.SetActive(true);
    }

    public static void CD_Tick(UIContext ctx, float skill1Time, float skill2Time, float skill3Time, float skill4Time) {
        var paenl = ctx.uIRepo.TryGet<Panel_SkillSlot>();
        paenl?.CD_Tick(skill1Time, skill2Time, skill3Time, skill4Time);
    }

    public static void Hide(UIContext ctx) {
        var paenl = ctx.uIRepo.TryGet<Panel_SkillSlot>();
        paenl?.gameObject.SetActive(false);
    }
}