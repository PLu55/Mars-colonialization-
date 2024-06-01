using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.AtmosphereSystem
{
    public abstract class GasNode : MonoBehaviour, IGasNode
    {
        public abstract AtmosphereCompounds GasBalance(float updateInterval);
    }
}
