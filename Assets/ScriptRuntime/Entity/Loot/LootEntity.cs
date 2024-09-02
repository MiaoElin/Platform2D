using System;
using UnityEngine;
using GameFunctions;

public class LootEntity : MonoBehaviour {

    public readonly EntityType entityType = EntityType.LootEntity;
    public int typeID;
    public int id;
    public int price;
    public GameObject mod;
    public Animator anim;
    public Transform setHintsPoint;
    [SerializeField] Rigidbody2D rb;
    Transform foot;
    public LootFSMComponent fsm;

    // public bool isTearDown;

    // DropLoot
    public bool needHints;
    public bool isDropLoot; // 会掉落loot

    // GetCoin
    public bool isGetCoin;
    public int coinCount;
    public int coinTypeID;

    // GetTips
    public bool isGetTips;

    // IsCoin
    public bool isCoin;
    public float moveSpeed;
    public float gravity;
    public float centrifugalForce;
    public bool isEnterGround; //检测到地面以后 重力失效，飞向owner
    public float coinFlyTimer;

    // GetBuff
    public bool isGetBuff;
    public int buffTypeId;


    // GetStuff
    public bool isGetStuff;

    // GetRobort
    public bool isGetRole;
    public int roleTypeID;

    public AudioClip getLootClip;
    public float volume;

    public void Ctor(GameObject mod) {
        this.mod = GameObject.Instantiate(mod, transform);
        this.anim = this.mod.GetComponentInChildren<Animator>();
        fsm = new LootFSMComponent();
        setHintsPoint = this.mod.transform.Find("SetHintsPoint");
        foot = this.mod.transform.Find("Foot");
    }

    internal void Reuse() {
        GameObject.Destroy(mod.gameObject);
        mod = null;
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

    #region Rb
    public void Falling(float dt) {
        var velocity = rb.velocity;
        velocity.y -= gravity * dt;
        rb.velocity = velocity;
    }

    public void RbAddForce_ByDir(Vector2 dir) {
        // rb.AddForce(dir.normalized * centrifugalForce, ForceMode2D.Force);
        rb.velocity = dir * centrifugalForce;
        // var velocity = rb.velocity;
        // velocity.y = centrifugalForce;
        // rb.velocity = velocity;
    }

    internal ulong GetTypeAndID() {
        return (ulong)((int)entityType << 32 | id);
    }

    public void SetVelocityZero() {
        rb.velocity = Vector2.zero;
    }

    public float GetVelocityY() {
        return rb.velocity.y;
    }

    public Vector2 GetFootPos() {
        return foot.position;
    }

    #endregion

    #region Move
    internal void Move_To(Vector2 target) {
        var dir = target - (Vector2)transform.position;
        var velocity = rb.velocity;
        velocity = dir.normalized * moveSpeed;
        rb.velocity = velocity;
    }
    #endregion

    #region Collider
    void OnTriggerEnter2D(Collider2D other) {
        if (isCoin) {
            // if (other.tag == "Ground") {
            //     // isDead = true;
            //     fsm.EnterDestroy();
            // }

            if (other.tag == "Role") {
                if (fsm.status == LootStatus.MovetoOwner) {
                    fsm.EnterDestroy();
                }
            }
        }
    }
    #endregion
}