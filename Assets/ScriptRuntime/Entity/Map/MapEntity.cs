using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class MapEntity : MonoBehaviour {
    public int stageID;
    public Sprite backSceneBG;
    public Sprite backSceneMid;
    public Sprite backSceneFront;
    Grid grid;
    Tilemap tilemap;
    public HashSet<Vector2Int> blockSet;
    public propSpawnerTM[] propSpawnerTMs;
    public LootSpawnerTM[] lootSpawnerTMs;
    public RoleSpawnerTM[] roleSpawnerTMs;

    public AudioClip bgm;
    public float bgmVolume;

    public int xCount;
    public int yCount;

    public Vector2 ownerEnterPos;

    public PolygonCollider2D cameraPolygonCollider;
    // public GridComponent gridCom;

    public void Ctor(int stageID, Grid grid, int xCount, int yCount) {
        this.stageID = stageID;
        this.grid = grid;
        // gridCom = new GridComponent();
        // gridCom.Ctor(xCount, yCount);
        tilemap = grid.transform.Find("Tilemap").GetComponent<Tilemap>();
        blockSet = new HashSet<Vector2Int>();
        for (int i = 0; i < tilemap.size.x; i++) {
            for (int j = 0; j < tilemap.size.y; j++) {
                Vector2Int pos = new Vector2Int(i, j);
                if (tilemap.GetTile((Vector3Int)pos) != null) {
                    blockSet.Add(pos);
                }
            }
        }

        this.cameraPolygonCollider = grid.GetComponentInChildren<PolygonCollider2D>();
    }


}