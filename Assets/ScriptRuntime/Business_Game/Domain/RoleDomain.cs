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

    public static void CheckGround(GameContext ctx, RoleEntity role) {

        var layerMask = 1 << 3;
        Collider2D[] hits = Physics2D.OverlapBoxAll(role.Pos() + Vector2.down * 0.5f, new Vector2(1, 0.1f), 0, layerMask);

        if (hits.Length == 0) {

        } else {
            role.ReuseJumpTimes();
        }

    }

}