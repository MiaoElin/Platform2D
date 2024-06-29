using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RectCell2D : IEquatable<RectCell2D>, IComparable<RectCell2D> {
    public Vector2Int pos;
    public float fCost;
    public float gCost;
    public float hCost;

    public RectCell2D parent;

    public void Init(Vector2Int pos, float f, float g, float h, RectCell2D parent) {
        this.pos = pos;
        this.fCost = f;
        this.gCost = g;
        this.hCost = h;
        this.parent = parent;
    }

    bool IEquatable<RectCell2D>.Equals(RectCell2D other) {
        return pos == other.pos;
    }

    public override int GetHashCode() {
        return pos.GetHashCode();
    }

    // public bool Equals(RectCell2D other) {
    //     if (other == null) {
    //         return false;
    //     }

    //     if (this.pos == other.pos) {
    //         return true;

    //     } else {
    //         return false;
    //     }
    // }

    // int IComparable<RectCell2D>.CompareTo(RectCell2D other) {
    //     // 暂时这么写
    //     if (fCost < other.fCost) {
    //         return -1;
    //     } else if (fCost > other.fCost) {
    //         return 1;
    //     } else {
    //         if (hCost > other.hCost) {
    //             return 1;
    //         } else if (hCost < other.hCost) {
    //             return -1;
    //         } else {
    //             if (pos.x > other.pos.x) {
    //                 return 1;
    //             } else {
    //                 return -1;
    //             }
    //         }
    //     }
    // }


    int IComparable<RectCell2D>.CompareTo(RectCell2D other) {

        Bit128 fKey = new Bit128();
        fKey.i32_0 = pos.y;
        fKey.i32_1 = pos.x;
        fKey.f32_2 = hCost;
        fKey.f32_3 = fCost;

        Bit128 otherFKey = new Bit128();
        otherFKey.i32_0 = other.pos.y;
        otherFKey.i32_1 = other.pos.x;
        otherFKey.f32_2 = other.hCost;
        otherFKey.f32_3 = other.fCost;

        if (fKey < otherFKey) {
            return -1;
        } else if (fKey > otherFKey) {
            return 1;
        } else {
            return 0;
        }

        // if (fCost < other.fCost) {
        //     return -1;
        // } else if (fCost > other.fCost) {
        //     return 1;
        // } else {
        //     if (hCost < other.hCost) {
        //         return -1;
        //     } else if (hCost > other.hCost) {
        //         return 1;
        //     } else {
        //         if (pos.x > other.pos.x) {
        //             return -1;
        //         } else if (pos.x < other.pos.x) {
        //             return 1;
        //         } else {
        //             if (pos.y > other.pos.y) {
        //                 return -1;
        //             } else if (pos.y < other.pos.y) {
        //                 return 1;
        //             } else {
        //                 return 0;
        //             }
        //         }
        //     }
        // }
    }
}