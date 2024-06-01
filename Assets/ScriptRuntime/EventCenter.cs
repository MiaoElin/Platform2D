using UnityEngine;
using System;

public class EventCenter {

    public Action<Vector2> openP_HintsHandle;
    public void Owner_OpenP_Hints(Vector2 pos) { openP_HintsHandle.Invoke(pos); }
}