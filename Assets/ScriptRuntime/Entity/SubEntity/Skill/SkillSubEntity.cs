using UnityEngine;

public class SkillSubEntity {

    public int typeID;
    public int id;

    public bool isCastBullet;
    // public float bulletDamageRate;
    public int bulletTypeID;

    public bool isCastProp;
    public int propTypeID;

    // cd
    public float cd;
    public float cdMax;

    // 前
    public float preCastCDMax;

    // 中
    public float castingMaintainSec;
    public float castingIntervalSec;

    // 后
    public float endCastSec;


    public SkillSubEntity() {
    }
}