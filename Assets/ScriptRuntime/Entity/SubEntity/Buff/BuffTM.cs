using UnityEngine;

[CreateAssetMenu(menuName = "TM/TM_Buff", fileName = "TM_Buff_")]
public class BuffTM : ScriptableObject {

    public int typeID;
    public Sprite icon;

    // 永久buff
    public bool isPermanent;

    // 获得护甲
    public bool isGetShield;
    public float shieldValue; // 按数值获得护甲
    public float shieldPersent; // 按比例获得护甲
    public float shieldCDMax; // 护甲 的cd
    public float shieldDuration; // 持续时间

    // 提高生命值
    public bool isAddHp; // 增加血量
    public int addHpMax; // 最大增加量
    public int regenerationHpMax; // 每隔一段时间的回血量

}