using UnityEditor;
using UnityEngine;

public class MapEM : MonoBehaviour {

    public MapTM tm;

    [ContextMenu("Save")]
    public void Save() {
        // 将roleEM[]存给RoleSpawnerTM[]
        // propEM[] 给 propSpawnerTM[]
        {
            PropEM[] propEMs = gameObject.GetComponentsInChildren<PropEM>();
            if (propEMs != null) {
                tm.propSpawnerTMs = new propSpawnerTM[propEMs.Length];
                for (int i = 0; i < propEMs.Length; i++) {
                    var em = propEMs[i];
                    propSpawnerTM propSpawnerTM = new propSpawnerTM() {
                        pos = em.transform.position,
                        rotation = em.transform.eulerAngles,
                        localScale = em.transform.localScale,
                        propTypeID = em.tm.typeID,
                        isModifySize = em.isModifySize,
                        sizeScale = em.sizeScalse,
                        jumpForce = em.jumpForce
                    };
                    tm.propSpawnerTMs[i] = propSpawnerTM;
                }
            }
        }

        {
            LootEM[] lootEMs = gameObject.GetComponentsInChildren<LootEM>();
            if (lootEMs.Length != 0) {
                tm.lootSpawnerTMs = new LootSpawnerTM[lootEMs.Length];
                for (int i = 0; i < lootEMs.Length; i++) {
                    var em = lootEMs[i];
                    LootSpawnerTM lootSpawnerTM = new LootSpawnerTM() {
                        lootTypeID = em.tm.typeID,
                        pos = em.transform.position,
                        rotation = em.transform.eulerAngles,
                        localScale = em.transform.localScale
                    };
                    tm.lootSpawnerTMs[i] = lootSpawnerTM;
                }
            }
        }

        {
            RoleEM[] roleEMs = gameObject.GetComponentsInChildren<RoleEM>();
            if (roleEMs.Length != 0) {
                tm.roleSpawnerTMs = new RoleSpawnerTM[roleEMs.Length];
                for (int i = 0; i < roleEMs.Length; i++) {
                    var em = roleEMs[i];
                    RoleSpawnerTM spawnerTM = new RoleSpawnerTM() {
                        roleTypeID = em.tm.typeID,
                        pos = em.transform.position,
                        rotation = em.transform.eulerAngles,
                        localScale = em.transform.localScale
                    };
                    tm.roleSpawnerTMs[i] = spawnerTM;
                }
            }
        }

        EditorUtility.SetDirty(tm);
    }
}