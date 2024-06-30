using UnityEngine;

public class GameFSMComponent {

    public GameStatus status;

    public bool isEnterLogin;

    public bool isEnterNormal;

    public bool isEnterNextStage;

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
}