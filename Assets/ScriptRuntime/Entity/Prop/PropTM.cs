using UnityEngine;
using TriInspector;

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

    public ColliderType colliderType;


    [Title("Ladder")]
    public bool isLadder;

    [Title("Altar")]
    public bool isAltar;
    public float altarDuration;

    [Title("Trampoline")]
    public bool isTrampoline;
    public Sprite[] anim_Normal;

    [Title("HurtFire")]
    public bool isHurtFire;
    public float hurtFireDamageRate;
    public float hurtFireDuration;

    [Title("Thron")]
    public bool isThron;
    public float thronDamageRate;
    public float thronDuration;


}