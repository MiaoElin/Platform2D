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

    public void Ctor(GameObject mod) {
        this.mod = GameObject.Instantiate(mod, transform);
        anim = this.mod.GetComponentInChildren<Animator>();
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

    public void Move() {
        var velocity = rb.velocity;
        velocity = moveDir.normalized * moveSpeed;
        rb.velocity = velocity;
        // if (moveDir != Vector2.zero) {
        //     Debug.Log(Time.frameCount + "Move");
        // }
    }
}