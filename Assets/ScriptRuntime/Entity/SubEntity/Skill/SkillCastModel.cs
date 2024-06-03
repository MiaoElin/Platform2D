using UnityEngine;

public class SkillCastStateModel {

    public SkillCastStage skillCastStage;

    // cd
    public float cd;
    public float cdMax;

    // 前
    public float preCastCDMax;
    public float preCastTimer;

    // 中
    public float castingMaintainSec;
    public float castingMainTimer;
    public float castingIntervalSec;
    public float castingIntervalTimer;

    // 后
    public float endCastSec;
    public float endCastTimer;

}