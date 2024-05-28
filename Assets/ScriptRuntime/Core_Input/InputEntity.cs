using UnityEngine;

public class InputEntity {

    public Vector2 moveAxis;

    public bool isJumpKeyDown;

    public void Process() {
        moveAxis = Vector2.zero;
        if (Input.GetKey(KeyCode.A)) {
            moveAxis.x = -1;
        } else if (Input.GetKey(KeyCode.D)) {
            moveAxis.x = 1;
        }

        if (Input.GetKey(KeyCode.W)) {
            moveAxis.y = 1;
        } else if (Input.GetKey(KeyCode.W)) {
            moveAxis.y = -1;
        }
        moveAxis.Normalize();

        // Jump
        isJumpKeyDown = Input.GetKeyDown(KeyCode.Space);
    }
}