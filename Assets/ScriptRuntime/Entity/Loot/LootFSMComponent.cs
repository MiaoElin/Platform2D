using System;
using UnityEngine;

public class LootFSMComponent {

    public LootStatus status;

    public bool isEnterEasingIn;
    public bool isEasingIn;
    public float easingIntimer;
    public float easingInduration;
    public Vector2 easingInStartPos;
    public Vector2 easingInEndPos;

    public bool isEnterNormal;

    public bool isEnterUsed;

    public bool isEnterDestroy;

    public void EnterEasingIn(Vector2 starPos) {
        status = LootStatus.EasingIn;
        isEnterEasingIn = true;
        isEasingIn = true;
        easingInStartPos = starPos;
        easingInEndPos = starPos + Vector2.up * 4f;
    }

    public void EnterNormal() {
        status = LootStatus.Normal;
        isEnterNormal = true;
    }

    public void EnterUsed() {
        status = LootStatus.Used;
        isEnterUsed = true;
    }

    internal void EnterDestroy() {
        status = LootStatus.Destroy;
        isEnterDestroy = true;
    }
}