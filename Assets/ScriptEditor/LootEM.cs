using UnityEngine;
using UnityEditor;
using System;

[ExecuteInEditMode]
public class LootEM : MonoBehaviour {
    public LootTM tm;
    public SpriteRenderer SR;

    void Awake() {
        if (SR.sprite == null) {
            SR.sprite = tm.sprite;
        }
    }
}