using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Panel_PlayerStatus : MonoBehaviour {
    [SerializeField] Text coinCount_Txt;
    [SerializeField] Image img_coin;
    [SerializeField] Transform itemGroup;
    [SerializeField] Panel_PlayerElement prefab;
    List<Panel_PlayerElement> elements;

    public void Ctor() {
        elements = new List<Panel_PlayerElement>();
    }

    public void Init(int count) {
        coinCount_Txt.text = count.ToString();
        
    }

}