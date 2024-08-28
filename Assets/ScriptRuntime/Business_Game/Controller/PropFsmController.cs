using System;
using UnityEngine;

public static class PropFsmController {

    public static void ApplyFsm(GameContext ctx, PropEntity prop, float dt) {
        var fsm = prop.fsm;
        var status = fsm.status;
        ApplyAny(ctx, prop, dt);
        if (status == PropStatus.Normal) {
            ApplyNormal(ctx, prop, dt);
        } else if (status == PropStatus.Hurt) {
            ApplyHurt(ctx, prop, dt);
        } else if (status == PropStatus.FadeOut) {
            ApplyFadeOut(ctx, prop, dt);
        }
    }

    private static void ApplyAny(GameContext ctx, PropEntity prop, float dt) {
        if (!prop.isPermanent) {
            prop.activeTimer -= dt;
            if (prop.activeTimer <= 0) {
                prop.fsm.EnterFadeOut();
            }
            return;
        }
    }

    private static void ApplyNormal(GameContext ctx, PropEntity prop, float dt) {
        var fsm = prop.fsm;
        var owenr = ctx.GetOwner();

        PropDomain.Move(prop, dt);

        if (fsm.isEnterNormal) {
            fsm.isEnterNormal = false;
        }

        // 楼梯 
        var pos = owenr.Pos();

        // if (prop.isLadder) {
        //     Vector2 lowPos = prop.Pos() + Vector2.down * (prop.srBaseSize.y / 2) + Vector2.left * prop.srBaseSize.x / 2;
        //     Vector2 hightPos = prop.Pos() + Vector2.up * (prop.srBaseSize.y / 2) + Vector2.right * prop.srBaseSize.x / 2;

        //     float head_Center_Offset = owenr.GetHead_Front().y - owenr.Pos().y;
        //     float foot_Center_Offset = owenr.Pos().y - owenr.GetFoot().y;         //素材的中心点不在角色身高的中心点导致的问题

        //     float lowestY = lowPos.y + head_Center_Offset;//head_Front localpos()
        //     float highestY = hightPos.y + foot_Center_Offset + 0.2f; // 0.2f作为编辑器的偏差量。角色爬到顶上高一点再落地，也会有种缓动的感觉

        //     // 限制x的范围
        //     if (pos.x > lowPos.x && pos.x < hightPos.x) {
        //         // 往上爬的Y范围
        //         if (pos.y + head_Center_Offset > lowPos.y && pos.y < hightPos.y) {
        //             if (ctx.input.moveAxis.y > 0) {
        //                 owenr.fsm.EnterLadder(lowestY, highestY);
        //             }
        //             // 往下爬的Y的范围 
        //         } else if (pos.y > hightPos.y && pos.y < highestY) {   // 大于highest 到达顶部，collider变硬、 小于是开始下降
        //             if (ctx.input.moveAxis.y < 0) {
        //                 owenr.fsm.EnterLadder(lowestY, highestY);
        //             }
        //         }
        //     }
        // }
    }

    private static void ApplyFadeOut(GameContext ctx, PropEntity prop, float dt) {
        var fsm = prop.fsm;
        if (fsm.isEnterFadeOut) {
            fsm.isEnterFadeOut = false;
            prop.Anim_FadOut();
        }
        ref var timer = ref fsm.fadeOutTimer;
        timer -= dt;
        if (timer <= 0) {
            PropDomain.UnSpawn(ctx, prop);
        }
    }

    private static void ApplyHurt(GameContext ctx, PropEntity prop, float dt) {
        var fsm = prop.fsm;
        if (fsm.isEnterHurt) {
            fsm.isEnterHurt = false;
            prop.hurtFireTimer = 0;
        }
        var owner = ctx.GetOwner();
        // hurt fire
        if (prop.isHurtFire) {
            ref var timer = ref prop.hurtFireTimer;
            timer -= dt;
            if (timer <= 0) {
                timer = prop.hurtFireDuration;
                int damage = (int)(prop.hurtFireDamageRate * CommonConst.BASEDAMAGE);
                RoleDomain.Role_Hurt(ctx, owner, damage);
            }
        }
        // Thron
        if (prop.isThron) {
            Debug.Log("Enter Thron");
            ref var timer = ref prop.thronTimer;
            timer -= dt;
            if (timer <= 0) {
                timer = prop.thronDuration;
                int damage = (int)(prop.thronDamageRate * CommonConst.BASEDAMAGE);
                RoleDomain.Role_Hurt(ctx, owner, damage);
            }
        }
    }

}