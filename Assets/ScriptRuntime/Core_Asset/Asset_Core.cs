using UnityEngine;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;

public class AssetCore {

    Dictionary<string, GameObject> entites;
    AsyncOperationHandle entityPtr;

    Dictionary<string, GameObject> uiPrefabs;
    AsyncOperationHandle uiPtr;

    Dictionary<int, RoleTM> roleTMs;
    AsyncOperationHandle roleTMPtr;

    Dictionary<int, MapTM> mapTMs;
    AsyncOperationHandle mapTMPtr;

    Dictionary<int, PropTM> propTMs;
    AsyncOperationHandle propTMPtr;

    Dictionary<int, LootTM> lootTMs;
    AsyncOperationHandle lootTMPtr;

    Dictionary<int, BuffTM> buffTMs;
    AsyncOperationHandle buffTMPtr;

    Dictionary<int, SkillTM> skillTMs;
    AsyncOperationHandle skillPtr;

    Dictionary<int, BulletTM> bulletTMs;
    AsyncOperationHandle bulletPtr;

    public ConfigTM configTM;
    AsyncOperationHandle configTMPtr;

    public AssetCore() {
        entites = new Dictionary<string, GameObject>();
        uiPrefabs = new Dictionary<string, GameObject>();
        roleTMs = new Dictionary<int, RoleTM>();
        mapTMs = new Dictionary<int, MapTM>();
        propTMs = new Dictionary<int, PropTM>();
        lootTMs = new Dictionary<int, LootTM>();
        buffTMs = new Dictionary<int, BuffTM>();
        skillTMs = new Dictionary<int, SkillTM>();
        bulletTMs = new Dictionary<int, BulletTM>();
    }

    public void LoadAll() {
        {
            var ptr = Addressables.LoadAssetsAsync<GameObject>("Entity", null);
            entityPtr = ptr;
            var list = ptr.WaitForCompletion();
            foreach (var prefab in list) {
                entites.Add(prefab.name, prefab);
            }
        }
        {
            var ptr = Addressables.LoadAssetsAsync<GameObject>("UI", null);
            uiPtr = ptr;
            var list = ptr.WaitForCompletion();
            foreach (var prefab in list) {
                uiPrefabs.Add(prefab.name, prefab);
            }
        }
        {
            var ptr = Addressables.LoadAssetsAsync<RoleTM>("TM_Role", null);
            roleTMPtr = ptr;
            var list = ptr.WaitForCompletion();
            foreach (var tm in list) {
                roleTMs.Add(tm.typeID, tm);
            }
        }
        {
            var ptr = Addressables.LoadAssetsAsync<MapTM>("TM_Map", null);
            mapTMPtr = ptr;
            var list = ptr.WaitForCompletion();
            foreach (var tm in list) {
                mapTMs.Add(tm.stage, tm);
            }
        }
        {
            var ptr = Addressables.LoadAssetsAsync<PropTM>("TM_Prop", null);
            propTMPtr = ptr;
            var list = ptr.WaitForCompletion();
            foreach (var tm in list) {
                propTMs.Add(tm.typeID, tm);
            }
        }
        {
            var ptr = Addressables.LoadAssetsAsync<LootTM>("TM_Loot", null);
            lootTMPtr = ptr;
            var list = ptr.WaitForCompletion();
            foreach (var tm in list) {
                lootTMs.Add(tm.typeID, tm);
            }
        }
        {
            var ptr = Addressables.LoadAssetsAsync<BuffTM>("TM_Buff", null);
            buffTMPtr = ptr;
            var list = ptr.WaitForCompletion();
            foreach (var tm in list) {
                buffTMs.Add(tm.typeID, tm);
            }
        }
        {
            var ptr = Addressables.LoadAssetsAsync<SkillTM>("TM_Skill", null);
            skillPtr = ptr;
            var list = ptr.WaitForCompletion();
            foreach (var tm in list) {
                skillTMs.Add(tm.typeID, tm);
            }
        }
        {
            var ptr = Addressables.LoadAssetsAsync<BulletTM>("TM_Bullet", null);
            bulletPtr = ptr;
            var list = ptr.WaitForCompletion();
            foreach (var tm in list) {
                bulletTMs.Add(tm.typeID, tm);
            }
        }
        {
            var ptr = Addressables.LoadAssetAsync<ConfigTM>("TM_ConfigTM");
            configTMPtr = ptr;
            configTM = ptr.WaitForCompletion();
        }
    }

    public void Unload() {
        Release(entityPtr);
        Release(uiPtr);
        Release(roleTMPtr);
        Release(mapTMPtr);
        Release(propTMPtr);
        Release(lootTMPtr);
        Release(buffTMPtr);
        Release(skillPtr);
        Release(bulletPtr);
    }

    public void Release(AsyncOperationHandle ptr) {
        if (ptr.IsValid()) {
            Addressables.Release(ptr);
        }
    }

    public bool TryGet_Entity_Prefab(string name, out GameObject prefab) {
        return entites.TryGetValue(name, out prefab);
    }

    public bool TryGet_UI_Prefab(string name, out GameObject prefab) {
        return uiPrefabs.TryGetValue(name, out prefab);
    }

    public bool TryGet_RoleTM(int typeID, out RoleTM tm) {
        return roleTMs.TryGetValue(typeID, out tm);
    }

    public bool TryGetRobotTMArray(out List<RoleTM> allRobot) {
        if (roleTMs == null) {
            allRobot = null;
            return false;
        }
        allRobot = new List<RoleTM>();
        foreach (var val in roleTMs) {
            if (val.Value.isRobot) {
                allRobot.Add(val.Value);
            }
        }
        return true;
    }

    public bool TryGet_MapTM(int stage, out MapTM tm) {
        return mapTMs.TryGetValue(stage, out tm);
    }

    public bool TryGet_PropTM(int typeID, out PropTM tm) {
        return propTMs.TryGetValue(typeID, out tm);
    }

    public bool TryGet_LootTM(int typeID, out LootTM tm) {
        return lootTMs.TryGetValue(typeID, out tm);
    }

    public bool TryGetLootTMArray(out List<LootTM> allLoot) {
        if (buffTMs == null) {
            allLoot = null;
            return false;
        }
        allLoot = new List<LootTM>();
        foreach (var loot in lootTMs) {
            if (loot.Value.needHints) {
                continue;
            }
            if (loot.Value.isCoin) {
                continue;
            }
            allLoot.Add(loot.Value);
        }
        return true;
    }

    public bool TryGet_BuffTM(int typeID, out BuffTM tm) {
        return buffTMs.TryGetValue(typeID, out tm);
    }

    public bool TryGet_SkillTM(int typeID, out SkillTM tm) {
        return skillTMs.TryGetValue(typeID, out tm);
    }

    public bool TryGet_BulletTM(int typeID, out BulletTM tm) {
        return bulletTMs.TryGetValue(typeID, out tm);
    }

}

