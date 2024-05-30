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
        role.height = tm.height;
        role.moveType = tm.moveType;
        role.Ctor(tm.mod);
        role.gravity = tm.gravity;
        role.jumpForce = tm.jumpForce;
        role.jumpTimes = tm.jumpTimesMax;
        role.jumpTimesMax = tm.jumpTimesMax;
        role.gameObject.SetActive(true);
        return role;
    }

    public static MapEntity Map_Create(GameContext ctx, int stageID) {
        ctx.asset.TryGet_MapTM(stageID, out var tm);
        if (!tm) {
            Debug.LogError($"GameFactory.Map_Create {stageID} is not find");
        }
        ctx.asset.TryGet_Entity_Prefab(typeof(MapEntity).Name, out var prefab);
        MapEntity map = GameObject.Instantiate(prefab).GetComponent<MapEntity>();
        Grid grid = GameObject.Instantiate(tm.grid, map.transform);
        map.Ctor(stageID, grid);
        return map;
    }

    public static BackSceneEntity BackScene_Create(GameContext ctx) {
        ctx.asset.TryGet_Entity_Prefab(typeof(BackSceneEntity).Name, out var prefab);
        BackSceneEntity backScene = GameObject.Instantiate(prefab).GetComponent<BackSceneEntity>();
        return backScene;
    }
}