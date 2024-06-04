using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Utilities;
using PLu.Mars.Kernel;

namespace PLu.Mars.FacillitySystem
{
    public class FacillityController : Controller
    {
        private List<IFacillity> _facillities;

        public static FacillityController FindClosestFacillityController(Vector3 position)
        {
            HabitatSystem.HabitatController habitatController = HabitatSystem.HabitatController.FindClosestHabitat(position);
            return habitatController.GetComponent<FacillityController>();
        }

        void Awake()
        {
            _facillities = new List<IFacillity>();
            
        }   
        public override void UpdateController(float updateInterval)
        {
            foreach (var facillity in _facillities)
            {
                facillity.UpdateFacillity(updateInterval);
            }
        }
        public void AddFacillity(IFacillity facillity)
        {
            _facillities.Add(facillity);
        }
        public void RemoveFacillity(IFacillity facillity)
        {
            _facillities.Remove(facillity);
        }
    }
}
