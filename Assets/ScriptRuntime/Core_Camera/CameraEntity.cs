using UnityEngine;
using Cinemachine;

public class CameraEntity {
    public CinemachineVirtualCamera curCamera;
    public Camera mainCamera;

    public void Inject(CinemachineVirtualCamera current, Camera camera) {
        this.curCamera = current;
        this.mainCamera = camera;
    }

    public void SetConfiner(PolygonCollider2D confiner) {
        curCamera.GetComponent<CinemachineConfiner>().m_BoundingShape2D = confiner;
    }

    public void SetFollow(Transform target) {
        curCamera.Follow = target;
    }

    public void SetLookAt(Transform target) {
        curCamera.LookAt = target;
    }

    public Vector2 Pos() {
        return mainCamera.transform.position;
    }
}