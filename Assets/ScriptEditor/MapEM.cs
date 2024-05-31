using UnityEditor;
using UnityEngine;

public class MapEM : MonoBehaviour {

    public MapTM tm;

    [ContextMenu("Save")]
    public void Save() {
        // 将roleEM[]存给RoleSpawnerTM[]
        // propEM[] 给 propSpawnerTM[]
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
                };
                tm.propSpawnerTMs[i] = propSpawnerTM;
            }
        }
    }
}