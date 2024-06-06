using System;
using UnityEngine;

public class BulletEntity : MonoBehaviour {

    public int typeID;
    public int id;
    public Ally ally;
    public GameObject mod;
    public Animator anim;
    public Rigidbody2D rb;
    public float moveSpeed;
    public float damgage;
    public Vector2 faceDir;

    public float flyTimer;
    public bool isTearDown;

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
    }

    internal void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public void Anim_Shoot() {
        anim.Play("Shoot");
    }

    public void Move(float dt) {
        var velocity = rb.velocity;
        velocity = faceDir.normalized * moveSpeed;
        rb.velocity = velocity;
        flyTimer -= dt;
        if (flyTimer <= 0) {
            isTearDown = true;
        }
    }

    internal void SetForward() {
        float rad = Mathf.Atan2(faceDir.y, faceDir.x);
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