using UnityEngine;

public class LootFSMComponent {

    public LootStatus status;

    public bool isEnterNormal;

    public bool isEnterUsed;

    public void EnterNormal() {
        status = LootStatus.Normal;
        isEnterNormal = true;
    }

    public void EnterUsed() {
        status = LootStatus.Used;
        isEnterUsed = true;
    }
}