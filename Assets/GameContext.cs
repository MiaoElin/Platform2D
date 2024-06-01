using UnityEngine;
using Cinemachine;

public class GameContext {

    public float resetTime;
    public PoolService poolService;
    public IDService iDService;

    // === Core ===
    public UIApp uIApp;
    public Asset_Core asset;

    // === Repo ===
    public RoleRepo roleRepo;
    public MapRepo mapRepo;
    public PropRepo propRepo;
    public LootRepo lootRepo;

    // === Entity ===
    public InputEntity input;
    public CameraEntity camera;
    public BackSceneEntity backScene;

    // EventCenter
    public EventCenter eventCenter;

    public int ownerID;
    public int currentStageID;

    public GameContext() {
        // Service
        poolService = new PoolService();
        iDService = new IDService();
        // Core
        uIApp = new UIApp();
        asset = new Asset_Core();
        // Repo
        roleRepo = new RoleRepo();
        mapRepo = new MapRepo();
        propRepo = new PropRepo();
        lootRepo = new LootRepo();
        // Entity
        input = new InputEntity();
        camera = new CameraEntity();
        // EventCenter
        eventCenter = new EventCenter();
    }
    public void Inject(CinemachineVirtualCamera camera, Canvas screenCanvas) {
        this.camera.Inject(camera);
        this.uIApp.Inject(asset, screenCanvas);
    }

    public RoleEntity GetOwner() {
        roleRepo.TryGet(ownerID, out var role);
        return role;
    }

    public MapEntity GetCurrentMap() {
        mapRepo.TryGet(currentStageID, out var map);
        return map;
    }
}