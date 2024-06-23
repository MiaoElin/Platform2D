using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;

public class ClientMain : MonoBehaviour {
    [SerializeField] Canvas screenCanvas;
    [SerializeField] Canvas hudCanvas;
    [SerializeField] CinemachineVirtualCamera mainCamera;
    GameContext ctx = new GameContext();
    bool isTearDown;

    // Start is called before the first frame update
    void Start() {

        ctx.Inject(mainCamera, screenCanvas, hudCanvas);

        LoadAll();

        // PoolSercive
        ctx.poolService.Init(
        () => GameFactory.Role_Create(ctx),
        () => GameFactory.Prop_Create(ctx),
        () => GameFactory.Loot_Create(ctx),
        () => new BuffSubEntity(),
        () => GameFactory.Bullet_Create(ctx));

        // EventBind
        EventBind();

        // Physics
        Physics2D.IgnoreLayerCollision(LayerConst.ROLE, LayerConst.ROLE);


        GameBusiness_Login.Enter(ctx);

        // GameBusiness_Normal.EnterStage(ctx);

    }

    public void EventBind() {
        var eventCenter = ctx.eventCenter;

        // Login
        eventCenter.OnStartGameHandle = () => {
            GameBusiness_Normal.EnterStage(ctx);
            UIDomain.Panel_Login_Hide(ctx);
        };

        eventCenter.OnExitGameHandle = () => {
#if UNITY_EDITOR    //在编辑器模式下
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        };

        eventCenter.OnStopBossWaveHande = () => {
            ctx.player.isEnterBossTime = false;
        };

        // Result
        eventCenter.OnRestartHandle = () => {
            // 销毁场景里所有的东西
            GameGameDomain.ExitGame(ctx);
            // 关闭结果页
            UIDomain.Panel_Result_Close(ctx);
            // 重新enterStage（）
            Time.timeScale = 1;
            GameBusiness_Normal.EnterStage(ctx);
        };
    }

    private void LoadAll() {
        ctx.asset.LoadAll();
        ctx.soundCore.LoadAll();
    }

    // Update is called once per frame
    public void TearDown() {
        if (isTearDown) {
            return;
        }
        ctx.asset.Unload();
        ctx.soundCore.Unload();

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

        var status = ctx.game.fsm.status;
        if (status == GameStatus.Login) {
            GameBusiness_Login.Tick(ctx);
        } else if (status == GameStatus.Normal) {
            GameBusiness_Normal.Tick(ctx, dt);
        }

    }
}
