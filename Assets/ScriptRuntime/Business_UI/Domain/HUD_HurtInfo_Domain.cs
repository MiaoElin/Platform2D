using UnityEngine;
using System;

public static class HUD_HurtInfo_Domain {

    public static void Open(UIContext ctx, Vector2 pos, int hurtInfo) {
        ctx.asset.TryGet_UI_Prefab(typeof(HUD_HurtInfo).Name, out var prefab);
        HUD_HurtInfo hud = GameObject.Instantiate(prefab, ctx.hudCanvas).GetComponent<HUD_HurtInfo>();
        hud.SetPos(pos);
        hud.Init(hurtInfo);
        ctx.hUD_HurtInfoRepo.Add(hud);
    }

    public static void Close(UIContext ctx, HUD_HurtInfo hud) {
        ctx.hUD_HurtInfoRepo.Remove(hud);
        hud.Close();
    }

    public static void Tick(UIContext ctx, float dt) {
        ctx.hUD_HurtInfoRepo.Foreach(hud => {
            if (hud.isTearDown) {
                Close(ctx, hud);
            } else {
                hud.Easing(dt);
            }
        });
    }
}