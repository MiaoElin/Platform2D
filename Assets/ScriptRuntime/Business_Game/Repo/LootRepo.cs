using UnityEngine;
using System.Collections.Generic;

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
}