using System;
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

    // Suffering
    public bool isEnterSuffering;
    public RoleStatus lastStatus;
    public float sufferingTimer;
    public AntiStiffenType antiStiffenType;

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

    public void EnterSuffering(float sufferingSec, RoleStatus currentStatus) {
        // 不是Suffering状态才需要记录，如果记录成Suffering状态，那永远出不去Suffering了
        if (currentStatus != RoleStatus.Suffering) {
            this.lastStatus = currentStatus;
        }
        this.status = RoleStatus.Suffering;
        isEnterSuffering = true;
        if (antiStiffenType == AntiStiffenType.None) {
            sufferingTimer = sufferingSec;
        } else if (antiStiffenType == AntiStiffenType.Elite) {
            sufferingTimer = sufferingSec * 0.5f;
        } else if (antiStiffenType == AntiStiffenType.Boss) {
            sufferingTimer = 0;
        }
    }

    public void EnterDestroy() {
        status = RoleStatus.Destroy;
        isEnterDestroy = true;
    }

    internal void EnterLastStatus() {
        if (lastStatus == RoleStatus.Normal) {
            EnterNormal();
        } else if (lastStatus == RoleStatus.Ladder) {
            // EnterLadder();
        }
    }
}