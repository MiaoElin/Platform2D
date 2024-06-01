using UnityEngine;
using System.Collections.Generic;
using System;

public class LootRepo {

    Dictionary<int, LootEntity> all;

    public LootRepo() {
        all = new Dictionary<int, LootEntity>();
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
}