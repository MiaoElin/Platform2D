using UnityEngine;
using System.Collections.Generic;
using System;

public class PoolService {
    public Pool<RoleEntity> rolePool;
    public Transform roleGroup;

    public Pool<PropEntity> propPool;
    public Transform propGroup;

    public void Init(Func<RoleEntity> create_Role, Func<PropEntity> create_Prop) {
        roleGroup = new GameObject("RoleGroup").transform;
        rolePool = new Pool<RoleEntity>(create_Role, 20);

        propGroup = new GameObject("PropGroup").transform;
        propPool = new Pool<PropEntity>(create_Prop, 20);
    }

    public RoleEntity GetRole() {
        return rolePool.Get();
    }

    public void ReturnRole(RoleEntity role){
        rolePool.Return(role);
    }

    public PropEntity GetProp() {
        return propPool.Get();
    }

    public void ReturnProp(PropEntity prop) {
        propPool.Return(prop);
    }
}