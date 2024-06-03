using UnityEngine;

[CreateAssetMenu(menuName = "TM/TM_Loot", fileName = "TM_Loot_")]
public class LootTM : ScriptableObject {

    public int typeID;
    public int price;

    public GameObject mod;

    // DropLoot
    public bool needHints;
    public bool isDropLoot; // 会掉落loot
    public float easingInduration;

    // GetBuff
    public bool isGetBuff;
    public int buffTypeId;

    // GetStuff
    public bool isGetStuff;
}