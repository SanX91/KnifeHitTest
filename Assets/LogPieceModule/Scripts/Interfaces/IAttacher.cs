using System.Collections.Generic;

namespace KnifeHitTest
{
    /// <summary>
    /// The IAttacher interface.
    /// </summary>
    public interface IAttacher
    {
        IEnumerable<Attachable> AttachedItems { get; }
        void AttachItem(Attachable item);
        void AttachItems(PooledAttachableFactory factory, IEnumerable<int> angles, bool isFlipped = false);
        void Reset();
    }
}
