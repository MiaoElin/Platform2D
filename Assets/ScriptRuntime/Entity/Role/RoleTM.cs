using UnityEngine;

[CreateAssetMenu(menuName = "TM/TM_Role", fileName = "TM_Role_")]
public class RoleTM : ScriptableObject {


    public int typeID;
    public int hpMax;
    public float moveSpeed;
    public float height;
    public AIType aiType;
    public GameObject mod;

    public float gravity;
    public float jumpForce;
    public int jumpTimesMax;
    public SkillTM[] sKillTMs;

}