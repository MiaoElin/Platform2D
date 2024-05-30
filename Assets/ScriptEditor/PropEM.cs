using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class PropEM : MonoBehaviour {

    [SerializeField] public PropTM tm;
    public GameObject mod;

    public bool isModifySize;
    public Vector2 sizeScalse;

    void Awake() {
        if (mod == null) {
            if (tm == null) {
                return;
            }
            // mod = new GameObject("SR");
            // mod.transform.SetParent(transform);
            // var sr = mod.AddComponent<SpriteRenderer>();
            // sr.name = propTM.TypeName;
            // sr.drawMode = SpriteDrawMode.Tiled;
            // sr.sprite = propTM.mesh;
            mod = GameObject.Instantiate(tm.mod, transform);
            mod.transform.position = Vector3.zero;
            mod.transform.eulerAngles = Vector3.zero;
            mod.transform.localScale = Vector3.one;
            mod.name = tm.TypeName;
        }
    }
}