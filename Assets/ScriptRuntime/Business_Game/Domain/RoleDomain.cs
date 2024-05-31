using UnityEngine;

public static class RoleDomain {

    public static RoleEntity Spawn(GameContext ctx, int typeID, Vector2 pos, Ally ally) {
        var role = GameFactory.Role_Spawn(ctx, typeID, pos, ally);
        ctx.roleRepo.Add(role);
        return role;
    }

    public static void Move(GameContext ctx, RoleEntity role) {
        role.Move(ctx.input.moveAxis);
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
        bool hasLadder = false;
        bool isOnGround = false;
        // Ground:3/Trampoline:6/Ladder:7
        var layerMask = 1 << 3 | 1 << 6 | 1 << 7;
        Collider2D[] hits = Physics2D.OverlapBoxAll(role.Pos() + Vector2.down * role.height / 2, new Vector2(0.98f, 0.1f), 0, layerMask);
        if (hits.Length == 0) {
        }
        foreach (var hit in hits) {
            if (hit.gameObject.layer == 3) {
                role.ReuseJumpTimes();
                role.Anim_JumpEnd();
                isOnGround = true;
            } else if (hit.gameObject.layer == 6) {
                var prop = hit.GetComponentInParent<PropEntity>();
                prop.isOwnerOnTrampoline = true;
            } else if (hit.gameObject.layer == 7) {
                var prop = hit.GetComponentInParent<PropEntity>();
                if (hit)
                    hasLadder = true;
                prop.isOwnerOnLadder = true;
            }
        }

        bool headInLadder = false;
        bool headInGround = false;
        var Ladderlayer = 1 << 7;
        var ladderHit = Physics2D.OverlapBox(role.Pos(), new Vector2(0.98f, 0.1f), 0, Ladderlayer);
        if (ladderHit) {
            headInLadder = true;
        }
        var owenr = ctx.GetOwner();
    
        if (hasLadder) {

            if (ctx.input.moveAxis.y < 0 && isOnGround && headInLadder || ctx.input.moveAxis.y > 0 && isOnGround && !headInLadder) {
                owenr.moveType = MoveType.ByAxix;
                ctx.GetCurrentMap().SetGridCollision();
            } else if (ctx.input.moveAxis.y < 0 && isOnGround && !headInLadder || headInGround || ctx.input.moveAxis.y > 0 && isOnGround && headInLadder || ctx.input.moveAxis.x == 0 && !isOnGround) {
                owenr.moveType = MoveType.ByAxiy;
                ctx.GetCurrentMap().SetGridTrigger();
            } else {
                owenr.moveType = MoveType.ByAxix;
                ctx.GetCurrentMap().SetGridCollision();

            }
        } else {
            if (headInGround) {
                owenr.moveType = MoveType.ByAxiy;
                ctx.GetCurrentMap().SetGridTrigger();
            } else {
                owenr.moveType = MoveType.ByAxix;
                ctx.GetCurrentMap().SetGridCollision();
            }
        }
    }

}