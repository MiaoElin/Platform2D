using UnityEngine;

public class RoleFSMComponent {

    public RoleStatus status;

    // Normal
    public bool isEnterNormal;

    // Casting
    public bool isEnterCasting;
    public bool isEnterCastStageReset;
    public SkillCastStage skillCastStage;
    // 前
    public float preCastTimer;
    // 中
    public float castingMainTimer;
    public float castingIntervalTimer;
    // 后
    public float endCastTimer;

    // Ladder
    public bool isEnterLadder;
    public float lowestY;
    public float highestY;

    // Trampoline
    public bool isEnterTrampoline;

    // Flash
    public bool isEnterFlash;

    // Destory
    public bool isEnterDestroy;

    public void EnterNormal() {
        status = RoleStatus.Normal;
        isEnterNormal = true;
        isEnterCastStageReset = true;
    }

    public void EnterLadder(float lowestY, float highestY) {
        status = RoleStatus.Ladder;
        isEnterLadder = true;
        this.lowestY = lowestY;
        this.highestY = highestY;
    }

    public void EnterCasting() {
        status = RoleStatus.Casting;
        isEnterCasting = true;
    }

    public void RestCastStage(SkillSubEntity skill) {
        skillCastStage = SkillCastStage.PreCast;
        preCastTimer = skill.preCastCDMax;
        castingMainTimer = skill.castingMaintainSec;
        castingIntervalTimer = skill.castingIntervalSec;
        endCastTimer = skill.endCastSec;
    }

    public void EnterTrampoline() {
        status = RoleStatus.Trampoline;
        isEnterTrampoline = true;
    }

    public void EnterFlash() {
        status = RoleStatus.Flash;
        isEnterFlash = true;
    }

    public void EnterDestroy() {
        status = RoleStatus.Destroy;
        isEnterDestroy = true;
    }
}