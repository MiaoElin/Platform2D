using UnityEngine;

[CreateAssetMenu(menuName = "TM/TM_Map", fileName = "TM_Map_")]
public class MapTM : ScriptableObject {

    public int stage;
    public Grid grid;

    public int xCount;
    public int yCount;
    public Sprite backSceneBG;
    public Sprite backSceneMid;
    public Sprite backSceneFront;

    public propSpawnerTM[] propSpawnerTMs;
    public LootSpawnerTM[] lootSpawnerTMs;
    public RoleSpawnerTM[] roleSpawnerTMs;

    public AudioClip bgm;
    public float bgmVolume;
    public Vector2 ownerEnterPos;
    public float bossWaveDuration;

}