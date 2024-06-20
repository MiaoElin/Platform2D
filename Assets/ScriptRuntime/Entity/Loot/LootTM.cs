using UnityEngine;
using System;
using TriInspector;

[CreateAssetMenu(menuName = "TM/TM_Loot", fileName = "TM_Loot_")]
public class LootTM : ScriptableObject {

    public int typeID;
    public int price;

    public GameObject mod;

    [Title("Need Hints")]
    public bool needHints;
    // DropLoot
    public bool isDropLoot; // 会掉落loot
    public float easingInduration;

    // GetCoin
    public bool isGetCoin;
    public int coinCount;
    public int coinTypeID;

    [Title("Don't Need Hints")]
    // IsCoin
    public bool isCoin;
    public float moveSpeed;
    public float gravity;
    public float centrifugalForce; // 用范围内随机更好

    // public float ;

    // GetBuff
    public bool isGetBuff;
    public int buffTypeId;

    // GetStuff
    public bool isGetStuff;

    // GetRobort
    public bool isGetRole;
    public int roleTypeID;

    public AudioClip getLootClip;
    public float volume;

}