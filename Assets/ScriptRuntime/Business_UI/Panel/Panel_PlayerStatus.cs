using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Panel_PlayerStatus : MonoBehaviour {

    // Coin
    [SerializeField] Text coinCount_Txt;
    [SerializeField] Image img_coin;

    // BuffGroup
    [SerializeField] Transform itemGroup;
    [SerializeField] Panel_PlayerElement prefab;
    Dictionary<int, Panel_PlayerElement> elements;

    // HPBar
    int hpMax;
    int shield;
    [SerializeField] Image img_HpBg;
    [SerializeField] Image img_HpBar;
    [SerializeField] Image img_Shield;
    [SerializeField] Text txt_hpRate;

    // BossWaveTimer
    [SerializeField] Text txt_BossWavetimer;
    float time;
    float duration;
    public bool isEnterBossWave;
    public Action OnStopCreateBossWaveHandle;
    public void Ctor() {
        elements = new Dictionary<int, Panel_PlayerElement>();
        time = 0;
        duration = 10;
    }

    public void Init(int hpMax, int shield, int count, int hp, float dt) {
        // Hpbar
        this.hpMax = hpMax;
        this.shield = shield;
        InitHPBar(hp, shield);
        txt_hpRate.text = $"{hp + shield}/{hpMax + shield}";
        // Coin
        coinCount_Txt.text = count.ToString();
        // BossWaveTimer
        if (isEnterBossWave) {
            time += dt;
            int txt_time = (int)time;
            txt_BossWavetimer.text = $"{txt_time}/{duration}";
            if (time >= duration) {
                OutBossWave();
            }
        }



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

    internal void EnterBossWave() {
        isEnterBossWave = true;
        txt_BossWavetimer.gameObject.SetActive(true);
    }

    public void OutBossWave() {
        isEnterBossWave = false;
        txt_BossWavetimer.gameObject.SetActive(false);
        OnStopCreateBossWaveHandle.Invoke();
    }

    public void NewElement(int typeID, Sprite sprite, int count) {
        bool has = elements.TryGetValue(typeID, out var el);
        if (has) {
            el.Init(count);
            return;
        }
        var element = GameObject.Instantiate(prefab, itemGroup);
        element.Ctor(typeID, sprite, count);
        elements.Add(typeID, element);
    }

}