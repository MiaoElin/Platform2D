using UnityEngine;
using System.Collections.Generic;

public class RobotComponent {

    Dictionary<int, Transform> all;
    Transform robotPoint;

    public RobotComponent() {
        all = new Dictionary<int, Transform>();
    }

    public void SetRobotPoint(Transform robotPoint) {
        this.robotPoint = robotPoint;
    }

    public void Add(int id) {
        var trans = GameObject.Instantiate(robotPoint, robotPoint);
        trans.localPosition = new Vector3(all.Count, 0, 0);
        all.Add(id, trans);
    }

    public Vector2 GetRobotPositon(int id) {
        all.TryGetValue(id, out var trans);
        return trans.position;
    }
}