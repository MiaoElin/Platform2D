using UnityEngine;
using Cinemachine;

public class CameraEntity {
    public CinemachineVirtualCamera curCamera;

    public void Inject(CinemachineVirtualCamera current) {
        this.curCamera = current;
    }

    public void SetFollow(Transform target) {
        curCamera.Follow = target;
    }

    public void SetLookAt(Transform target) {
        curCamera.LookAt = target;
    }
}