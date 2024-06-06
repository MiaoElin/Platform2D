using UnityEngine;

public static class PureFunction {

    public static bool IsInRange(Vector2 target, Vector2 pos, float range) {
        float distance = Vector2.SqrMagnitude(target - pos);
        if (distance <= range * range) {
            return true;
        } else {
            return false;
        }
    }
}