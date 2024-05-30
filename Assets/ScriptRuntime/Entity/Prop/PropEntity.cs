using UnityEngine;

public class PropEntity : MonoBehaviour {
    public int typeID;
    public SpriteRenderer sr;
    public Vector2 size;
    public bool isLadder;

    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public void SetRotation(Vector3 rotation) {
        transform.eulerAngles = rotation;
    }

    public void SetScale(Vector3 scale) {
        transform.localScale = scale;
    }
}