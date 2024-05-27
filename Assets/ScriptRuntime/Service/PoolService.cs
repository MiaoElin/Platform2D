using UnityEngine;
using System.Collections.Generic;
using System;

public class PoolService {
    public Pool<RoleEntity> rolePool;
    public Transform roleGroup;

    public void Init(Func<RoleEntity> create_Role) {
        roleGroup = new GameObject("RoleGroup").transform;
        rolePool = new Pool<RoleEntity>(create_Role, 20);
    }

    public RoleEntity GetRole() {
        return rolePool.Get();
    }
}