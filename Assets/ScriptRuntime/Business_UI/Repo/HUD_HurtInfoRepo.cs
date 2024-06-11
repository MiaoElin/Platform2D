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

    // public void For() {
    //     List<int> all = new List<int>() { 0, 1, 2, 3, 4, 5 };

    //     // Plan A error
    //     // value 0 1 2 4 5
    //     // index 0 1 2 3 4
    //     for (int i = 0; i < all.Count; i += 1) {
    //         if (i == 3) {
    //             all.Remove(i);
    //         }
    //     }

    //     // Plan B correct
    //     // value 0 1 2 4 5
    //     // index 0 1 2 3 4 5
    //     for (int i = all.Count - 1; i >= 0; i -= 1) {
    //         if (i == 3) {
    //             all.Remove(i);
    //         }
    //     }

    //     // Plan C
    //     int[] temp = all.ToArray();
    //     // List 0 1 2 4 5
    //     // temp 0 1 2 3 4 5
    //     //      0 1 2 3 4 5
    //     for (int i = 0; i < temp.Length; i += 1) {
    //         if (i == 3) {
    //             all.Remove(i);
    //         }
    //     }

    // }

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