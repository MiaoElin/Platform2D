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

    public Pool<BuffSubEntity> buffPool;

    public Pool<BulletEntity> bulletPool;
    public Transform bulletGroup;

    public void Init(Func<RoleEntity> create_Role, Func<PropEntity> create_Prop, Func<LootEntity> create_Loot, Func<BuffSubEntity> create_buff, Func<BulletEntity> create_bullet) {
        roleGroup = new GameObject("RoleGroup").transform;
        rolePool = new Pool<RoleEntity>(create_Role, 20);

        propGroup = new GameObject("PropGroup").transform;
        propPool = new Pool<PropEntity>(create_Prop, 20);

        lootGroup = new GameObject("LootGroup").transform;
        lootPool = new Pool<LootEntity>(create_Loot, 20);

        buffPool = new Pool<BuffSubEntity>(create_buff, 20);

        bulletGroup = new GameObject("BulletGroup").transform;
        bulletPool = new Pool<BulletEntity>(create_bullet, 20);

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

    public BuffSubEntity GetBuff() {
        return buffPool.Get();
    }

    public void ReturnBuff(BuffSubEntity buff) {
        buffPool.Return(buff);
    }

    public BulletEntity GetBullet() {
        return bulletPool.Get();
    }

    public void ReturnBullet(BulletEntity bullet) {
        bulletPool.Return(bullet);
    }
}