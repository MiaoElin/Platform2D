using UnityEngine;
using System.Collections.Generic;

public class HUD_HPBarRepo {

    Dictionary<int, HUD_HPBar> all;

    public HUD_HPBarRepo() {
        all = new Dictionary<int, HUD_HPBar>();
    }

    public void Add(int id, HUD_HPBar hud) {
        all.Add(id, hud);
    }

    public void Remove(int id) {
        all.Remove(id);
    }

    public bool TryGet(int id, out HUD_HPBar hud) {
        return all.TryGetValue(id, out hud);
    }
}