using UnityEngine;
using System;

public class EventCenter {
    public Action OnStartGameHandle;
    public void StartGame() {
        OnStartGameHandle.Invoke();
    }
}