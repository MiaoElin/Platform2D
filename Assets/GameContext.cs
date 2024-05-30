using UnityEngine;
using Cinemachine;

public class GameContext {

    public float resetTime;
    public PoolService poolService;
    public IDService iDService;

    // === Core ===
    public Asset_Core asset;

    // === Repo ===
    public RoleRepo roleRepo;
    public MapRepo mapRepo;

    // === Entity ===
    public InputEntity input;
    public CameraEntity camera;
    public BackSceneEntity backScene;

    public int ownerID;

    public GameContext() {
        // Service
        poolService = new PoolService();
        iDService = new IDService();
        // Core
        asset = new Asset_Core();
        // Repo
        roleRepo = new RoleRepo();
        mapRepo = new MapRepo();
        // Entity
        input = new InputEntity();
        camera = new CameraEntity();
    }
    public void Inject(CinemachineVirtualCamera camera, Canvas screenCanvas) {
        this.camera.Inject(camera);
    }

    public RoleEntity GetOwner() {
        roleRepo.TryGet(ownerID, out var role);
        return role;
    }
}