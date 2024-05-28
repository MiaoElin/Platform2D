using UnityEngine;
using System.Collections.Generic;

public class MapRepo {

    Dictionary<int, MapEntity> all;
    MapEntity[] temp;

    public MapRepo() {
        all = new Dictionary<int, MapEntity>();
        temp = new MapEntity[128];
    }

    public void Add(MapEntity map) {
        all.Add(map.stageID, map);
    }

    public bool TryGet(int typeID, out MapEntity map) {
        return all.TryGetValue(typeID, out map);
    }

}