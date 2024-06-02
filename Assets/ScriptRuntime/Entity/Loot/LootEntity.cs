using System;
using UnityEngine;

public class LootEntity : MonoBehaviour {

    public int typeID;
    public int id;
    public int price;

    public GameObject mod;
    public Animator anim;
    public LootFSMComponent fsm;

    // DropLoot
    public bool needHints;
    public bool isDropLoot; // 会掉落loot

    // GetBuff
    public bool isGetBuff;
    public int buffTypeId;


    // GetStuff
    public bool isGetStuff;


    public void Ctor(GameObject mod) {
        this.mod = GameObject.Instantiate(mod, transform);
        this.anim = this.mod.GetComponentInChildren<Animator>();
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

    public void Anim_Used() {
        anim.Play("Used");
    }
}