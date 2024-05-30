using UnityEngine;

public class MapEntity : MonoBehaviour {
    public int stageID;
    public Sprite backSceneBG;
    public Sprite backSceneMid;
    public Sprite backSceneFront;
    Grid grid;
    public propSpawnerTM[] propSpawnerTMs;

    public void Ctor(int stageID, Grid grid) {
        this.stageID = stageID;
        this.grid = grid;
    }

}