using UnityEngine;
using System.Collections.Generic;
using System;

public class BuffSlotComponent {

    Dictionary<int, BuffSubEntity> all;
    BuffSubEntity[] temp;

    public BuffSlotComponent() {
        all = new Dictionary<int, BuffSubEntity>();
        temp = new BuffSubEntity[128];
    }

    public void Add(BuffSubEntity buff) {
        if (all.ContainsKey(buff.typeID)) {
            all[buff.typeID].count++;
            return;
        }
        all.Add(buff.typeID, buff);
    }

    public void Remove(BuffSubEntity buff) {
        all.Remove(buff.typeID);
    }

    public void Foreach(Action<BuffSubEntity> action) {
        int len = TakeAll(out var allBuff);
        for (int i = 0; i < len; i++) {
            var buff = allBuff[i];
            action.Invoke(buff);
        }
    }

    public int TakeAll(out BuffSubEntity[] allBuff) {
        if (all.Count > temp.Length) {
            temp = new BuffSubEntity[(int)(all.Count * 1.5f)];
        }
        all.Values.CopyTo(temp, 0);
        allBuff = temp;
        return all.Count;
    }
}