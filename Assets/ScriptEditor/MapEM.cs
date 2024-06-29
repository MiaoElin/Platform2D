using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class MapEM : MonoBehaviour {

    public MapTM tm;
    public int xCount;
    public int yCount;

    [ContextMenu("Save")]
    public void Save() {
        // 将roleEM[]存给RoleSpawnerTM[]
        // propEM[] 给 propSpawnerTM[]

        tm.xCount = this.xCount;
        tm.yCount = this.yCount;

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
                        jumpForce = em.jumpForce,
                        ally = em.ally
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
                    var path = em.transform.Find("path");
                    var trans = path.GetComponentsInChildren<Transform>();
                    List<Vector2Int> posArray = new List<Vector2Int>();
                    for (int j = 0; j < trans.Length; j++) {
                        var pos = new Vector2Int(Mathf.RoundToInt(trans[j].position.x), Mathf.RoundToInt(trans[j].position.y));
                        posArray.Add(pos);
                    }

                    RoleSpawnerTM spawnerTM = new RoleSpawnerTM() {
                        roleTypeID = em.tm.typeID,
                        searchRange = em.tm.searchRange,
                        pos = em.transform.position,
                        rotation = em.transform.eulerAngles,
                        localScale = em.transform.localScale,
                        path = posArray,
                        isPermanent = em.isPermanent,
                        cd = 0,
                        cdMax = em.cdMax,
                        isBossWave = em.isBossWave

                    };
                    tm.roleSpawnerTMs[i] = spawnerTM;
                }
            }
        }

        EditorUtility.SetDirty(tm);
    }
}