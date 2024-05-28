using UnityEngine;

public class MapEntity : MonoBehaviour {
    public int stageID;
    Grid grid;

    public void Ctor(int stageID, Grid grid) {
        this.stageID = stageID;
        this.grid = grid;
    }
}