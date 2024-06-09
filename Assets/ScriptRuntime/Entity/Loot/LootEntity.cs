using System;
using UnityEngine;
using GameFunctions;

public class LootEntity : MonoBehaviour {

    public int typeID;
    public int id;
    public int price;
    public GameObject mod;
    public Animator anim;
    public Transform setHintsPoint;
    public LootFSMComponent fsm;

    public bool isDead;

    // GetCoin
    public bool isGetCoin;
    public int coinCount;

    // DropLoot
    public bool needHints;
    public bool isDropLoot; // 会掉落loot

    // GetBuff
    public bool isGetBuff;
    public int buffTypeId;


    // GetStuff
    public bool isGetStuff;

    // GetRobort
    public bool isGetRole;
    public int roleTypeID;


    public void Ctor(GameObject mod) {
        this.mod = GameObject.Instantiate(mod, transform);
        this.anim = this.mod.GetComponentInChildren<Animator>();
        fsm = new LootFSMComponent();
        setHintsPoint = this.mod.transform.Find("SetHintsPoint");
    }

    internal void Reuse() {
        isDead = false;
        GameObject.Destroy(mod.gameObject);
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

    public void EasingIN(float dt) {
        if (!fsm.isEasingIn) {
            return;
        }
        ref var timer = ref fsm.easingIntimer;
        var duration = fsm.easingInduration;
        var startPos = fsm.easingInStartPos;
        var endPos = fsm.easingInEndPos;
        if (timer < duration - 0.1f) {
            transform.position = GFEasing.Ease2D(GFEasingEnum.MountainInCirc, timer, duration - 0.1f, startPos, endPos);
            timer += dt;
        } else if (timer < duration) {
            timer += dt;
        } else {
            timer = 0;
            fsm.isEasingIn = false;
        }
    }
}