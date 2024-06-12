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

    [Title("Don't Need Hints")]
    // GetBuff
    public bool isGetBuff;
    public int buffTypeId;

    // GetStuff
    public bool isGetStuff;

    // GetRobort
    public bool isGetRole;
    public int roleTypeID;

}