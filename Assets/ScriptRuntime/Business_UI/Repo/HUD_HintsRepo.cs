using UnityEngine;
using System.Collections.Generic;

public class HUD_HintsRepo {

    Dictionary<ulong, HUD_Hints> all;

    public HUD_HintsRepo() {
        all = new Dictionary<ulong, HUD_Hints>();
    }

    public void Add(ulong typeAndID, HUD_Hints ui) {
        all.Add(typeAndID, ui);
    }

    public bool TryGet(ulong typeAndID, out HUD_Hints hud) {
        return all.TryGetValue(typeAndID, out hud);
    }

    public void Remove(ulong typeAndID) {
        all.Remove(typeAndID);
    }
}