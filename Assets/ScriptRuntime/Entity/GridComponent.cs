using UnityEngine;

public class GridComponent {

    int xCount;
    int yCount;
    Vector2Int[] grids;

    public void Ctor(int xCount, int yCount) {
        this.xCount = xCount;
        this.yCount = yCount;
        grids = new Vector2Int[xCount * yCount];
        int index = 0;
        for (int x = -xCount / 2; x < xCount / 2; x++) {
            for (int y = -yCount / 2; y < yCount / 2; y++) {
                grids[index++] = new Vector2Int(x, y);
            }
        }
    }

    public void FindNearlyGrid(Vector2 pos) {

    }
}