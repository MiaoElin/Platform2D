using UnityEngine;

public class SkillSubEntity {

    public int typeID;
    public int id;

    // castBullet
    public bool isCastBullet;
    public int bulletTypeID;

    // castProp
    public bool isCastProp;
    public int propTypeID;

    // castFlash
    public bool isFlash;

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