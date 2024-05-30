using UnityEngine;

[CreateAssetMenu(menuName = "TM/TM_Prop", fileName = "TM_Prop_")]
public class PropTM : ScriptableObject {

    public int typeID;
    public Sprite sprite;
    public Vector2 size;

    public bool isLadder;
    
}