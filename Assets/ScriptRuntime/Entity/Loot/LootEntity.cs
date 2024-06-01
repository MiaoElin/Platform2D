using System;
using UnityEngine;

public class LootEntity : MonoBehaviour {

    public int typeID;
    public int id;

    public GameObject mod;
    public LootFSMComponent fsm;
    public bool isDropLoot; // 会掉落物品
    public bool needHints;


    public void Ctor(GameObject mod) {
        this.mod = GameObject.Instantiate(mod, transform);
        fsm = new LootFSMComponent();
    }

    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public Vector2 Pos() {
        return transform.position;
    }

    public void SetRotation(Vector3 rotation) {
        transform.eulerAngles = rotation;
    }

    public void SetLocalScale(Vector3 scale) {
        transform.localScale = scale;
    }

}