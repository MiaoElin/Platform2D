using UnityEngine;
using System.Collections.Generic;

public class RobotComponent {

    Dictionary<int, Transform> all;
    Transform robotGroup;

    public RobotComponent() {
        all = new Dictionary<int, Transform>();
    }

    public void SetRobotPoint(Transform robotPoint) {
        this.robotGroup = robotPoint;
    }

    public void Add(int id) {
        var trans = GameObject.Instantiate(robotGroup.gameObject, robotGroup).transform;
        trans.localPosition = new Vector3(all.Count * 1.5f, 0, 0);
        all.Add(id, trans);
    }

    public Vector2 GetRobotPositon(int id) {
        all.TryGetValue(id, out var trans);
        return trans.position;
    }
}