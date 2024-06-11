using UnityEngine;
using UnityEngine.UI;
using GameFunctions;

public class HUD_HurtInfo : MonoBehaviour {

    [SerializeField] Text txt_hurtInfo;
    public float time;
    public float duration;
    public bool isTearDown;
    public Vector2 startPos;

    public void SetPos(Vector2 pos) {
        transform.position = pos;
        startPos = pos;
    }

    public void Init(int hurtInfo) {
        isTearDown = false;
        txt_hurtInfo.text = $"-{hurtInfo.ToString()}";
    }

    public void Easing(float dt) {

        if (time > duration) {
            time = 0;
            isTearDown = true;
        } else {
            transform.position = GFEasing.Ease2D(GFEasingEnum.MountainInCirc, time, duration + 0.2f, startPos, startPos + Vector2.up * 3);
            var scale = GFEasing.Ease2D(GFEasingEnum.Linear, time, duration, Vector2.one, Vector2.zero);
            transform.localScale = scale;
            time += dt;
        }
    }

    public void Close() {
        Destroy(gameObject);
    }

}