using UnityEngine;

public class GameContext {

    public PoolService poolService;
    public IDService iDService;

    // === Core ===
    public Asset_Core asset;

    // === Repo ===
    public RoleRepo roleRepo;
    // === Entity ===
    public InputEntity input;

    public int ownerID;

    public GameContext() {
        poolService = new PoolService();
        iDService = new IDService();
        asset = new Asset_Core();
        roleRepo = new RoleRepo();
        input = new InputEntity();
    }
    public void Inject(Camera camera, Canvas screenCanvas) {

    }

    public RoleEntity GetOwner() {
        roleRepo.TryGet(ownerID, out var role);
        return role;
    }
}