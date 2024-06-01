using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ClientMain : MonoBehaviour {
    [SerializeField] Canvas screenCanvas;
    [SerializeField] CinemachineVirtualCamera mainCamera;
    GameContext ctx = new GameContext();
    bool isTearDown;

    // Start is called before the first frame update
    void Start() {


        ctx.Inject(mainCamera, screenCanvas);

        LoadAll();

        ctx.poolService.Init(() => GameFactory.Role_Create(ctx), () => GameFactory.Prop_Create(ctx), () => GameFactory.Loot_Create(ctx));

        EventBind();

        GameBusiness_Normal.EnterStage(ctx);

    }

    public void EventBind() {
        var eventCenter = ctx.eventCenter;
        eventCenter.openP_HintsHandle += (Vector2 pos) => {
            pos += Vector2.up * 3;
            Debug.Log(pos);
            Vector2 screenPos = Camera.main.WorldToScreenPoint(pos);
            UIDomain.Panel_Hints_Open(ctx, screenPos);
        };
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

        var dt = Time.deltaTime;

        GameBusiness_Normal.Tick(ctx, dt);
    }
}
