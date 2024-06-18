using UnityEngine;
using UnityEngine.UI;

public class Panel_PlayerElement : MonoBehaviour {
    [SerializeField] Image img_item;
    public int typeID;
    [SerializeField] Text txt_Count;
    public void Ctor(int typeID, Sprite item, int count) {
        this.typeID = typeID;
        img_item.sprite = item;
        txt_Count.text = $"X{count}";
    }

    public void Init(int count) {
        txt_Count.text = $"X{count}";
    }


}