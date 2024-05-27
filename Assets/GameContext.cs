using UnityEngine;

public class GameContext {

    public PoolService poolService;
    public IDService iDService;

    // === Core ===
    public Asset_Core asset;

    // === Repo ===
    public RoleRepo roleRepo;

    public int ownerID;

    public GameContext() {
        poolService = new PoolService();
        iDService = new IDService();
        asset = new Asset_Core();
        roleRepo = new RoleRepo();
    }
    public void Inject(Camera camera, Canvas screenCanvas) {

    }

    public RoleEntity GetOwner() {
        roleRepo.TryGet(ownerID, out var role);
        return role;
    }
}