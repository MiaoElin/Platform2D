using System;
using UnityEngine;

public class RoleEntity : MonoBehaviour {

    public int id;
    public int typeID;
    public bool isOwner;
    public int hp;
    public int hpMax;
    public float moveSpeed;
    public float height;
    public float attackRange;
    public Ally ally;
    public AIType aiType;
    public Vector2 faceDir;
    public Vector2[] path;
    [SerializeField] Rigidbody2D rb;
    public Animator anim;
    public Transform body;
    public Transform launchPoint; // 发射点

    public bool isStayInGround;
    public float gravity;
    public float jumpForce;
    public bool isJumpKeyDown;
    public bool isFlashKeyDown;
    public int jumpTimes;
    public int jumpTimesMax;

    public RoleFSMComponent fsm;
    public BuffSlotComponent buffCom;
    public SkillSlotComponent skillCom;
    public Action<Collider2D> OnTriggerEnterHandle;

    public Action<Collider2D> OnTriggerExitHandle;
    public Action<Collider2D> OnTriggerStayHandle;

    public void Ctor(GameObject mod) {
        var bodyMod = GameObject.Instantiate(mod, body);
        launchPoint = bodyMod.transform.Find("launchPoint").transform;
        this.anim = bodyMod.GetComponentInChildren<Animator>();
        fsm = new RoleFSMComponent();
        fsm.EnterNormal();
        buffCom = new BuffSlotComponent();
        skillCom = new SkillSlotComponent();
    }

    internal void Reuse() {
        Destroy(body.gameObject.GetComponentInChildren<GameObject>());
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

    private void SetForwardByTarget(Vector2 dir) {
        float rad = Mathf.Atan2(dir.y, dir.x);
        float deg = rad * Mathf.Rad2Deg;
        var euler = transform.eulerAngles;
        euler.z = deg;
        transform.eulerAngles = euler;
        faceDir = dir;
    }

    public Vector2 GetForWard() {
        if (aiType == AIType.ByTarget) {
            return faceDir;
        } else {
            if (body.transform.localScale.x > 0) {
                return Vector2.right;
            } else {
                return Vector2.left;
            }
        }

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

    internal Vector2 LaunchPoint() {
        return launchPoint.position;
    }

    #region  Move   
    public void MoveByAxisX(float axisX) {
        var velocity = rb.velocity;
        velocity.x = axisX * moveSpeed;
        rb.velocity = velocity;
        Anim_Run();
    }

    public void MoveByAxisY(float axisY) {
        var velocity = rb.velocity;
        velocity.x = 0;
        Anim_Run();
        velocity.y = axisY * moveSpeed;
        rb.velocity = velocity;
    }

    public void MoveByPath() {

    }

    public void MoveByTarget(Vector2 target) {
        Vector2 dir = target - Pos();
        var velocity = rb.velocity;
        velocity = dir.normalized * moveSpeed;
        rb.velocity = velocity;
        SetForwardByTarget(dir.normalized);
    }
    #endregion

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

    internal void SetVelocityY(float jumpForce) {
        var velocity = rb.velocity;
        velocity.y = jumpForce;
        rb.velocity = velocity;
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

    internal void Anim_Shoot(float axisX) {
        if (axisX == 0) {
            anim.SetBool("B_StandShoot", true);
        } else {
            anim.SetBool("B_StandShoot", false);
            anim.CrossFade("Run_Shoot", 0);
        }
    }

    // Collider
    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Ground") {
            isStayInGround = false;
        }
        if (isOwner) {
            OnTriggerExitHandle.Invoke(other);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ground") {
            isStayInGround = false;
        }
        if (isOwner) {
            OnTriggerEnterHandle.Invoke(other);
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Ground") {
            isStayInGround = true;
        }
        if (isOwner) {
            OnTriggerStayHandle.Invoke(other);
        }
    }


}