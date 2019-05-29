using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public interface ILogPiece
    {
        Collider2D Collider { get; }
        SpriteRenderer Renderer { get; }
        Vector3 Position { get; }
    } 
}
