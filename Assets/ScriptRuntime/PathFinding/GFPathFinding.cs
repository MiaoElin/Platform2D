using UnityEngine;
using System.Collections.Generic;

public static class GFPathFinding {

    // 用有序的哈希表存储已经打开的格子
    public static SortedSet<RectCell2D> openSet = new SortedSet<RectCell2D>();
    // 用字典存储打开的格子，便于一次找到
    public static Dictionary<Vector2Int, RectCell2D> openSetDic = new Dictionary<Vector2Int, RectCell2D>();
    public static SortedSet<RectCell2D> closeSet = new SortedSet<RectCell2D>();
    public static Dictionary<Vector2Int, RectCell2D> closeSetDic = new Dictionary<Vector2Int, RectCell2D>();
    const int CELLSIZE = 10;
    public static int xCount;
    public static int yCount;

    readonly static Vector2Int[] neighbors = new Vector2Int[4]{
        // 这里没有优先级
        new Vector2Int(1,0),
        new Vector2Int(0,-1),
        new Vector2Int(-1,0),
        new Vector2Int(0,1)
    };

    // 看情况调用（要不要追逐到 障碍物再停止）
    public static bool IsStart_EndPosInBlockSet(Vector2Int start, Vector2Int end, HashSet<Vector2Int> blockSet) {
        if (blockSet.Contains(start) || blockSet.Contains(end)) {
            return true;
        } else {
            return false;
        }

    }

    public static int Astar(Vector2Int start, Vector2Int end, HashSet<Vector2Int> blockSet, out List<Vector2Int> path) {
        // -1无路
        // 1有路
        path = new List<Vector2Int>();

        openSet.Clear();
        openSetDic.Clear();
        closeSet.Clear();
        closeSetDic.Clear();

        int stepCount = 0;
        // int count = 0;

        RectCell2D startRect = new RectCell2D();
        startRect.Init(start, 0, 0, 0, null);

        openSet.Add(startRect);
        openSetDic.Add(start, startRect);

        // 当有路可走的时候
        while (openSet.Count > 0) {
            // 找到最近的那个，移除
            RectCell2D cur = openSet.Min;
            openSet.Remove(cur);
            openSetDic.Remove(cur.pos);
            closeSet.Add(cur);
            closeSetDic.Add(cur.pos, cur);

            // 遍历四个方向
            for (int i = 0; i < 4; i++) {
                Vector2Int neighborPos = cur.pos + neighbors[i];
                if (blockSet.Contains(neighborPos) || closeSetDic.ContainsKey(neighborPos)) {
                    continue;
                }
                if (neighborPos.x < -xCount / 2 || neighborPos.x >= xCount / 2) {
                    continue;
                }
                if (neighborPos.y < -yCount / 2 || neighborPos.y >= yCount / 2) {
                    continue;
                }
                // 如果到达 
                if (neighborPos == end) {
                    // count = 0;
                    // 从最后一个开始回溯，因为每个格子的父节点只有一个，而子节点可以多个，所以存父节点更好             从头开始生成路径 、、 后面再试，
                    path.Add(end);
                    // count++;
                    path.Add(cur.pos);
                    while (cur.parent != null) {
                        path.Add(cur.parent.pos);
                        cur = cur.parent;
                    }
                    stepCount += 1;
                    return 1;
                }

                // 还没到达 计算这个格子的 g h f
                float gCost = CELLSIZE;
                float hCost = H_Manhattan(neighborPos, end);
                float fCost = gCost + hCost;
                // 判断openSetDic里是否已经有这个格子
                RectCell2D rectNeighbor;
                bool has = openSetDic.TryGetValue(neighborPos, out rectNeighbor);
                if (has) {
                    // 如果格子新的f值小于之前存的，则用新的值覆盖
                    if (fCost < rectNeighbor.fCost) {
                        rectNeighbor.Init(neighborPos, fCost, gCost, hCost, cur);
                    }
                } else {
                    rectNeighbor = new RectCell2D();
                    rectNeighbor.Init(neighborPos, fCost, gCost, hCost, cur);
                    Debug.Assert(rectNeighbor != null);
                    openSet.Add(rectNeighbor);
                    openSetDic.Add(neighborPos, rectNeighbor);
                }
            }
            stepCount++;
        }
        return -1;
    }

    public static float H_Manhattan(Vector2Int cur, Vector2Int end) {
        return CELLSIZE * Mathf.Abs(cur.x - end.x) + Mathf.Abs(cur.y - end.y);
    }
}