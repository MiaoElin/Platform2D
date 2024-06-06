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

    public void Remove(RoleEntity role) {
        all.Remove(role.id);
    }

    public void Foreach(Action<RoleEntity> action) {
        foreach (var dic in all) {
            action.Invoke(dic.Value);
        }
        // for (int i = 0; i < all.Count; i++) {
        //     var role = all[i];
        //     action.Invoke(role);
        // }
    }

    public int TakeAll(out RoleEntity[] allRole) {
        if (all.Count > temp.Length) {
            temp = new RoleEntity[(int)(all.Count * 1.5f)];
        }
        all.Values.CopyTo(temp, 0);
        allRole = temp;
        return all.Count;
    }
}