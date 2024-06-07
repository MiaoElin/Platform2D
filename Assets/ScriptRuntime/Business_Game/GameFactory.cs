using System;
using UnityEngine;

public static class GameFactory {

    public static RoleEntity Role_Create(GameContext ctx) {
        ctx.asset.TryGet_Entity_Prefab(typeof(RoleEntity).Name, out var prefab);
        var role = GameObject.Instantiate(prefab, ctx.poolService.roleGroup).GetComponent<RoleEntity>();
        role.gameObject.SetActive(false);
        return role;
    }

    public static RoleEntity Role_Spawn(GameContext ctx, int typeID, Vector2 pos, Ally ally, Vector2[] path) {
        ctx.asset.TryGet_RoleTM(typeID, out var tm);
        if (!tm) {
            Debug.LogError($"GameFactory.Role_Spawn {typeID} is not find");
        }
        var role = ctx.poolService.GetRole();
        role.typeID = typeID;
        role.id = ctx.iDService.roleIDRecord++;
        role.SetPos(pos);
        role.ally = ally;
        role.path = path;
        role.hp = tm.hpMax;
        role.hpMax = tm.hpMax;
        role.regenerationTimer = tm.regenerationDuration;
        role.regenerationDuration = tm.regenerationDuration;
        role.regenerationHpMax = tm.regenerationHpMax;
        role.moveSpeed = tm.moveSpeed;
        role.height = tm.height;
        role.attackRange = tm.attackRange;
        role.aiType = tm.aiType;
        role.Ctor(tm.mod);
        role.gravity = tm.gravity;
        role.jumpForce = tm.jumpForce;
        role.jumpTimes = tm.jumpTimesMax;
        role.jumpTimesMax = tm.jumpTimesMax;

        var skillTMs = tm.sKillTMs;
        if (skillTMs.Length > 0) {
            for (int i = 0; i < skillTMs.Length; i++) {
                SkillTM skilltm = skillTMs[i];
                var skill = Skill_Spawn(ctx, skilltm.typeID);
                role.skillCom.Add(i, skill);
            }
        }

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
        map.roleSpawnerTMs = tm.roleSpawnerTMs;
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
        loot.Ctor(tm.mod);
        loot.SetPos(pos);
        loot.SetRotation(rotation);
        loot.SetLocalScale(localScale);
        loot.price = tm.price;
        loot.isDead = false;

        loot.isGetCoin = tm.isGetCoin;
        loot.coinCount = tm.coinCount;

        loot.needHints = tm.needHints;

        loot.isDropLoot = tm.isDropLoot;
        loot.fsm.easingInduration = tm.easingInduration;

        loot.isGetBuff = tm.isGetBuff;
        loot.buffTypeId = tm.buffTypeId;

        loot.isGetStuff = tm.isGetStuff;

        loot.gameObject.SetActive(true);
        return loot;
    }

    public static BuffSubEntity Buff_Spawn(GameContext ctx, int typeID) {
        ctx.asset.TryGet_BuffTM(typeID, out var tm);
        if (!tm) {
            Debug.LogError($"GameFactory.Buff_Spawn {typeID} is not find");
        }

        BuffSubEntity buff = ctx.poolService.GetBuff();
        buff.typeID = typeID;
        buff.id = ctx.iDService.buffIDRecord++;
        buff.icon = tm.icon;
        buff.isPermanent = tm.isPermanent;

        buff.isGetShield = tm.isGetShield;
        buff.shieldValue = tm.shieldValue;
        buff.shieldPersent = tm.shieldPersent;
        buff.shieldCDMax = tm.shieldCDMax;
        buff.shieldDuration = tm.shieldDuration;
        buff.shieldTimer = 0;

        buff.isAddHp = tm.isAddHp;
        buff.addHpMax = tm.addHpMax;
        buff.regenerationHpMax = tm.regenerationHpMax;

        return buff;
    }

    public static SkillSubEntity Skill_Spawn(GameContext ctx, int typeID) {
        ctx.asset.TryGet_SkillTM(typeID, out var tm);
        if (!tm) {
            Debug.LogError($"GameFactory.Skill_Spawn {typeID} is not find");
        }
        SkillSubEntity skill = new SkillSubEntity();
        skill.typeID = tm.typeID;
        skill.damageRate = tm.damageRate;
        skill.bulletTypeID = tm.bulletTypeID;

        skill.cd = tm.cdMax;
        skill.cdMax = tm.cdMax;
        skill.preCastCDMax = tm.preCastCDMax;
        skill.castingMaintainSec = tm.castingMaintainSec;
        skill.castingIntervalSec = tm.castingIntervalSec;
        skill.endCastSec = tm.endCastSec;

        return skill;
    }

    public static BulletEntity Bullet_Create(GameContext ctx) {
        ctx.asset.TryGet_Entity_Prefab(typeof(BulletEntity).Name, out var prefab);
        BulletEntity bullet = GameObject.Instantiate(prefab, ctx.poolService.bulletGroup).GetComponent<BulletEntity>();
        bullet.gameObject.SetActive(false);
        return bullet;
    }

    public static BulletEntity Bullet_Spawn(GameContext ctx, int typeID, Vector2 pos, Ally ally, float damgeRate) {
        ctx.asset.TryGet_BulletTM(typeID, out var tm);
        if (!tm) {
            Debug.LogError($"GameFactory.Bullet_Spawn {typeID} is not find");
        }
        var bullet = ctx.poolService.GetBullet();
        bullet.typeID = typeID;
        bullet.ally = ally;
        bullet.id = ctx.iDService.bulletIDRecord++;
        bullet.Ctor(tm.mod, tm.moveSpeed);
        bullet.SetPos(pos);
        bullet.damgage = damgeRate * CommonConst.BASEDAMAGE;
        bullet.gameObject.SetActive(true);
        return bullet;
    }
}