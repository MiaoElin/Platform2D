using UnityEngine;
using UnityEngine.UI;

public class HUD_HPBar : MonoBehaviour {
    [SerializeField] Image img_Hpbar;
    [SerializeField] Image img_HpBG;

    int hpMax;

    public void Ctor(int hpMax) {
        this.hpMax = hpMax;
    }

    public void Update_Tick(int hp, Vector2 pos) {
        float rate = (float)hp / hpMax;
        img_Hpbar.fillAmount = rate;

        transform.position = pos;
    }
}