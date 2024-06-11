using System;
using UnityEngine;

public class VFXModel : MonoBehaviour {

    [SerializeField] SpriteRenderer sr;

    Sprite[] sprites;
    public int currentIndex;
    public bool isEnd;
    float timer;

    public void Init(Sprite[] sprites) {
        currentIndex = 0;
        this.sprites = sprites;
        sr.sprite = sprites[currentIndex];
        isEnd = false;
    }

    public void Tick(float dt) {
        if (isEnd) {
            return;
        }
        timer += dt;
        if (timer >= CommonConst.VFXFPS) {
            timer -= CommonConst.VFXFPS;
            currentIndex++;
            if (currentIndex >= sprites.Length) {
                isEnd = true;
                return;
            }
            sr.sprite = sprites[currentIndex];
        }
    }

    internal void SetPos(Vector2 pos) {
        transform.position = pos;
    }
}