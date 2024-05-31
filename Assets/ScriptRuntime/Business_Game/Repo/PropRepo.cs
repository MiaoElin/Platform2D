using UnityEngine;
using System.Collections.Generic;
using System;

public class PropRepo {
    Dictionary<int, PropEntity> all;
    PropEntity[] temp;

    public PropRepo() {
        all = new Dictionary<int, PropEntity>();
        temp = new PropEntity[128];
    }

    public void Add(PropEntity prop) {
        all.Add(prop.id, prop);
    }

    public bool TryGet(int typeID, out PropEntity prop) {
        return all.TryGetValue(typeID, out prop);
    }

    public void Remove(PropEntity prop) {
        all.Remove(prop.id);
    }

    public void Foreach(Action<PropEntity> action) {
        foreach (var prop in all.Values) {
            action(prop);
        }
    }

    // Dictionary 不能直接移除，会影响遍历；所以要拷贝到一个数组，数组有空值；
    public int TakeAll(out PropEntity[] Allprop) {
        if (all.Count > temp.Length) {
            temp = new PropEntity[(int)(all.Count * 1.5f)];
        }
        all.Values.CopyTo(temp, 0);
        Allprop = temp;
        return all.Count;
    }
}