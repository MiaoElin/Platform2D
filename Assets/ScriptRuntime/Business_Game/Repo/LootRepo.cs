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
        int len = TakeAll(out var allLoot);
        for (int i = 0; i < len; i++) {
            var loot = allLoot[i];
            action.Invoke(loot);
        }
    }

    public int TakeAll(out LootEntity[] allLoot) {
        if (all.Count > temp.Length) {
            temp = new LootEntity[(int)(all.Count * 1.5f)];
        }
        all.Values.CopyTo(temp, 0);
        allLoot = temp;
        return all.Count;
    }
}