using System;
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
        map.backSceneBG = tm.backSceneBG;
        map.backSceneMid = tm.backSceneMid;
        map.backSceneFront = tm.backSceneFront;
        map.propSpawnerTMs = tm.propSpawnerTMs;
        map.lootSpawnerTMs = tm.lootSpawnerTMs;
        return map;
    }

    public static BackSceneEntity BackScene_Create(GameContext ctx) {
        ctx.asset.TryGet_Entity_Prefab(typeof(BackSceneEntity).Name, out var prefab);
        BackSceneEntity backScene = GameObject.Instantiate(prefab).GetComponent<BackSceneEntity>();
        return backScene;
    }

    public static PropEntity Prop_Create(GameContext ctx) {
        ctx.asset.TryGet_Entity_Prefab(typeof(PropEntity).Name, out var prefab);
        PropEntity prop = GameObject.Instantiate(prefab, ctx.poolService.propGroup).GetComponent<PropEntity>();
        prop.gameObject.SetActive(false);
        return prop;
    }

    public static PropEntity Prop_Spawn(GameContext ctx, int typeID, Vector2 pos, Vector3 rotaion, Vector3 localScale, bool isModifySize, Vector2 sizeScale, float jumpForce) {
        ctx.asset.TryGet_PropTM(typeID, out var tm);
        if (!tm) {
            Debug.LogError($"GameFactory.Prop_Spawn {typeID} is not find");
        }
        PropEntity prop = ctx.poolService.GetProp();
        prop.typeID = typeID;
        prop.id = ctx.iDService.propIDRecord++;
        prop.jumpForce = jumpForce;
        prop.Ctor();
        prop.SetPos(pos);
        prop.SetRotation(rotaion);
        prop.SetScale(localScale);
        prop.SetMesh(tm.mod);
        prop.size = tm.size;

        prop.isLadder = tm.isLadder;

        prop.isAltar = tm.isAltar;

        prop.isTrampoline = tm.isTrampoline;
        prop.anim_BePress = tm.anim_Normal;
        // prop.OnPressTrampolineHandle = (float jumpForce) => {
        //     // TO do
        //     ctx.GetOwner().SetVelocityY(jumpForce);
        // };

        prop.SetCollider(tm.colliderType, isModifySize, sizeScale);
        prop.gameObject.SetActive(true);
        return prop;
    }

    public static LootEntity Loot_Create(GameContext ctx) {
        ctx.asset.TryGet_Entity_Prefab(typeof(LootEntity).Name, out var prefab);
        LootEntity loot = GameObject.Instantiate(prefab, ctx.poolService.lootGroup).GetComponent<LootEntity>();
        loot.gameObject.SetActive(false);
        return loot;
    }


    internal static LootEntity Loot_Spawn(GameContext ctx, int typeID, Vector2 pos, Vector3 rotation, Vector3 localScale) {
        ctx.asset.TryGet_LootTM(typeID, out var tm);
        if (!tm) {
            Debug.LogError($"GameFactory.Loot_Spawn {typeID} is not find");
        }
        LootEntity loot = ctx.poolService.GetLoot();
        loot.typeID = typeID;
        loot.id = ctx.iDService.lootIDRecord++;
        loot.isDropLoot = tm.isDropLoot;
        loot.needHints = tm.needHints;
        loot.Ctor(tm.mod);
        loot.SetPos(pos);
        loot.SetRotation(rotation);
        loot.SetLocalScale(localScale);
        loot.gameObject.SetActive(true);
        return loot;
    }


}