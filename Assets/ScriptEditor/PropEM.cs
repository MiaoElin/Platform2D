using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class PropEM : MonoBehaviour {

    [SerializeField] public PropTM tm;
    public GameObject mod;

    public bool isModifySize;
    public Vector2 sizeScalse;
    public float jumpForce;
    public Ally ally;

    void Awake() {
        if (mod == null) {
            if (tm == null) {
                return;
            }
            mod = GameObject.Instantiate(tm.mod, transform);
            mod.transform.position = Vector3.zero;
            mod.transform.eulerAngles = Vector3.zero;
            mod.transform.localScale = Vector3.one;
            mod.name = tm.TypeName;
        }
    }
}