using UnityEngine;

public class PropFSMComponent {
    public PropStatus propStatus;
    public bool isEnterNormal;

    public bool isEnterHurt;

    public bool isEnterFadeOut;
    public float fadeOutTimer;

    public void EnterNormal() {
        propStatus = PropStatus.Normal;
        isEnterNormal = true;
    }

    public void EnterFadeOut() {
        propStatus = PropStatus.FadeOut;
        isEnterFadeOut = true;
    }

    public void EnterHurt() {
        propStatus = PropStatus.Hurt;
        isEnterHurt = true;
    }
}