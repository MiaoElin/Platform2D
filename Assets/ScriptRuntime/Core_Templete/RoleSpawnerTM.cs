using UnityEngine;
using System;

[Serializable]
public class RoleSpawnerTM {

    public int roleTypeID;

    public Vector2 pos;
    public Vector3 rotation;
    public Vector3 localScale;

    public Vector2[] path;

    public bool isPermanent;
    public float cd;
    public float cdMax;

    public bool isBossWave;


}