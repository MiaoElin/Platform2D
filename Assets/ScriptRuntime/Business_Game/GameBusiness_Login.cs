using UnityEngine;

public static class GameBusiness_Login {

    public static void Enter(GameContext ctx) {
        UIDomain.Panel_Login_Open(ctx);
        ctx.game.fsm.EnterLogin();

        // BackScene
        ConfigTM configTM = ctx.asset.configTM;
        var backScene = GameFactory.BackScene_Create(ctx);
        backScene.Ctor(configTM.backSceneBG, configTM.backSceneMid, configTM.backSceneFront);
        ctx.backScene = backScene;
    }

    public static void Tick(GameContext ctx) {
        // LayerMask btn = LayerConst.BTN;
        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // bool has = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero, 10, btn);
        // Debug.Log(has);
        // if (has) {
        //     Debug.Log("Play");
        //     SFXDomain.BTN_Interact_Play(ctx);
        // }
        SFXDomain.Login_BGM_Play(ctx);
    }
}