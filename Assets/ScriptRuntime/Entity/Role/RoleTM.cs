using UnityEngine;

[CreateAssetMenu(menuName = "TM/TM_Role", fileName = "TM_Role_")]
public class RoleTM : ScriptableObject {


    public int typeID;
    public float moveSpeed;
    public MoveType moveType;
    public GameObject mod;
    
    public float gravity;
    public float jumpForce;
    public int jumpTimesMax;

}