using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PLu.Mars.AtmosphereSystem
{
    public interface IGasNode
    {
        AtmosphereCompounds GasBalance(float updateInterval);
    }
}
