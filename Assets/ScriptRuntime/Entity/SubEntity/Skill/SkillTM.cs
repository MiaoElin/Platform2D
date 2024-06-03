using UnityEngine;

[CreateAssetMenu(menuName = "TM/TM_Skill", fileName = "TM_SKill")]
public class SKillTM : ScriptableObject {
    public int typeID;
    public string skillName;
    public float damageRate;
    public int bulletTypeID;

    // cd
    public float cdMax;

    // 前
    public float preCastCDMax;

    // 中
    public float castingMaintainSec;
    public float castingIntervalSec;

    // 后
    public float endCastSec;

}