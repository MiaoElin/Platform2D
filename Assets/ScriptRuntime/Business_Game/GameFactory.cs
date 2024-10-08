using System;
using UnityEngine;
using System.Collections.Generic;

public static class GameFactory {

    public static RoleEntity Role_Create(GameContext ctx) {
        ctx.asset.TryGet_Entity_Prefab(typeof(RoleEntity).Name, out var prefab);
        var role = GameObject.Instantiate(prefab, ctx.poolService.roleGroup).GetComponent<RoleEntity>();
        role.gameObject.SetActive(false);
        return role;
    }

    public static RoleEntity Role_Spawn(GameContext ctx, int typeID, Vector2 pos, Vector3 rotation, Ally ally, List<Vector2Int> path) {
        ctx.asset.TryGet_RoleTM(typeID, out var tm);
        if (!tm) {
            Debug.LogError($"GameFactory.Role_Spawn {typeID} is not find");
        }
        var role = ctx.poolService.GetRole();
        role.typeID = typeID;
        role.id = ctx.iDService.roleIDRecord++;
        role.SetPos(pos);
        role.SetRotation(rotation);
        role.ally = ally;
        role.isBoss = tm.isBoss;
        role.price = tm.price;
        role.hp = tm.hpMax;
        role.hpMax = tm.hpMax;
        role.regenerationTimer = tm.regenerationDuration;
        role.regenerationDuration = tm.regenerationDuration;
        role.regenerationHpMax = tm.regenerationHpMax;
        role.moveSpeed = tm.moveSpeed;
        role.height = tm.height;
        role.searchRange = tm.searchRange;
        role.aiType = tm.aiType;
        role.Ctor(tm.mod, path);
        role.gravity = tm.gravity;
        role.jumpForce = tm.jumpForce;
        role.jumpTimes = tm.jumpTimesMax;
        role.jumpTimesMax = tm.jumpTimesMax;

        // suffering
        role.fsm.antiStiffenType = tm.antiStiffenType;

        // destroy
        role.die_Sfx = tm.die_Sfx;
        role.dieVolume = tm.dieVolume;
        role.deathTimer = tm.deathTimer;

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
        map.Ctor(stageID, grid, tm.xCount, tm.yCount);
        map.backSceneBG = tm.backSceneBG;
        map.backSceneMid = tm.backSceneMid;
        map.backSceneFront = tm.backSceneFront;
        map.propSpawnerTMs = tm.propSpawnerTMs;
        map.lootSpawnerTMs = tm.lootSpawnerTMs;
        map.roleSpawnerTMs = tm.roleSpawnerTMs;
        map.bgm = tm.bgm;
        map.bgmVolume = tm.bgmVolume;

        map.xCount = tm.xCount;
        map.yCount = tm.yCount;

        map.ownerEnterPos = tm.ownerEnterPos;
        map.bossWaveDuration = tm.bossWaveDuration;

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

    public static PropEntity Prop_Spawn(GameContext ctx, int typeID, Vector2 pos, Vector3 rotaion, Vector3 localScale, bool isModifySize, Vector2 sizeScale, float jumpForce, Ally ally) {
        ctx.asset.TryGet_PropTM(typeID, out var tm);
        if (!tm) {
            Debug.LogError($"GameFactory.Prop_Spawn {typeID} is not find");
        }
        PropEntity prop = ctx.poolService.GetProp();
        prop.typeID = typeID;
        prop.id = ctx.iDService.propIDRecord++;
        prop.ally = ally;
        prop.moveSpeed = tm.moveSpeed;
        prop.isPermanent = tm.isPermanent;
        prop.activeTimer = tm.activeTimer;
        prop.jumpForce = jumpForce;
        prop.Ctor();
        prop.SetPos(pos);
        prop.SetRotation(rotaion);
        prop.SetScale(localScale);
        prop.SetMesh(tm.mod, tm.hasAnim);
        // prop.srBaseSize = tm.srBaseSize;
        prop.fsm.fadeOutTimer = tm.fadeOutTimer;

        // 梯子
        prop.isLadder = tm.isLadder;
        // 祭坛
        prop.isAltar = tm.isAltar;
        prop.altarDuration = tm.altarDuration;
        // 蹦床
        prop.isTrampoline = tm.isTrampoline;
        prop.anim_BePress = tm.anim_Normal;
        // hurtFire
        prop.isHurtFire = tm.isHurtFire;
        prop.hurtFireDamageRate = tm.hurtFireDamageRate;
        prop.hurtFireDuration = tm.hurtFireDuration;
        prop.hurtFireTimer = 0;
        // Thron
        prop.isThron = tm.isThron;
        prop.thronDamageRate = tm.thronDamageRate;
        prop.thronDuration = tm.thronDuration;
        prop.thronTimer = 0;

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
        // loot.isDead = false;

        loot.needHints = tm.needHints;

        loot.isDropLoot = tm.isDropLoot;
        loot.fsm.easingInduration = tm.easingInduration;

        loot.isGetCoin = tm.isGetCoin;
        loot.coinCount = tm.coinCount;
        loot.coinTypeID = tm.coinTypeID;

        loot.isGetTips = tm.isTips;

        loot.isCoin = tm.isCoin;
        loot.moveSpeed = tm.moveSpeed;
        loot.gravity = tm.gravity;
        loot.centrifugalForce = tm.centrifugalForce;

        loot.isGetBuff = tm.isGetBuff;
        loot.buffTypeId = tm.buffTypeId;

        loot.isGetStuff = tm.isGetStuff;

        loot.isGetRole = tm.isGetRole;
        loot.roleTypeID = tm.roleTypeID;

        loot.getLootClip = tm.getLootClip;
        loot.volume = tm.volume;

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
        buff.shieldCD = 0;
        buff.shieldDuration = tm.shieldDuration;
        buff.shieldTimer = tm.shieldDuration;

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
        skill.attackRange = tm.attackRange;

        // skill.bulletDamageRate = tm.damageRate;
        skill.isCastBullet = tm.isCastBullet;
        skill.bulletTypeID = tm.bulletTypeID;

        skill.isCastProp = tm.isCastProp;
        skill.propTypeID = tm.propTypeID;

        skill.isFlash = tm.isFlash;

        skill.isCure = tm.isCure;
        skill.addHpMax = tm.addHpMax;

        skill.isMelee = tm.isMelee;
        skill.meleeDamageRate = tm.meleeDamageRate;

        skill.cd = tm.cdMax;
        skill.cdMax = tm.cdMax;
        skill.preCastCDMax = tm.preCastCDMax;
        skill.castingMaintainSec = tm.castingMaintainSec;
        skill.castingIntervalSec = tm.castingIntervalSec;
        skill.endCastSec = tm.endCastSec;
        skill.castSfx = tm.castSfx;
        skill.volume = tm.volume;

        skill.stiffenSec = tm.stiffenSec;

        return skill;
    }

    public static BulletEntity Bullet_Create(GameContext ctx) {
        ctx.asset.TryGet_Entity_Prefab(typeof(BulletEntity).Name, out var prefab);
        BulletEntity bullet = GameObject.Instantiate(prefab, ctx.poolService.bulletGroup).GetComponent<BulletEntity>();
        bullet.gameObject.SetActive(false);
        return bullet;
    }

    public static BulletEntity Bullet_Spawn(GameContext ctx, int typeID, Vector2 pos, Ally ally, float stiffenSec) {
        ctx.asset.TryGet_BulletTM(typeID, out var tm);
        if (!tm) {
            Debug.LogError($"GameFactory.Bullet_Spawn {typeID} is not find");
        }
        var bullet = ctx.poolService.GetBullet();
        bullet.typeID = typeID;
        bullet.ally = ally;
        bullet.stiffenSec = stiffenSec;
        bullet.id = ctx.iDService.bulletIDRecord++;
        bullet.Ctor(tm.mod, tm.moveSpeed);
        bullet.SetPos(pos);
        bullet.damgage = tm.damageRate * CommonConst.BASEDAMAGE;
        bullet.gameObject.SetActive(true);
        bullet.moveType = tm.moveType;
        return bullet;
    }

    public static VFXModel VFX_Spawn(GameContext ctx, Sprite[] sprites, Vector2 pos) {
        ctx.asset.TryGet_Entity_Prefab(typeof(VFXModel).Name, out var prefab);
        VFXModel vfx = GameObject.Instantiate(prefab, ctx.vfxGroup).GetComponent<VFXModel>();
        vfx.Init(sprites);
        vfx.SetPos(pos);
        return vfx;
    }
}