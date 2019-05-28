using System.Collections.Generic;

namespace KnifeHitTest
{
    public interface ILogSettings
    {
        IEnumerable<int> KnifeAngles { get; }
        IEnumerable<int> FruitAngles { get; }
        IRotationSettings RotationSettings { get; }
    } 
}
