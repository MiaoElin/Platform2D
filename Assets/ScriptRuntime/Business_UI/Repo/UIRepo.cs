using UnityEngine;
using System.Collections.Generic;

public class UIRepo {

    Dictionary<string, GameObject> uis;

    public UIRepo() {
        uis = new Dictionary<string, GameObject>();
    }

    public void Add(string name, GameObject ui) {
        uis.Add(name, ui);
    }

    public T TryGet<T>() where T : MonoBehaviour {
        bool has = uis.TryGetValue(typeof(T).Name, out var ui);
        T t;
        if (has) {
            t = ui.GetComponent<T>();
        } else {
            t = null;
        }
        return t;
    }

    public void Remove(string name) {
        uis.Remove(name);
    }
}