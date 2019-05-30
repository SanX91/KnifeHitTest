using System.Collections.Generic;

namespace KnifeHitTest
{
    /// <summary>
    /// The ILogSettings interface.
    /// </summary>
    public interface ILogSettings
    {
        IEnumerable<int> KnifeAngles { get; }
        IEnumerable<int> FruitAngles { get; }
        IRotationSettings RotationSettings { get; }
    }
}
