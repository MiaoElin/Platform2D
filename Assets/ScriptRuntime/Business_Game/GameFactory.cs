using UnityEngine;

public static class GameFactory {

    public static RoleEntity Role_Create(GameContext ctx) {
        ctx.asset.TryGet_Entity_Prefab(typeof(RoleEntity).Name, out var prefab);
        var role = GameObject.Instantiate(prefab, ctx.poolService.roleGroup).GetComponent<RoleEntity>();
        role.gameObject.SetActive(false);
        return role;
    }

    public static RoleEntity Role_Spawn(GameContext ctx, int typeID, Vector2 pos, Ally ally) {
        ctx.asset.TryGet_RoleTM(typeID, out var tm);
        if (!tm) {
            Debug.LogError($"GameFactory.Role_Spawn {typeID} is not find");
        }
        var role = ctx.poolService.GetRole();
        role.typeID = typeID;
        role.id = ctx.iDService.roleIDRecord++;
        role.SetPos(pos);
        role.ally = ally;
        role.moveSpeed = tm.moveSpeed;
        role.moveType = tm.moveType;
        role.Ctor(tm.mod);
        role.gameObject.SetActive(true);
        return role;
    }
}