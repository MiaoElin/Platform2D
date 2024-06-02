using UnityEngine;
using System.Collections.Generic;

public class HUD_HintsRepo {

    Dictionary<int, HUD_Hints> all;

    public HUD_HintsRepo() {
        all = new Dictionary<int, HUD_Hints>();
    }

    public void Add(int id, HUD_Hints ui) {
        all.Add(id, ui);
    }

    public bool TryGet(int id, out HUD_Hints hud) {
        return all.TryGetValue(id, out hud);
    }

    public void Remove(int id) {
        all.Remove(id);
    }
}