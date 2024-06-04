using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.AtmosphereSystem
{
    public abstract class AtmosphereNode : MonoBehaviour, IAtmosphereNode
    {
        public abstract AtmosphereCompounds AtmosphereBalance(float updateInterval);
    }
}
