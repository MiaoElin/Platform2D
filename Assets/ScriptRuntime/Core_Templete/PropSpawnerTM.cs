using UnityEngine;
using System;

[Serializable]
public class propSpawnerTM {

    public Vector2 pos;
    public Vector3 rotation;
    public Vector3 localScale;
    
    public int propTypeID;
    
    public bool isModifySize;
    public Vector2 sizeScale;

    // trampoline
    public float jumpForce;
}