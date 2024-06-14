using UnityEngine;

public class PropFSMComponent {
    public PropStatus status;
    public bool isEnterNormal;

    public bool isEnterHurt;

    public bool isEnterFadeOut;
    public float fadeOutTimer;

    public void EnterNormal() {
        status = PropStatus.Normal;
        isEnterNormal = true;
    }

    public void EnterFadeOut() {
        status = PropStatus.FadeOut;
        isEnterFadeOut = true;
    }

    public void EnterHurt() {
        status = PropStatus.Hurt;
        isEnterHurt = true;
    }
}