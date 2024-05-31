using UnityEngine;

public static class RoleDomain {

    public static RoleEntity Spawn(GameContext ctx, int typeID, Vector2 pos, Ally ally) {
        var role = GameFactory.Role_Spawn(ctx, typeID, pos, ally);
        ctx.roleRepo.Add(role);
        return role;
    }

    public static void Move(GameContext ctx, RoleEntity role) {
        if (role.moveType == MoveType.ByInput) {
            role.Move(ctx.input.moveAxis);
            role.Anim_Move();
        }
    }

    public static void Jump(GameContext ctx, RoleEntity role) {
        role.Jump();
    }

    public static void Falling(RoleEntity role, float dt) {
        role.Falling(dt);
    }

    public static void CheckGround(GameContext ctx, RoleEntity role) {
        if (role.GetVelocityY() > 0) {
            return;
        }

        // Ground:3/Trampoline:6
        var layerMask = 1 << 3 | 1 << 6;
        Collider2D[] hits = Physics2D.OverlapBoxAll(role.Pos() + Vector2.down * role.height / 2, new Vector2(0.98f, 0.1f), 0, layerMask);
        if (hits.Length == 0) {
            return;
        }
        foreach (var hit in hits) {
            if (hit.gameObject.layer == 3) {
                role.ReuseJumpTimes();
                role.Anim_JumpEnd();
            } else if (hit.gameObject.layer == 6) {
                var prop = hit.GetComponentInParent<PropEntity>();
                prop.isOwnerOnTrampoline = true;
            }
        }
    }

}