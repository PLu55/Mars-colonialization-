using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Mars.HabitatSystem;
using PLu.Utilities;
 
namespace PLu.Mars.Kernel
{
    public interface IController
    {
        CountdownTimer UpdateTimer { get; }
        HabitatController HabitatController { get; }

        void UpdateController(float updateInterval);

    }
}
