using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Mars.HabitatSystem;
using PLu.Utilities;
 
namespace PLu.Mars.Core
{
    public interface IManager
    {
        CountdownTimer UpdateTimer { get; }
        HabitatManager HabitatManager { get; }

        void UpdateManager(float updateInterval);

    }
}
