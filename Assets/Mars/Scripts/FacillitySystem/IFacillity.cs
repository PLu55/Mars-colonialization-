using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.FacillitySystem
{
    public interface IFacillity
    {
        float Durability { get; set; } // Durability of the facillity (0-1)
        void UpdateFacillity(float updateInterval); // Update the facillity
    }
}
