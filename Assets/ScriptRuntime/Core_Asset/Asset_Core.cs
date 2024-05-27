using UnityEngine;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;

public class Asset_Core {

    Dictionary<string, GameObject> entites;
    AsyncOperationHandle entityPtr;

    Dictionary<int, RoleTM> roleTMs;
    AsyncOperationHandle roleTMPtr;

    public Asset_Core() {
        entites = new Dictionary<string, GameObject>();
        roleTMs = new Dictionary<int, RoleTM>();
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
            var ptr = Addressables.LoadAssetsAsync<RoleTM>("TM_Role", null);
            roleTMPtr = ptr;
            var list = ptr.WaitForCompletion();
            foreach (var tm in list) {
                roleTMs.Add(tm.typeID, tm);
            }
        }
    }

    public void Unload() {
        Release(entityPtr);
        Release(roleTMPtr);
    }

    public void Release(AsyncOperationHandle ptr) {
        if (ptr.IsValid()) {
            Addressables.Release(ptr);
        }
    }

    public bool TryGet_Entity_Prefab(string name, out GameObject prefab) {
        return entites.TryGetValue(name, out prefab);
    }

    public bool TryGet_RoleTM(int typeID, out RoleTM tm) {
        return roleTMs.TryGetValue(typeID, out tm);
    }
}