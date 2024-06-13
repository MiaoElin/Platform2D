using UnityEngine;
using TriInspector;

[CreateAssetMenu(menuName = "TM/TM_Role", fileName = "TM_Role_")]
public class RoleTM : ScriptableObject {


    public int typeID;
    public bool isRobot;
    public int price;
    public int hpMax;
    public int regenerationHpMax;
    public float regenerationDuration;
    public float moveSpeed;
    public float height;
    public float attackRange;
    public AIType aiType;
    public GameObject mod;

    public float gravity;
    public float jumpForce;
    public int jumpTimesMax;
    public SkillTM[] sKillTMs;
    public Sprite[] vfx_Flash;

    [Title("Destory")]
    public bool isDropLoot;
}