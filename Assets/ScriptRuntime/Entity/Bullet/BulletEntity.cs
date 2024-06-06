using System;
using UnityEngine;

public class BulletEntity : MonoBehaviour {

    public int typeID;
    public int id;
    public GameObject mod;
    public Animator anim;
    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 moveDir;

    public float flyTimer;
    public bool isTearDown;

    public void Ctor(GameObject mod, float moveSpeed) {
        this.mod = GameObject.Instantiate(mod, transform);
        anim = this.mod.GetComponentInChildren<Animator>();
        this.moveSpeed = moveSpeed;
        flyTimer = CommonConst.BULLETFLYDISTANCEMAX / moveSpeed;
        isTearDown = false;
    }

    public void Reuse() {
        Destroy(mod.gameObject);
    }

    internal void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public void Anim_Shoot() {
        anim.Play("Shoot");
    }

    public void Move(float dt) {
        var velocity = rb.velocity;
        velocity = moveDir.normalized * moveSpeed;
        rb.velocity = velocity;
        flyTimer -= dt;
        if (flyTimer <= 0) {
            isTearDown = true;
        }
    }

    internal void SetForward() {
        float rad = Mathf.Atan2(moveDir.y, moveDir.x);
        float deg = rad * Mathf.Rad2Deg;
        var euler = transform.eulerAngles;
        euler.z = deg;
        transform.eulerAngles = euler;
    }
}