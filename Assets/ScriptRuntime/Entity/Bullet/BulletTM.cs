using UnityEngine;

[CreateAssetMenu(menuName = "TM/TM_Bullet", fileName = "TM_Bullet_")]
public class BulletTM : ScriptableObject {

    public int typeID;
    public GameObject mod;
    public float moveSpeed;
    public float damageRate;

    // public float flyDistanceMax;
}