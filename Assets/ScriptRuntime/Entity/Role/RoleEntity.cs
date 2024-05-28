using System;
using UnityEngine;

public class RoleEntity : MonoBehaviour {

    public int id;
    public int typeID;
    public float moveSpeed;
    public Ally ally;
    public MoveType moveType;
    [SerializeField] Rigidbody2D rb;
    public Animator anim;
    public Transform body;

    public float jumpForce;
    public bool isJumpKeyDown;
    public int jumpTimes;
    public int jumpTimesMax;

    public void Ctor(GameObject mod) {
        var bodyMod = GameObject.Instantiate(mod, body);
        this.anim = bodyMod.GetComponent<Animator>();
    }

    public void SetForward(Vector2 moveAxis) {
        var scale = body.transform.localScale;
        if (moveAxis.x > 0) {
            scale.x = Mathf.Abs(scale.x);
        } else if (moveAxis.x < 0) {
            scale.x = -Mathf.Abs(scale.x);
        }
        body.transform.localScale = scale;
    }

    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    internal void ReuseJumpTimes() {
        this.jumpTimes = jumpTimesMax;
    }

    public Vector2 Pos() {
        return transform.position;
    }

    public void Move(Vector2 moveAxis) {
        var velocity = rb.velocity;
        velocity.x = moveAxis.x * moveSpeed;
        rb.velocity = velocity;

        SetForward(moveAxis);
    }

    public void Jump() {
        if (isJumpKeyDown && jumpTimes > 0) {
            var velocity = rb.velocity;
            velocity.y = jumpForce;
            rb.velocity = velocity;
            isJumpKeyDown = false;
            jumpTimes--;
        }
    }

    // === Anim ===
    public void Anim_Move() {
        var speed = Mathf.Abs(rb.velocity.x);
        anim.SetFloat("F_MoveSpeed", speed);
    }

    public void Anim_Jump() {
        anim.SetTrigger("T_Jump");
    }



}