using UnityEngine;
using System.Collections.Generic;
using System;

public class HUD_HurtInfoRepo {

    List<HUD_HurtInfo> all;
    HUD_HurtInfo[] temp;
    public HUD_HurtInfoRepo() {
        all = new List<HUD_HurtInfo>();
        temp = new HUD_HurtInfo[128];
    }

    public void Add(HUD_HurtInfo hud) {
        all.Add(hud);
    }

    public void Remove(HUD_HurtInfo hud) {
        all.Remove(hud);
    }

    public void Foreach(Action<HUD_HurtInfo> action) {
        int allLen = TakeAll(out var allInfo);
        for (int i = 0; i < allLen; i++) {
            var hud = allInfo[i];
            action.Invoke(hud);
        }
    }

    public int TakeAll(out HUD_HurtInfo[] allInfo) {
        if (all.Count > temp.Length) {
            temp = new HUD_HurtInfo[(int)(all.Count * 1.5f)];
        }
        all.CopyTo(0, temp, 0, all.Count);
        allInfo = temp;
        return all.Count;
    }
}