using UnityEngine;

[CreateAssetMenu(menuName = "TM/TM_Prop", fileName = "TM_Prop_")]
public class PropTM : ScriptableObject {

    public int typeID;
    public string TypeName;
    public float moveSpeed;
    public bool isPermanent;
    public float activeTimer;
    public GameObject mod;
    public Vector2 srBaseSize;

    public bool hasAnim;
    public float fadeOutTimer; 

    // 梯子
    public bool isLadder;

    // 祭坛
    public bool isAltar;

    // 反射板
    public bool isTrampoline;
    public Sprite[] anim_Normal;

    // hurtFire
    public bool isHurtFire;
    public float hurtFireDamageRate;
    public float hurtFireDuration;


    public ColliderType colliderType;

}