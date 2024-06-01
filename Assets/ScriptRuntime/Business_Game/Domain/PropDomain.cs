using UnityEngine;

public static class PropDomain {

    public static PropEntity Spawn(GameContext ctx, int typeID, Vector2 pos, Vector3 rotaion, Vector3 localScale, bool isModifySize, Vector2 sizeScale, float jumpForce) {
        var prop = GameFactory.Prop_Spawn(ctx, typeID, pos, rotaion, localScale, isModifySize, sizeScale, jumpForce);
        ctx.propRepo.Add(prop);
        prop.fsm.EnterNormal();
        return prop;
    }

}