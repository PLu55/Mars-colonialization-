using System.Collections;
using System.Collections.Generic;
using PLu.Mars.AtmosphereSystem;
using UnityEngine;
 
namespace PLu.Mars.BotanicSystem
{
    public abstract class PlantBase : MonoBehaviour, IPlant
    {
        [SerializeField] private float _leafArea = 1f;
        public virtual float LeafArea => _leafArea;
        public BotanicController PlantController => _botanicController;
        private BotanicController _botanicController;

        protected void Start()
        {
            _botanicController = BotanicController.FindClosestPlantController(transform.position);
            Debug.Assert(_botanicController != null, "Plant Controller is not found in PlantBase");

            _botanicController.AddPlant(this);
        }
        public virtual void UpdatePlant(float updateInterval)
        {
            
        }
        public abstract AtmosphereCompounds GasBalance(float updateInterval);

    }
}
