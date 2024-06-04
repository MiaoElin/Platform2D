using UnityEngine;
using System.Collections.Generic;
using System;

public class BulletRepo {

    public Dictionary<int, BulletEntity> all;
    BulletEntity[] temp;
    public BulletRepo() {
        all = new Dictionary<int, BulletEntity>();
        temp = new BulletEntity[128];
    }

    public void Add(BulletEntity bullet) {
        all.Add(bullet.id, bullet);
    }

    public bool TryGet(int typeID, out BulletEntity bullet) {
        return all.TryGetValue(typeID, out bullet);
    }

    public void Remove(BulletEntity bullet) {
        all.Remove(bullet.id);
    }

    public void Foreach(Action<BulletEntity> action) {
        foreach (var bullet in all.Values) {
            action(bullet);
        }
    }
    public int TakeAll(out BulletEntity[] allBullet) {
        if (all.Count > temp.Length) {
            temp = new BulletEntity[(int)(all.Count * 1.5f)];
        }
        all.Values.CopyTo(temp, 0);
        allBullet = temp;
        return all.Count;
    }
}