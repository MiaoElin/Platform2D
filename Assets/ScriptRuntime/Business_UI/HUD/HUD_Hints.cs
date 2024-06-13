using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Hints : MonoBehaviour {

    [SerializeField] Image img_Icon;
    [SerializeField] Text txt_price;

    internal void Ctor(int price) {
        if (price == 0) {
            txt_price.text = "";
            return;
        }
        txt_price.text = price.ToString();
    }

    internal void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public void ShowHintsIcon() {
        img_Icon.gameObject.SetActive(true);
    }

    public void HideHintIcon() {
        if (!img_Icon) {
            return;
        }
        img_Icon.gameObject.SetActive(false);
    }
}