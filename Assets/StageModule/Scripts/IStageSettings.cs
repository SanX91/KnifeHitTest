using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public interface IStageSettings
    {
        ILogSettings LogSettings { get; }
        int Knives { get; }
    } 
}
