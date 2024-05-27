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

    public void Ctor(GameObject mod) {
        var bodyMod = GameObject.Instantiate(mod, body);
        this.anim = bodyMod.GetComponent<Animator>();
    }

    public void Move(Vector2 moveAxis) {
        var velocity = rb.velocity;
        velocity.x = moveAxis.x * moveSpeed;
        rb.velocity = velocity;

        SetForward(moveAxis);
    }

    public void Anim_Move() {
        var speed = Mathf.Abs(rb.velocity.x);
        anim.SetFloat("F_MoveSpeed", speed);
    }

    public void Anim_Jump() {
        anim.SetTrigger("T_Jump");
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

    public Vector2 Pos() {
        return transform.position;
    }



}