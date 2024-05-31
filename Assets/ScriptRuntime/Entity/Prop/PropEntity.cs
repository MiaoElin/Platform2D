using UnityEngine;
using System;

public class PropEntity : MonoBehaviour {
    public int typeID;
    public int id;
    public PropFSMComponent fsm;
    public GameObject mod;
    public SpriteRenderer sr;
    public Vector2 size;

    // 梯子
    public bool isLadder;
    public bool isOwnerOnLadder;

    // 祭坛
    public bool isAltar;

    // 反射板
    public bool isTrampoline;
    public bool isOwnerOnTrampoline;
    public float jumpForce;
    public Sprite[] anim_BePress;

    public bool isModifySize;
    public ColliderType colliderType;
    public BoxCollider2D boxCollider;
    public CapsuleCollider2D capsuleCollider;
    public CircleCollider2D circleCollider;


    public void Ctor() {
        fsm = new PropFSMComponent();
    }

    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public void SetRotation(Vector3 rotation) {
        transform.eulerAngles = rotation;
    }

    public void SetScale(Vector3 scale) {
        transform.localScale = scale;
    }

    public void SetMesh(GameObject mod) {
        this.mod = GameObject.Instantiate(mod, transform);
        this.sr = this.mod.GetComponentInChildren<SpriteRenderer>();
    }

    public void SetCollider(ColliderType colliderType, bool isModifySize, Vector2 sizeScale) {

        this.colliderType = colliderType;
        this.isModifySize = isModifySize;

        if (colliderType == ColliderType.Box) {
            boxCollider = mod.transform.GetComponent<BoxCollider2D>();
            if (isModifySize) {
                boxCollider.size = sizeScale;
                sr.size = sizeScale;
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

}