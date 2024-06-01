using System;
using UnityEngine;

public class RoleEntity : MonoBehaviour {

    public int id;
    public int typeID;
    public bool isOwner;
    public float moveSpeed;
    public float height;
    public Ally ally;
    public MoveType moveType;
    [SerializeField] Rigidbody2D rb;
    public Animator anim;
    public Transform body;

    public bool isStayInGround;
    public float gravity;
    public float jumpForce;
    public bool isJumpKeyDown;
    public int jumpTimes;
    public int jumpTimesMax;

    public RoleFSMComponent fsm;
    public Action<Vector2> openLootHintsHandle;

    public void Ctor(GameObject mod) {
        var bodyMod = GameObject.Instantiate(mod, body);
        this.anim = bodyMod.GetComponentInChildren<Animator>();
        fsm = new RoleFSMComponent();
        fsm.EnterNormal();

    }

    public void SetForward(float axisX) {
        var scale = body.transform.localScale;
        if (axisX > 0) {
            scale.x = Mathf.Abs(scale.x);
        } else if (axisX < 0) {
            scale.x = -Mathf.Abs(scale.x);
        }
        body.transform.localScale = scale;
    }

    internal void MoveByVertical(float y) {
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

    public void MoveByAxisX(float axisX) {
        var velocity = rb.velocity;
        velocity.x = axisX * moveSpeed;
        rb.velocity = velocity;
        Anim_Run();
        SetForward(axisX);


    }

    public void MoveByAxisY(float axisY) {
        var velocity = rb.velocity;
        velocity.x = 0;
        Anim_Run();
        velocity.y = axisY * moveSpeed;
        rb.velocity = velocity;
    }

    public void Jump() {
        if (isJumpKeyDown && jumpTimes > 0) {
            var velocity = rb.velocity;
            velocity.y = jumpForce;
            rb.velocity = velocity;
            isJumpKeyDown = false;
            jumpTimes--;

            Anim_Jump();
        }
    }

    public void Falling(float dt) {
        var velocity = rb.velocity;
        velocity.y -= gravity * dt;
        rb.velocity = velocity;
        if (velocity.y < 0) {
            Anim_FallingStart();
        }
    }

    public float GetVelocityY() {
        return rb.velocity.y;
    }

    // === Anim ===
    public void Anim_Run() {
        var speed = Mathf.Abs(rb.velocity.x);
        anim.SetFloat("F_MoveSpeed", speed);
    }

    public void Anim_Climb() {
        var speed = Mathf.Abs(rb.velocity.y);
        anim.CrossFade("Climb", 0);
    }

    public void Anim_Jump() {
        anim.Play("Jump");
    }

    public void Anim_FallingStart() {
        anim.SetTrigger("T_FallingStart");
    }


    public void Anim_JumpEnd() {
        anim.SetTrigger("T_JumpEnd");
        // anim.CrossFade()
    }

    internal void SetVelocityY(float jumpForce) {
        var velocity = rb.velocity;
        velocity.y = jumpForce;
        rb.velocity = velocity;
    }

    // Collider
    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Ground") {
            isStayInGround = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ground") {
            isStayInGround = false;
        }
        if (isOwner) {
            if (other.tag == "Loot") {
                var loot = other.gameObject.GetComponentInParent<LootEntity>();
                if (loot.needHints) {
                    openLootHintsHandle.Invoke(loot.Pos());
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Ground") {
            isStayInGround = true;
        }
    }



}