using UnityEngine;
using System;

[CreateAssetMenu(menuName = "TM/TM_Loot", fileName = "TM_Loot_")]
public class LootTM : ScriptableObject {

    public int typeID;
    public int price;

    public GameObject mod;

    public bool needHints;
    // DropLoot
    public bool isDropLoot; // 会掉落loot
    public float easingInduration;

    // GetCoin
    public bool isGetCoin;
    public int coinCount;

    // Don't need Hints
    // GetBuff
    public bool isGetBuff;
    public int buffTypeId;

    // GetStuff
    public bool isGetStuff;

    // GetRobort
    public bool isGetRole;
    public int roleTypeID;

}