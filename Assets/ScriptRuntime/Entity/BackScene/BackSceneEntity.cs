using UnityEngine;

public class BackSceneEntity : MonoBehaviour {

    [SerializeField] SpriteRenderer bg;
    [SerializeField] SpriteRenderer mid;
    [SerializeField] SpriteRenderer front;

    public void Ctor(Sprite bg, Sprite mid, Sprite front) {
        this.bg.sprite = bg;
        this.mid.sprite = mid;
        this.front.sprite = front;
    }

    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }
}