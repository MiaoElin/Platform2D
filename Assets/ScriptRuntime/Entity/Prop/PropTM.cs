using UnityEngine;

[CreateAssetMenu(menuName = "TM/TM_Prop", fileName = "TM_Prop_")]
public class PropTM : ScriptableObject {

    public int typeID;
    public string TypeName;
    public GameObject mod;
    public Vector2 size;

    // 梯子
    public bool isLadder;

    // 祭坛
    public bool isAltar;

    // 反射板
    public bool isTrampoline;
    public Sprite[] anim_Normal;

    public ColliderType colliderType;

}