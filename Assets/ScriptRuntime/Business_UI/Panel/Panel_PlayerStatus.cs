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
    int shield;
    [SerializeField] Image img_HpBg;
    [SerializeField] Image img_HpBar;
    [SerializeField] Image img_Shield;
    [SerializeField] Text txt_hpRate;


    public void Ctor() {
        elements = new Dictionary<int, Panel_PlayerElement>();
    }

    public void Init(int hpMax, int shield, int count, int hp) {
        this.hpMax = hpMax;
        this.shield = shield;
        InitHPBar(hp, shield);
        txt_hpRate.text = $"{hp + shield}/{hpMax + shield}";

        coinCount_Txt.text = count.ToString();

    }

    public void InitHPBar(int hp, int shield) {

        float width_HpBar = ((float)hp / (hpMax + shield)) * 200;
        var size = img_HpBar.rectTransform.sizeDelta;
        size.x = width_HpBar;
        img_HpBar.rectTransform.sizeDelta = size;

        float width_Shield = ((float)shield / (hpMax + shield)) * 200;
        var size_Shield = img_Shield.rectTransform.sizeDelta;
        size_Shield.x = width_Shield;
        img_Shield.rectTransform.sizeDelta = size_Shield;
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