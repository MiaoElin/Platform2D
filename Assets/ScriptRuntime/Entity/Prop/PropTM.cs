using UnityEngine;

[CreateAssetMenu(menuName = "TM/TM_Prop", fileName = "TM_Prop_")]
public class PropTM : ScriptableObject {

    public int typeID;
    public Sprite mesh;
    public Vector2 size;

    // 梯子
    public bool isLadder;

    // 祭坛
    public bool isAltar;

    // 反射板
    public bool isTrampoline;
    public float jumpForce;
    public Sprite[] anim_BePress;

}