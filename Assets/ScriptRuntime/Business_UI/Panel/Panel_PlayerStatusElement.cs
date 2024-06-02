using UnityEngine;
using UnityEngine.UI;

public class Panel_PlayerElement : MonoBehaviour {
    [SerializeField] Image img_item;

    public void Ctor(Sprite item) {
        img_item.sprite = item;
    }


}