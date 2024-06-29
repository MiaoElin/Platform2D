using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class RoleSpawnerTM {

    public int roleTypeID;

    public Vector2 pos;
    public Vector3 rotation;
    public Vector3 localScale;

    public List<Vector2Int> path;

    public bool isPermanent;
    public float cd;
    public float cdMax;
    public float searchRange;

    public bool isBossWave;


}