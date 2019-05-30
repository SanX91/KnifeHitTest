using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The ILogPiece interface.
    /// </summary>
    public interface ILogPiece
    {
        Collider2D Collider { get; }
        SpriteRenderer Renderer { get; }
        Vector3 Position { get; }
    }
}
