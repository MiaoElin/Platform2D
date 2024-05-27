using UnityEngine;

public class RoleEntity : MonoBehaviour {

    public int id;
    public int typeID;
    public float moveSpeed;
    public Ally ally;
    public MoveType moveType;
    [SerializeField] Rigidbody2D rb;
    public Animator anim;
    public Transform modTransform;

    public void Ctor(Animator anim, GameObject mod) {
        this.anim = anim;
        GameObject.Instantiate(mod, modTransform);
    }

    public void Move(Vector2 moveAxis, float dt) {
        var velocity = rb.velocity;
        velocity = moveAxis.normalized * moveSpeed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;

        SetForward(moveAxis);
    }

    public void Anim_Move() {
        anim.SetFloat("F_MoveSpeed", rb.velocity.magnitude);
    }

    public void Anim_Jump() {
        anim.SetTrigger("T_Jump");
    }

    public void SetForward(Vector2 moveAxis) {
        if (moveAxis.x >= 0) {
            transform.forward = Vector2.right;
        } else {
            transform.forward = Vector2.left;
        }
    }

    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public Vector2 Pos() {
        return transform.position;
    }



}