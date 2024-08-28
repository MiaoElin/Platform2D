using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;

public class ClientMain : MonoBehaviour {
    [SerializeField] Canvas screenCanvas;
    [SerializeField] Canvas hudCanvas;
    [SerializeField] CinemachineVirtualCamera cmCamera;
    [SerializeField] Camera mainCamera;
    GameContext ctx = new GameContext();
    bool isTearDown;

    // Start is called before the first frame update
    void Start() {

        ctx.Inject(cmCamera, mainCamera, screenCanvas, hudCanvas);

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
            GameGameDomain.EnterStage(ctx, 1);
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
            GameGameDomain.EnterStage(ctx, ctx.currentStageID);
        };

        // AltarBar
        eventCenter.OnAltarTimeIsEndHandle = (int id) => {
            // 将prop设置为AltarBarFull
            ctx.propRepo.TryGet(id, out var prop);
            prop.isAltarBarFull = true;
            // 打开进入下一关的提示UI
            UIDomain.HUD_Hints_Open(ctx, prop.GetTypeAddID(), prop.Pos(), 0);
            UIDomain.HUD_Hints_ShowHIntIcon(ctx, prop.GetTypeAddID());
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
        } else if (status == GameStatus.EnterNextStage) {
            GameBusiness_EnterNextStage.Tick(ctx);
        }

    }
}
