using System;
using UnityEngine;

public static class PropDomain {

    public static PropEntity Spawn(GameContext ctx, int typeID, Vector2 pos, Vector3 rotaion, Vector3 localScale, bool isModifySize, Vector2 sizeScale, float jumpForce, Ally ally) {
        var prop = GameFactory.Prop_Spawn(ctx, typeID, pos, rotaion, localScale, isModifySize, sizeScale, jumpForce, ally);
        ctx.propRepo.Add(prop);
        prop.fsm.EnterNormal();
        return prop;
    }

    public static void Move(PropEntity prop, float dt) {
        prop.Move(dt);
    }

    internal static void UnSpawn(GameContext ctx, PropEntity prop) {
        ctx.propRepo.Remove(prop);
        prop.Reuse();
        prop.gameObject.SetActive(false);
        ctx.poolService.ReturnProp(prop);
    }

}