using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD_AltarBar : MonoBehaviour {

    [SerializeField] Image img_BG;
    [SerializeField] Image img_Bar;
    [SerializeField] Text txt_percent;
    float timer;
    float duration;

    public void Ctor(float duration) {
        timer = 0;
        this.duration = duration;
    }

    public void Init(float dt) {
        if (timer >= duration) {
            timer = duration;
            return;
        }
        float percent = timer / duration;
        img_Bar.fillAmount = percent;
        // txt_percent.text = $"{percent * 100}%";
        timer += dt;
    }

    internal void SetPos(Vector2 pos) {
        transform.position = pos;
    }
}