using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD_AltarBar : MonoBehaviour {

    [SerializeField] Image img_BG;
    [SerializeField] Image img_Bar;
    [SerializeField] Text txt_percent;
    public int id;
    float timer;
    float duration;
    public bool timeisEnd;
    public Action<int> OnAltarTimeisEndHandle;
    public void Ctor(float duration, int id) {
        this.id = id;
        timer = 0;
        this.duration = duration;
        timeisEnd = false;
    }

    public void Init(float dt) {
        if (timeisEnd) {
            return;
        }
        if (timer >= duration) {
            // 祭坛时间到
            timeisEnd = true;
            // 只会进入一次
            OnAltarTimeisEndHandle.Invoke(id);
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