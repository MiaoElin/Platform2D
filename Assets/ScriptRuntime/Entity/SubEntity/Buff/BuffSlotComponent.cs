using UnityEngine;
using System.Collections.Generic;
using System;

public class BuffSlotComponent {

    public Dictionary<int, BuffSubEntity> all;
    BuffSubEntity[] temp;

    public BuffSlotComponent() {
        all = new Dictionary<int, BuffSubEntity>();
        temp = new BuffSubEntity[128];
    }

    public void Add(BuffSubEntity buff) {
        all.Add(buff.id, buff);
    }

    public void Remove(BuffSubEntity buff) {
        all.Remove(buff.id);
    }

    public void Foreach(Action<BuffSubEntity> action) {
        foreach (var prop in all.Values) {
            action(prop);
        }
    }

    public int TakeAll(out BuffSubEntity[] Allprop) {
        if (all.Count > temp.Length) {
            temp = new BuffSubEntity[(int)(all.Count * 1.5f)];
        }
        all.Values.CopyTo(temp, 0);
        Allprop = temp;
        return all.Count;
    }
}