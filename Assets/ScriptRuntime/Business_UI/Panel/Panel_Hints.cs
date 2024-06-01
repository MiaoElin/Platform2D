using System;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Hints : MonoBehaviour {

    [SerializeField] Image img_Icon;

    internal void Ctor() {
    }

    internal void SetPos(Vector2 screenPos) {
        transform.position = screenPos;
    }
}