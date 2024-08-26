using UnityEngine;
using System;

public class PropEntity : MonoBehaviour {

    public readonly EntityType entityType = EntityType.PropEntity;
    public int typeID;
    public int id;
    public Ally ally;
    public float moveSpeed;
    public bool isPermanent;
    public float activeTimer;

    public PropFSMComponent fsm;
    GameObject mod;
    public Transform setHintsPoint;
    public SpriteRenderer sr;
    // public Vector2 srBaseSize;
    public Vector2 size;
    public Vector2 moveDir;

    public bool hasAnim;
    public Animator anim;


    // 梯子
    public bool isLadder;

    // 
    public bool isAltarBarFull;

    internal ulong GetTypeAddID() {
        return (ulong)((int)entityType << 32 | id);
    }

    public bool isOwnerOnLadder;

    // 祭坛
    public bool isAltar;
    public float altarDuration;


    // 反射板
    public bool isTrampoline;
    public bool isOwnerOnTrampoline;
    public float jumpForce;
    public Sprite[] anim_BePress;

    // hurtFire
    public bool isHurtFire;
    public float hurtFireDamageRate;
    public float hurtFireTimer;
    public float hurtFireDuration;

    public bool isTearDown;

    public bool isModifySize;
    public ColliderType colliderType;
    public BoxCollider2D boxCollider;
    public CapsuleCollider2D capsuleCollider;
    public CircleCollider2D circleCollider;


    public void Ctor() {
        fsm = new PropFSMComponent();
    }

    public void Reuse() {
        GameObject.Destroy(mod.gameObject);
        isTearDown = false;
    }

    internal void Move(float dt) {
        transform.position += (Vector3)moveDir.normalized * moveSpeed * dt;
    }

    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public void SetForward() {
        if (moveDir.x > 0) {
            transform.localScale = Vector3.one;
        } else if (moveDir.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public Vector2 Pos() {
        return transform.position;
    }

    public void SetRotation(Vector3 rotation) {
        transform.eulerAngles = rotation;
    }

    public void SetScale(Vector3 scale) {
        transform.localScale = scale;
    }

    public void SetMesh(GameObject mod, bool hasAnim) {
        this.mod = GameObject.Instantiate(mod, transform);
        this.sr = this.mod.GetComponentInChildren<SpriteRenderer>();
        this.hasAnim = hasAnim;
        if (hasAnim) {
            anim = this.mod.GetComponentInChildren<Animator>();
        }
        this.setHintsPoint = this.mod.transform.Find("setHintsPoint");
    }

    public void SetCollider(ColliderType colliderType, bool isModifySize, Vector2 sizeScale) {

        this.colliderType = colliderType;
        this.isModifySize = isModifySize;

        if (colliderType == ColliderType.Box) {
            boxCollider = mod.transform.GetComponent<BoxCollider2D>();
            if (isModifySize) {
                boxCollider.size = sizeScale;
                sr.size = sizeScale;
                size = sizeScale;
            }
        } else if (colliderType == ColliderType.Capsule) {
            capsuleCollider = mod.transform.GetComponent<CapsuleCollider2D>();
            if (isModifySize) {
                capsuleCollider.size = sizeScale;
                sr.size = sizeScale;
            }
        } else if (colliderType == ColliderType.Circle) {
            circleCollider = mod.transform.GetComponent<CircleCollider2D>();
            if (isModifySize) {
                // circleCollider.size = sizeScale;
                sr.size = sizeScale;
            }
        }
    }

    void HideAllCollider() {
        this.boxCollider.gameObject.SetActive(false);
        this.capsuleCollider.gameObject.SetActive(false);
        this.circleCollider.gameObject.SetActive(false);
    }

    public void Anim_FadOut() {
        anim.CrossFade("FadeOut", 0);
    }
}