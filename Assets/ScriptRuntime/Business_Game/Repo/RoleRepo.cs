using UnityEngine;
using System.Collections.Generic;
using System;

public class RoleRepo {

    Dictionary<int, RoleEntity> all;
    RoleEntity[] temp;

    public RoleRepo() {
        all = new Dictionary<int, RoleEntity>();
        temp = new RoleEntity[128];
    }

    public void Add(RoleEntity role) {
        all.Add(role.id, role);
    }

    public bool TryGet(int typeID, out RoleEntity role) {
        return all.TryGetValue(typeID, out role);
    }

    public void Foreach(Action<RoleEntity> action) {
        for (int i = 0; i < all.Count; i++) {
            var role = all[i];
            action.Invoke(role);
        }
    }
}