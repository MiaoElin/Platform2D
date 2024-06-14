using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class RoleEM : MonoBehaviour {

    [SerializeField] public RoleTM tm;
    public GameObject mod;
    public bool isPermanent;
    public float cdMax;
    public bool isBossWave;
    void Awake() {
        if (mod == null) {
            mod = GameObject.Instantiate(tm.mod, transform);
        }
    }
}