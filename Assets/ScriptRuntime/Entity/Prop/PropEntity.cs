using UnityEngine;
using System;

public class PropEntity : MonoBehaviour {
    public int typeID;
    public SpriteRenderer sr;
    public Vector2 size;

    // 梯子
    public bool isLadder;

    // 祭坛
    public bool isAltar;

    // 反射板
    public bool isTrampoline;
    public float jumpForce;
    public Sprite[] anim_BePress;
    public Action OnPressTrampolineHandle;

    public void Ctor() {

    }

    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public void SetRotation(Vector3 rotation) {
        transform.eulerAngles = rotation;
    }

    public void SetScale(Vector3 scale) {
        transform.localScale = scale;
    }

    public void SetMesh(Sprite mesh) {
        sr.sprite = mesh;
    }
}