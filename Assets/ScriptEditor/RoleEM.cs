using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class RoleEM : MonoBehaviour {

    [SerializeField] public RoleTM tm;
    public GameObject mod;

    void Awake() {
        if (mod == null) {
            mod = GameObject.Instantiate(tm.mod, transform);

        }
    }
}