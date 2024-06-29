using UnityEngine;

public class BackSceneEntity : MonoBehaviour {

    // [SerializeField] SpriteRenderer bg;
    // [SerializeField] SpriteRenderer mid;
    // [SerializeField] SpriteRenderer front;
    [SerializeField] MeshRenderer mesh_BG;
    [SerializeField] MeshRenderer mesh_Mid;
    [SerializeField] MeshRenderer mesh_Front;

    public float moveSpeed_BG;
    public float moveSpeed_Mid;
    public float moveSpeed_Front;
    Vector2 bg_Offset;
    Vector2 mid_Offset;
    Vector2 front_Offset;



    public void Ctor(Sprite bg, Sprite mid, Sprite front) {
        // this.bg.sprite = bg;
        // this.mid.sprite = mid;
        // this.front.sprite = front;
        mesh_BG.materials[0].mainTexture = bg.texture;
        mesh_Mid.materials[0].mainTexture = mid.texture;
        mesh_Front.materials[0].mainTexture = front.texture;
    }

    public void Tick(Vector2 moveAxis, float dt) {
        bg_Offset.x += moveAxis.x * moveSpeed_BG * dt;
        // bg_Offset.y += moveAxis.y * moveSpeed_BG * dt;

        mid_Offset.x += moveAxis.x * moveSpeed_Mid * dt;
        // mid_Offset.y += moveAxis.y * moveSpeed_Mid * dt;

        front_Offset.x += moveAxis.x * moveSpeed_Front * dt;
        // front_Offset.y += moveAxis.y * moveSpeed_Front * dt;

        mesh_BG.material.mainTextureOffset = bg_Offset;
        mesh_Mid.material.mainTextureOffset = mid_Offset;
        mesh_Front.material.mainTextureOffset = front_Offset;
    }

    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }
}