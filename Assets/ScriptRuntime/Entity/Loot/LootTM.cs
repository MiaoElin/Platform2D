using UnityEngine;

[CreateAssetMenu(menuName = "TM/TM_Loot", fileName = "TM_Loot_")]
public class LootTM : ScriptableObject {

    public int typeID;
    public Sprite sprite;

    public bool isDropLoot; // 会掉落物品
}