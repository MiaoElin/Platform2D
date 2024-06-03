using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Panel_PlayerStatus : MonoBehaviour {
    [SerializeField] Text coinCount_Txt;
    [SerializeField] Image img_coin;
    [SerializeField] Transform itemGroup;
    [SerializeField] Panel_PlayerElement prefab;
    Dictionary<int, Panel_PlayerElement> elements;

    public void Ctor() {
        elements = new Dictionary<int, Panel_PlayerElement>();
    }

    public void Init(int count) {
        coinCount_Txt.text = count.ToString();
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