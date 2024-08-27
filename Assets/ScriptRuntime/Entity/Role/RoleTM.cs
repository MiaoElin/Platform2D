using UnityEngine;
using TriInspector;

[CreateAssetMenu(menuName = "TM/TM_Role", fileName = "TM_Role_")]
public class RoleTM : ScriptableObject {


    public int typeID;
    public bool isRobot;
    public bool isBoss;
    public int price;
    public int hpMax;
    public int regenerationHpMax;
    public float regenerationDuration;
    public float moveSpeed;
    public float height;
    public float searchRange;
    public AIType aiType;
    public GameObject mod;

    public float gravity;
    public float jumpForce;
    public int jumpTimesMax;
    public SkillTM[] sKillTMs;
    public Sprite[] vfx_Flash;

    // Suffering
    public AntiStiffenType antiStiffenType;

    [Title("Destory")]
    public bool isDropLoot;
    public AudioClip die_Sfx;
    public float dieVolume;
    public float deathTimer;
}