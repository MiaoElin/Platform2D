using System;
using UnityEngine;

public class BulletEntity : MonoBehaviour {

    public int typeID;
    public int id;
    public Ally ally;
    public GameObject mod;
    public Animator anim;
    public Rigidbody2D rb;
    public MoveType moveType;
    public float moveSpeed;
    public float damgage;
    public Vector2 moveDir;
    public int targetID;

    public float flyTimer;
    public bool isTearDown;

    public float stiffenSec;

    public Action<Collider2D> onTriggerEnterHandle;

    public void Ctor(GameObject mod, float moveSpeed) {
        this.mod = GameObject.Instantiate(mod, transform);
        anim = this.mod.GetComponentInChildren<Animator>();
        this.moveSpeed = moveSpeed;
        flyTimer = CommonConst.BULLETFLYDISTANCEMAX / moveSpeed;
        isTearDown = false;
    }

    public Vector2 Pos() {
        return transform.position;
    }

    public void Reuse() {
        Destroy(mod.gameObject);
        isTearDown = false;
    }

    internal void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    internal void MoveByTarget(Vector2 target) {
        var dir = target - Pos();
        var velocity = rb.velocity;
        velocity = dir.normalized * moveSpeed;
        rb.velocity = velocity;
        SetForward(dir);
    }

    public void Anim_Shoot() {
        anim.Play("Shoot");
    }

    public void Move(Vector2 moveDir, float dt) {
        var velocity = rb.velocity;
        velocity = moveDir.normalized * moveSpeed;
        rb.velocity = velocity;
        flyTimer -= dt;
        if (flyTimer <= 0) {
            isTearDown = true;
        }
        SetForward(moveDir);
    }

    internal void SetForward(Vector2 moveDir) {
        float rad = Mathf.Atan2(moveDir.y, moveDir.x);
        float deg = rad * Mathf.Rad2Deg;
        var euler = transform.eulerAngles;
        euler.z = deg;
        transform.eulerAngles = euler;
    }

    // void OnTriggerEnter2D(Collider2D other) {
    //     Debug.Log("Enter");
    //     if (other.gameObject.tag == "Role") {
    //         Debug.Log("In");
    //     }
    // }
}