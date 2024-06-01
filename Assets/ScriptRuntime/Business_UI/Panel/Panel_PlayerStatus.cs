using UnityEngine;
using UnityEngine.UI;

public class Panel_PlayerStatus : MonoBehaviour {
    [SerializeField] Text coinCount_Txt;
    [SerializeField] Image img_coin;

    public void Ctor() {
    }

    public void Init(int count) {
        coinCount_Txt.text = count.ToString();
    }
    
}