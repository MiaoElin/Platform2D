using UnityEngine;
using System.Collections.Generic;
using System;

public class LootRepo {

    public Dictionary<int, LootEntity> all;
    LootEntity[] temp;
    public LootRepo() {
        all = new Dictionary<int, LootEntity>();
        temp = new LootEntity[128];
    }

    public void Add(LootEntity loot) {
        all.Add(loot.id, loot);
    }

    public bool TryGet(int typeID, out LootEntity loot) {
        return all.TryGetValue(typeID, out loot);
    }

    public void Remove(LootEntity loot) {
        all.Remove(loot.id);
    }

    public void Foreach(Action<LootEntity> action) {
        foreach (var loot in all.Values) {
            action(loot);
        }
    }
    
    public int TakeAll(out LootEntity[] allprop) {
        if (all.Count > temp.Length) {
            temp = new LootEntity[(int)(all.Count * 1.5f)];
        }
        all.Values.CopyTo(temp, 0);
        allprop = temp;
        return all.Count;
    }
}