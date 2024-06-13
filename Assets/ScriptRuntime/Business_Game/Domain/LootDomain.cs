using System;
using UnityEngine;

public static class LootDomain {

    public static LootEntity Spawn(GameContext ctx, int typeID, Vector2 pos, Vector3 rotation, Vector3 localScale) {
        var loot = GameFactory.Loot_Spawn(ctx, typeID, pos, rotation, localScale);
        ctx.lootRepo.Add(loot);
        loot.fsm.EnterNormal();
        return loot;
    }

    public static void UnSpawn(GameContext ctx, LootEntity loot) {
        ctx.lootRepo.Remove(loot);
        loot.Reuse();
        loot.gameObject.SetActive(false);
        ctx.poolService.ReturnLoot(loot);
    }

    public static void Move(GameContext ctx, LootEntity loot) {
        loot.Move_To(ctx.GetOwner().Pos());
    }

    internal static void SpawnCoin(GameContext ctx, int typeID, Vector2 pos) {
        Vector2 dir = Vector2.zero;
        dir.x = UnityEngine.Random.Range(-1f, 1f);
        dir.y = UnityEngine.Random.Range(0, 1f);
        var coin = LootDomain.Spawn(ctx, typeID, pos, Vector3.zero, Vector3.one);
        coin.RbAddForce_ByDir(dir);
    }

    public static void CheckGround(GameContext ctx, LootEntity loot) {
        if (!loot.isCoin) {
            return;
        }

        LayerMask map = 1 << 3;

        // Collider2D hitwall = Physics2D.OverlapCircle(loot.Pos(), 0.36f, map);
        // if (hitwall) {
        //     loot
        // }

        if (loot.GetVelocityY() < 0) {
            RaycastHit2D hitGround = Physics2D.Raycast(loot.GetFootPos(), Vector2.down, 0.1f, map);
            if (hitGround) {
                loot.SetVelocityZero();
                loot.fsm.EnterMoveToOwner();
            }
        }



    }
}