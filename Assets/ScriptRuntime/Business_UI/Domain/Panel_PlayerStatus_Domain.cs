using UnityEngine;

public static class Panel_PlayerStatus_Domain {

    public static void Open(UIContext ctx) {
        var panel = ctx.uIRepo.TryGet<Panel_PlayerStatus>();
        if (panel == null) {
            ctx.asset.TryGet_UI_Prefab(typeof(Panel_PlayerStatus).Name, out var prefab);
            panel = GameObject.Instantiate(prefab, ctx.screenCanvas).GetComponent<Panel_PlayerStatus>();
            panel.Ctor();
            ctx.uIRepo.Add(typeof(Panel_PlayerStatus).Name, panel.gameObject);
        }
        panel.gameObject.SetActive(true);
    }

    public static void Hide(UIContext ctx) {
        var panel = ctx.uIRepo.TryGet<Panel_PlayerStatus>();
        panel?.gameObject.SetActive(false);
    }

    public static void Update_Tick(UIContext ctx, int hpMax, int shield, int coinCount, int hp, BuffSlotComponent buffCom) {
        var panel = ctx.uIRepo.TryGet<Panel_PlayerStatus>();
        panel?.Init(hpMax, shield, coinCount, hp);

        buffCom.Foreach(buff => {
            if (buff.isPermanent) {
                // Debug.Log(buff.id);
                panel.NewElement(buff.id, buff.icon, buff.count);
            }
        });

    }
}