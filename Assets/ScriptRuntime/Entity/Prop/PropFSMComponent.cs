using UnityEngine;

public class PropFSMComponent {
    public PropStatus propStatus;
    public bool isEnterNormal;

    public bool isEterReborn;

    public bool isEnterFadeOut;

    public void EnterNormal() {
        propStatus = PropStatus.Normal;
        isEnterNormal = true;
    }
}