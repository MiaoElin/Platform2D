using UnityEngine;

public class MapEntity : MonoBehaviour {
    public int stageID;
    public Sprite backSceneBG;
    public Sprite backSceneMid;
    public Sprite backSceneFront;
    Grid grid;
    public propSpawnerTM[] propSpawnerTMs;
    public LootSpawnerTM[] lootSpawnerTMs;
    public RoleSpawnerTM[] roleSpawnerTMs;

    public AudioClip bgm;
    public float bgmVolume;

    public GridComponent gridCom;

    public void Ctor(int stageID, Grid grid, int xCount, int yCount) {
        this.stageID = stageID;
        this.grid = grid;
        gridCom = new GridComponent();
        gridCom.Ctor(xCount, yCount);
    }

}