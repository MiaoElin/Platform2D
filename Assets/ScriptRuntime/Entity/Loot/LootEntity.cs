using System;
using UnityEngine;

public class LootEntity : MonoBehaviour {

    public int typeID;
    public int id;

    public SpriteRenderer sr;

    public void Ctor(Sprite sprite) {
        sr.sprite = sprite;
    }

    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public Vector2 Pos() {
        return transform.position;
    }

    public void SetRotation(Vector3 rotation) {
        transform.eulerAngles = rotation;
    }

    public void SetLocalScale(Vector3 scale) {
        transform.localScale = scale;
    }

}