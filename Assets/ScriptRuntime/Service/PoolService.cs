using UnityEngine;
using System.Collections.Generic;
using System;

public class PoolService {
    public Pool<RoleEntity> rolePool;
    public Transform roleGroup;

    public Pool<PropEntity> propPool;
    public Transform propGroup;

    public Pool<LootEntity> lootPool;
    public Transform lootGroup;

    public void Init(Func<RoleEntity> create_Role, Func<PropEntity> create_Prop, Func<LootEntity> create_Loot) {
        roleGroup = new GameObject("RoleGroup").transform;
        rolePool = new Pool<RoleEntity>(create_Role, 20);

        propGroup = new GameObject("PropGroup").transform;
        propPool = new Pool<PropEntity>(create_Prop, 20);

        lootGroup = new GameObject("LootGroup").transform;
        lootPool = new Pool<LootEntity>(create_Loot, 20);
    }

    public RoleEntity GetRole() {
        return rolePool.Get();
    }

    public void ReturnRole(RoleEntity role) {
        rolePool.Return(role);
    }

    public PropEntity GetProp() {
        return propPool.Get();
    }

    public void ReturnProp(PropEntity prop) {
        propPool.Return(prop);
    }

    public LootEntity GetLoot() {
        return lootPool.Get();
    }

    public void ReturnLoot(LootEntity loot) {
        lootPool.Return(loot);
    }
}