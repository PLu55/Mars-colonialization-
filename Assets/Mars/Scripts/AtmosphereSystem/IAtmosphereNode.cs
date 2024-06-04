using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PLu.Mars.AtmosphereSystem
{
    public interface IAtmosphereNode
    {
        AtmosphereCompounds AtmosphereBalance(float updateInterval);
    }
}
