using UnityEngine;

public static class VFXDomain {
    public static void Play(GameContext ctx, Vector2 pos, Sprite[] sprites) {
        VFXModel vfx = GameFactory.VFX_Spawn(ctx, sprites, pos);
        ctx.vfxs.Add(vfx);
    }

    public static void Tick(GameContext ctx, VFXModel vfx, float dt) {
        vfx.Tick(dt);
        if (vfx.isEnd) {
            Unspawn(ctx, vfx);
        }
    }

    public static void Unspawn(GameContext ctx, VFXModel vfx) {
        ctx.vfxs.Remove(vfx);
        GameObject.Destroy(vfx.gameObject);
    }
}