using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMain : MonoBehaviour {
    [SerializeField] Canvas screenCanvas;
    [SerializeField] Camera mainCamera;
    GameContext ctx = new GameContext();
    bool isTearDown;

    // Start is called before the first frame update
    void Start() {


        ctx.Inject(mainCamera, screenCanvas);

        LoadAll();

        ctx.poolService.Init(() => GameFactory.Role_Create(ctx));

        GameBusiness_Normal.EnterStage(ctx);

    }

    private void LoadAll() {
        ctx.asset.LoadAll();
    }

    // Update is called once per frame
    public void TearDown() {
        if (isTearDown) {
            return;
        }
        ctx.asset.Unload();
        isTearDown = true;
    }

    void OnDestory() {
        TearDown();
    }

    void OnApplicationQuit() {
        TearDown();
    }

    void Update() {
        ctx.input.Process();

        GameBusiness_Normal.Tick(ctx);
    }
}
