using UnityEngine;
using System.Collections.Generic;

public class PropRepo {
    Dictionary<int, PropEntity> all;

    public PropRepo() {
        all = new Dictionary<int, PropEntity>();
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
}