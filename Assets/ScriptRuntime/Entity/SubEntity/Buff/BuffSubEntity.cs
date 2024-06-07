using System;
using UnityEngine;

public class BuffSubEntity {

    public int typeID;
    public int id;
    public Sprite icon;

    // 永久buff
    public bool isPermanent;

    // 获得护甲
    public bool isGetShield;
    public float shieldValue; // 按数值获得护甲
    public float shieldPersent; // 按比例获得护甲
    public float shieldCDMax; // 护甲 的cd
    public float shieldDuration; // 持续时间
    public float shieldTimer;
    public bool isAddShield;

    // 提高生命值
    public bool isAddHp; // 增加血量
    public int addHpMax; // 最大增加量
    public int regenerationHpMax; // 每秒回血量


}