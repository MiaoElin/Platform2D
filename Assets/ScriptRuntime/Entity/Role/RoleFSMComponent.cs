using UnityEngine;

public class RoleFSMComponent {

    public RoleStatus status;

    public bool isEnterNormal;
    public bool isEnterLadder;
    public float lowestY;
    public float highestY;

    public void EnterNormal() {
        status = RoleStatus.Normal;
        isEnterNormal = true;
    }

    public void EnterLadder(float lowestY, float highestY) {
        status = RoleStatus.Ladder;
        isEnterLadder = true;
        this.lowestY = lowestY;
        this.highestY = highestY;
    }


}