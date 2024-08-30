using UnityEngine;

public class GameFSMComponent {

    public GameStatus status;

    public bool isEnterLogin;

    public bool isEnterNormal;

    public bool isEnterNextStage;

    public bool isEnterPause;

    public void EnterLogin() {
        status = GameStatus.Login;
        isEnterLogin = true;
    }

    public void EnterNormal() {
        status = GameStatus.Normal;
        isEnterNormal = true;
    }

    public void EnterNextStage() {
        status = GameStatus.EnterNextStage;
        isEnterNextStage = true;
    }

    public void EnterPause() {
        status = GameStatus.Pause;
        isEnterPause = true;
    }
}