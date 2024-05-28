using UnityEngine;

public class GameContext {

    public PoolService poolService;
    public IDService iDService;

    // === Core ===
    public Asset_Core asset;

    // === Repo ===
    public RoleRepo roleRepo;
    public MapRepo mapRepo;

    // === Entity ===
    public InputEntity input;

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
    }
    public void Inject(Camera camera, Canvas screenCanvas) {

    }

    public RoleEntity GetOwner() {
        roleRepo.TryGet(ownerID, out var role);
        return role;
    }
}