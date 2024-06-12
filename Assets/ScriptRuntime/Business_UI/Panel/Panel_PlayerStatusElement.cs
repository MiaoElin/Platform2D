using UnityEngine;
using UnityEngine.UI;

public class Panel_PlayerElement : MonoBehaviour {
    [SerializeField] Image img_item;
    public int typeID;
    public int count;
    public void Ctor(int typeID, Sprite item) {
        this.typeID = typeID;
        img_item.sprite = item;
    }


}