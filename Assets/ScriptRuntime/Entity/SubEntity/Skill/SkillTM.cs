using UnityEngine;

[CreateAssetMenu(menuName = "TM/TM_Skill", fileName = "TM_SKill")]
public class SkillTM : ScriptableObject {
    public int typeID;
    public string skillName;
    // public float damageRate;
    public float attackRange;

    // castBullet
    public bool isCastBullet;
    public int bulletTypeID;

    // castProp
    public bool isCastProp;
    public int propTypeID;

    // castFlash
    public bool isFlash;

    // cure
    public bool isCure;
    public int addHpMax;

    // melee
    public bool isMelee;
    public float meleeDamageRate;

    // cd
    public float cdMax;

    // 前
    public float preCastCDMax;

    // 中
    public float castingMaintainSec;
    public float castingIntervalSec;

    // 后
    public float endCastSec;

    // AudioClip
    public AudioClip castSfx;
    public float volume;

    // Stiffen
    public float stiffenSec;

}