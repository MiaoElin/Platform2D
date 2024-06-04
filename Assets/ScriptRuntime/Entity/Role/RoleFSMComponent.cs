using UnityEngine;

public class RoleFSMComponent {

    public RoleStatus status;

    public bool isEnterNormal;
    public bool isEnterCastStageReset;
    public SkillCastStage skillCastStage;
    // 前
    public float preCastTimer;
    // 中
    public float castingMainTimer;
    public float castingIntervalTimer;
    // 后
    public float endCastTimer;


    public bool isEnterLadder;
    public float lowestY;
    public float highestY;

    public bool isEnterCasting;

    public void EnterNormal() {
        status = RoleStatus.Normal;
        isEnterNormal = true;
        isEnterCastStageReset = true;
    }

    public void RestCastStage(SkillSubEntity skill) {
        skillCastStage = SkillCastStage.PreCast;
        castingMainTimer = skill.castingMaintainSec;
        castingIntervalTimer = skill.castingIntervalSec;
        endCastTimer = skill.endCastSec;
    }

    public void EnterLadder(float lowestY, float highestY) {
        status = RoleStatus.Ladder;
        isEnterLadder = true;
        this.lowestY = lowestY;
        this.highestY = highestY;
    }

}