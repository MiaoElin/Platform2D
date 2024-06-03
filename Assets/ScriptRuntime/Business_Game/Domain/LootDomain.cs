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

}