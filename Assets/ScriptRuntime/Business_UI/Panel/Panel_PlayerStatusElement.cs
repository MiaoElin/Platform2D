using UnityEngine;
using UnityEngine.UI;

public class Panel_PlayerElement : MonoBehaviour {
    [SerializeField] Image img_item;
    public int typeID;
    int count;
    [SerializeField] Text txt_Count;
    public void Ctor(int typeID, Sprite item, int count) {
        this.typeID = typeID;
        img_item.sprite = item;
        this.count = count;
        txt_Count.text = $"X{count}";
    }

    public void Init(int count) {
        this.count += count;
        txt_Count.text = $"X{count}";
    }


}