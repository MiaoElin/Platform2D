using System;
using UnityEngine;

public static class PropFsmController {

    public static void ApplyFsm(GameContext ctx, PropEntity prop, float dt) {
        var fsm = prop.fsm;
        var status = fsm.propStatus;
        if (status == PropStatus.Normal) {
            ApplyNormal(ctx, prop, dt);
        } else if (status == PropStatus.FadeOut) {
            ApplyFadeOut(ctx, prop, dt);
        }
    }

    private static void ApplyFadeOut(GameContext ctx, PropEntity prop, float dt) {

    }

    private static void ApplyNormal(GameContext ctx, PropEntity prop, float dt) {
        var fsm = prop.fsm;
        var owenr = ctx.GetOwner();
        if (fsm.isEnterNormal) {
            fsm.isEnterNormal = false;
        }
        // 反射板
        if (prop.isTrampoline) {
            if (prop.isOwnerOnTrampoline) {
                prop.isOwnerOnTrampoline = false;
                owenr.SetVelocityY(prop.jumpForce);
                owenr.fsm.EnterTrampoline();
            }
        }

        // 楼梯 
        var pos = owenr.Pos();
        if (prop.isLadder) {
            Vector2 lowPos = prop.Pos() + Vector2.down * (prop.size.y / 2) + Vector2.left * prop.size.x / 2;
            Vector2 hightPos = prop.Pos() + Vector2.up * (prop.size.y / 2) + Vector2.right * prop.size.x / 2;

            float lowestY = lowPos.y + owenr.height / 2;
            float highestY = hightPos.y + owenr.height / 2 + 0.2f; // 0.2f作为编辑器的偏差量。角色爬到顶上高一点再落地，也会有种缓动的感觉

            // 限制x的范围
            if (pos.x > lowPos.x && pos.x < hightPos.x) {
                // 往上爬的Y范围
                if (pos.y + owenr.height / 2 > lowPos.y && pos.y < hightPos.y) {
                    if (ctx.input.moveAxis.y > 0) {
                        owenr.fsm.EnterLadder(lowestY, highestY);
                    }
                    // 往下爬的Y的范围 
                } else if (pos.y > hightPos.y && pos.y < highestY) {   // 大于highest 到达顶部，collider变硬、 小于是开始下降
                    if (ctx.input.moveAxis.y < 0) {
                        owenr.fsm.EnterLadder(lowestY, highestY);
                    }
                }
            }
        }

        // hurt fire
        if (prop.isHurtFire) {
            ref var timer = ref prop.hurtFireTimer;
            timer -= dt;
            if (timer <= 0) {
                timer = prop.hurtFireDuration;
                owenr.hp -= (int)prop.hurtFireDamageRate * CommonConst.BASEDAMAGE;
            }
        }
    }
}