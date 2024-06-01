using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Hints : MonoBehaviour {

    [SerializeField] Image img_Icon;

    internal void Ctor() {
    }

    internal void SetPos(Vector2 pos) {
        transform.position = pos;
    }
}