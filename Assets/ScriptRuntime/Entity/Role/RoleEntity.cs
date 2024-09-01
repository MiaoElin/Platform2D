using System;
using System.Collections.Generic;
using UnityEngine;

public class RoleEntity : MonoBehaviour {

    public readonly EntityType entityTyp = EntityType.RoleEntity;
    public int id;
    public int typeID;
    public int price;
    public bool isOwner;
    public bool isBoss;

    // ==== Attribute ====
    public int hp;
    public int hpMax;
    public int shieldMax;
    public int shield;
    public int regenerationHpMax;
    public float regenerationTimer;
    public float regenerationDuration;
    public float moveSpeed;
    public float height;
    public float searchRange;
    public bool hasTarget;
    public Vector2 targetPos;

    public Vector2 lastPos;

    // buff Shield Dic
    Dictionary<int /*EntityID*/, int /*Shield*/> shieldDict; // 过程


    public int targeID;
    // public bool isDead;
    public Ally ally;
    public AIType aiType;
    public Vector2 faceDir;
    public List<Vector2Int> path;
    public float pathXMin;
    public float pathXMax;
    public int pathIndex;
    [SerializeField] Rigidbody2D rb;
    public Animator anim;
    public GameObject body;
    public Transform launchPoint; // 发射点
    // public Transform robotPoint;
    public RobotComponent robotCom;
    // LineRender
    public bool isCureRole;
    public LineRenderer lineR;
    List<Vector3> bezierPoints;
    public float bezierTimer;
    public float bezierTimeMax;
    public bool isStayInGround;
    public bool isOnGround;
    public float gravity;
    public float jumpForce;
    public bool isJumpKeyDown;
    public Vector2 moveAxis;
    // public bool isFlashKeyDown;
    public int jumpTimes;
    public int jumpTimesMax;
    public RoleFSMComponent fsm;
    public BuffSlotComponent buffCom;
    public SkillSlotComponent skillCom;
    public Action<Collider2D> OnTriggerEnterHandle;

    public Action<Collider2D> OnTriggerExitHandle;
    public Action<Collider2D> OnTriggerStayHandle;

    // dead
    public AudioClip die_Sfx;
    public float dieVolume;
    public float deathTimer;

    public void Ctor(GameObject mod, List<Vector2Int> path) {
        fsm = new RoleFSMComponent();
        fsm.EnterNormal();
        buffCom = new BuffSlotComponent();
        skillCom = new SkillSlotComponent();
        robotCom = new RobotComponent();

        // Body 生成
        body = GameObject.Instantiate(mod, transform);
        // set LaunchPoint
        launchPoint = body.transform.Find("LaunchPoint").transform;
        // RobotGroup
        var robotGroup = body.transform.Find("RobotPoint");
        robotCom.SetRobotPoint(robotGroup);
        // Anim
        this.anim = body.GetComponentInChildren<Animator>();

        shieldDict = new Dictionary<int, int>();
        // path
        this.path = path;
        if (path != null) {
            pathXMax = path[0].x;
            pathXMin = path[0].x;
            foreach (var pos in path) {
                if (pos.x >= pathXMax) {
                    pathXMax = pos.x;
                }
                if (pos.x <= pathXMin) {
                    pathXMin = pos.x;
                }
            }
        }
    }

    #region  Pos
    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public Vector2 Pos() {
        return transform.position;
    }

    public void SetLastPos(Vector2 lastPos) {
        this.lastPos = lastPos;
    }

    public Vector2 LastPos() {
        return lastPos;
    }

    public Vector2Int GetPos_Int() {
        return new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }

    public Vector2 GetHead_Top() {
        return body.transform.Find("Head_Top").position;
    }

    public Vector2 GetBody_Center() {
        return body.transform.Find("Body_Center").position;
    }

    public Vector2 GetBelly() {
        return body.transform.Find("Belly").position;
    }

    public Vector2 GetFoot_Front() {
        return body.transform.Find("Foot_Front").position;
    }

    public Vector2 GetFoot() {
        return body.transform.Find("Foot").position;
    }
    #endregion

    internal void Reuse() {
        Destroy(body.gameObject);
    }

    public void SetForward(float axisX) {
        var scale = transform.localScale;
        if (axisX > 0) {
            scale.x = Mathf.Abs(scale.x);
        } else if (axisX < 0) {
            scale.x = -Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    public void SetForwardByOwner(Vector2 dir) {
        dir = -dir;
        float rad = Mathf.Atan2(dir.y, dir.x);
        float deg = rad * Mathf.Rad2Deg;
        var euler = transform.eulerAngles;
        euler.z = deg;
        transform.eulerAngles = euler;
        faceDir = dir;
    }

    public Vector2 GetForWard() {
        if (aiType == AIType.Flyer) {
            return faceDir;
        } else {
            if (transform.localScale.x > 0) {
                return Vector2.right;
            } else {
                return Vector2.left;
            }
        }
    }

    public void SetRotation(Vector3 rotation) {
        transform.eulerAngles = rotation;
    }

    internal Vector2 LaunchPoint() {
        return launchPoint.position;
    }

    #region  Move   
    public void MoveByAxisX(float axisX) {
        if (axisX > 0) {
            axisX = 1;
        } else if (axisX < 0) {
            axisX = -1;
        }
        var velocity = rb.velocity;
        velocity.x = axisX * moveSpeed;
        rb.velocity = velocity;
        Anim_SetSpeed();
    }

    public void MoveByAxisY(float axisY) {
        var velocity = rb.velocity;
        velocity.x = 0;
        velocity.y = axisY * moveSpeed;
        rb.velocity = velocity;
    }

    internal void Move_AxisX_Stop() {
        var velocity = rb.velocity;
        velocity.x = 0;
        rb.velocity = velocity;
    }

    public void Move_Stop() {
        rb.velocity = Vector2.zero;
    }


    public void MoveByPath(float dt) {
        if (pathIndex >= path.Count) {
            // rb.velocity = Vector2.zero;
            pathIndex = 0;
            return;
        }
        var nextPos = path[pathIndex];
        Vector2 dir = nextPos - Pos();
        if (Vector2.SqrMagnitude(dir) <= (moveSpeed * dt) * (moveSpeed * dt)) {
            pathIndex++;
        }

        var velocity = rb.velocity;
        if (dir.x > 0) {
            dir.x = 1;
        } else {
            if (dir.x < 0) {
                dir.x = -1;
            }
        }
        if (dir.y > 0) {
            dir.y = 1;
        } else {
            if (dir.y < 0) {
                dir.y = -1;
            }
        }
        velocity = dir.normalized * moveSpeed;
        rb.velocity = velocity;
        SetForward(velocity.x);
        Anim_SetSpeed();
    }

    public void MoveByTarget(Vector2 target, float dt) {
        Vector2 dir = target - Pos();
        if (Vector2.SqrMagnitude(dir) < Mathf.Pow(moveSpeed * dt, 2)) {
            rb.velocity = Vector2.zero;
            return;
        }
        var velocity = rb.velocity;
        velocity = dir.normalized * moveSpeed;
        rb.velocity = velocity;
        SetForward(dir.x);
    }
    #endregion

    #region  Jump
    public void Jump() {
        if (isJumpKeyDown && jumpTimes > 0) {
            var velocity = rb.velocity;
            velocity.y = jumpForce;
            rb.velocity = velocity;
            isJumpKeyDown = false;
            isOnGround = false;
            jumpTimes--;
            Anim_Jump();
        }
    }

    internal void ReuseJumpTimes() {
        this.jumpTimes = jumpTimesMax;
    }

    internal void SetVelocityY(float jumpForce) {
        var velocity = rb.velocity;
        velocity.y = jumpForce;
        rb.velocity = velocity;
    }

    public Vector2 GetVelocity() {
        return rb.velocity;
    }

    public float GetVelocityY() {
        return rb.velocity.y;
    }

    public float GetVelocityX() {
        return rb.velocity.x;
    }

    #endregion

    #region  Falling
    public void Falling(float dt) {
        var velocity = rb.velocity;
        // if (isOnGround) {
        //     velocity.y = 0;
        //     rb.velocity = velocity;
        //     return;
        // }
        velocity.y -= gravity * dt;
        rb.velocity = velocity;
        if (velocity.y < 0) {
            // Anim_FallingStart();
        }
    }
    #endregion

    // === Anim ===
    #region Animator
    internal void Anim_Idle() {
        anim.CrossFade("Idle", 0);
    }

    public void Anim_SetSpeed() {
        var speed = Mathf.Abs(rb.velocity.x);
        anim.SetFloat("F_MoveSpeed", speed);
    }

    public void Anim_SetSpeedZero() {
        anim.SetFloat("F_MoveSpeed", 0);
    }

    public void Anim_Climb() {
        var speed = Mathf.Abs(rb.velocity.y);
        anim.CrossFade("Climb", 0);
    }

    public void Anim_Jump() {
        anim.Play("Jump");
    }

    public void Anim_FallingStart() {
        anim.ResetTrigger("T_FallingStart");
        anim.SetTrigger("T_FallingStart");
    }


    public void Anim_JumpEnd() {
        anim.SetTrigger("T_JumpEnd");
        // anim.CrossFade()
    }

    internal void Anim_Shoot(float axisX) {
        if (!isOwner) {
            anim.CrossFade("Shoot", 0);
            return;
        }
        if (axisX == 0) {
            anim.CrossFade("Stand_Shoot", 0);
        } else {
            anim.CrossFade("Run_Shoot", 0);
        }
    }

    internal void anim_Attack() {
        if (isOwner) {
            if (moveAxis.x == 0) {
                anim.CrossFade("Stand_Shoot", 0);
            } else {
                anim.CrossFade("Run_Shoot", 0);
            }
        } else {
            anim.Play("Attack_Pre", 0);
        }
    }

    internal void Anim_Hurt() {
        anim.CrossFade("Hurt", 0);
    }

    internal void Anim_Die() {
        anim.CrossFade("Die", 0);
    }

    #endregion

    // Collider
    #region  Collider
    public void ColliderEnAble(bool b) {
        body.GetComponent<Collider2D>().isTrigger = !b;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (isOwner) {
            if (other.tag == "Ground") {
                isStayInGround = false;
            }
            OnTriggerExitHandle.Invoke(other);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        OnTriggerEnterHandle.Invoke(other);
    }

    void OnTriggerStay2D(Collider2D other) {
        if (isOwner) {
            OnTriggerStayHandle.Invoke(other);
            if (other.tag == "Ground") {
                isStayInGround = true;
            }
        }
    }
    #endregion

    #region ShieldDic
    public void BuffShieldAdd(int typeID, int value) {
        if (shieldDict.ContainsKey(typeID)) {
            shieldDict[typeID] += value;
        } else {
            shieldDict.Add(typeID, value);
        }
    }

    public void BuffShieldRemove(int id) {
        if (shieldDict.ContainsKey(id)) {
            shieldDict.Remove(id);
        }
    }

    public void BuffShieldUseAll() {
        for (int i = 0; i < shieldDict.Count; i++) {
            shieldDict[i] = 0;
        }
    }

    public int GetallShield() {
        shield = 0;
        foreach (var val in shieldDict) {
            shield += val.Value;
        }
        return shield;
    }

    public bool ShieldDicTryget(int typeID, out int shield) {
        return shieldDict.TryGetValue(typeID, out shield);
    }

    public void BuffShieldReduce(int typeID, int reduceValue) {
        if (shieldDict[typeID] < reduceValue) {
            shieldDict[typeID] = 0;
            return;
        }
        shieldDict[typeID] -= reduceValue;
    }

    #endregion

    #region  LineRender

    public void OpenLineR(Vector2 ownerPos) {
        this.isCureRole = true;
        GetLineRender();
        LR_SetWidth();
        LR_Tick(ownerPos);
        // LineREnable(false);
    }

    public void LineREnable(bool b) {
        lineR.enabled = b;
    }

    public void GetLineRender() {
        this.lineR = body.GetComponentInChildren<LineRenderer>();
        bezierPoints = new List<Vector3>();
    }

    public void LR_SetColor() {
        lineR.startColor = new Color((float)103 / 255, 1f, (float)68 / 255, 0.4f);
        lineR.endColor = new Color((float)103 / 255, 1f, (float)68 / 255, 0f);
    }

    public void LR_SetWidth() {
        lineR.startWidth = 0.4f;
        lineR.endWidth = 0.15f;
    }

    public void LR_Tick(Vector2 end) {
        var p0 = lineR.transform.Find("p0").transform.position;
        var p1 = lineR.transform.Find("p1").transform.position;
        var p2 = end;
        bezierPoints.Clear();
        for (int i = 0; i < 101; i++) {
            float t = (float)i / 100;
            var pos = GetCurrentPoint(t, p0, p1, p2);
            bezierPoints.Add(pos);
        }

        lineR.positionCount = bezierPoints.Count;
        lineR.SetPositions(bezierPoints.ToArray());

    }

    public Vector2 GetCurrentPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2) {
        Vector2 p0_1 = Vector2.Lerp(p0, p1, t);
        Vector2 p1_2 = Vector2.Lerp(p1, p2, t);
        Vector2 p01_p12 = Vector2.Lerp(p0_1, p1_2, t);
        return p01_p12;
    }

    #endregion
}