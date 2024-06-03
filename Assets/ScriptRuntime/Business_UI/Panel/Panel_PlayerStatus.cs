using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Panel_PlayerStatus : MonoBehaviour {
    [SerializeField] Text coinCount_Txt;
    [SerializeField] Image img_coin;
    [SerializeField] Transform itemGroup;
    [SerializeField] Panel_PlayerElement prefab;
    Dictionary<int, Panel_PlayerElement> elements;

    int hpMax;
    [SerializeField] Image img_HpBg;
    [SerializeField] Image img_HpBar;
    [SerializeField] Text txt_hpRate;


    public void Ctor() {
        elements = new Dictionary<int, Panel_PlayerElement>();
    }

    public void Init(int hpMax, int count, int hp) {
        this.hpMax = hpMax;
        InitHPBar(hp);
        txt_hpRate.text = $"{hp}/{hpMax}";

        coinCount_Txt.text = count.ToString();

    }

    public void InitHPBar(int hp) {
        float rate = (float)hp / hpMax;
        img_HpBar.fillAmount = rate;
    }

    public void NewElement(int id, Sprite sprite) {
        bool has = elements.TryGetValue(id, out var el);
        if (has) {
            return;
        }
        var element = GameObject.Instantiate(prefab, itemGroup);
        element.Ctor(sprite);
        elements.Add(id, element);
    }

}