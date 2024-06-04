using System.Collections;
using System.Collections.Generic;
using PLu.Utilities;
using UnityEngine;
using PLu.Mars.HabitatSystem;
using PLu.Mars.AtmosphereSystem;

namespace PLu.Mars.BotanicSystem
{
    public class BotanicController : MonoBehaviour
    {
        [SerializeField] private float _updateInterval = 3600f;
        private CountdownTimer _timer;
        private List<IPlant> _plants;
        private HabitatController _habitatController;
        private AtmosphereController _atmosphereController;

        public float UpdateIntervall => _updateInterval;
        public HabitatController HabitatController => _habitatController;

        void Awake()
        {
            _habitatController = HabitatController.FindClosestHabitat(transform.position);
            Debug.Assert(_habitatController != null, "Habitat Controller is not found in PlantController");
            _atmosphereController = FindObjectOfType<AtmosphereController>();
            Debug.Assert(_atmosphereController != null, "AtmosphereController is not found in PlantController");
            _plants = new List<IPlant>();
        }
        void Start()
        {   
            _timer = new CountdownTimer(_updateInterval);
            _timer.Start();
        }
        void Update()
        {
            _timer.Tick(Time.deltaTime);
            if (_timer.IsFinished)
            {
                UpdatePlants(_updateInterval);
                _timer.Reset();
                _timer.Start();
            }
        }
        private void UpdatePlants(float updateInterval)
        {
            foreach (var plant in _plants)
            {
                plant.UpdatePlant(updateInterval);
            }
            UpdateGasBallance(updateInterval);
        }
        private void UpdateGasBallance(float updateInterval)
        {
            AtmosphereCompounds gasBalance = new AtmosphereCompounds();
            foreach (var plant in _plants)
            {
                gasBalance += plant.AtmosphereBalance(updateInterval);
            }
            _atmosphereController.UpdateGases(updateInterval, gasBalance);
        }

        public void AddPlant(IPlant plant)
        {
            _plants.Add(plant);
        }
        public void RemovePlant(IPlant plant)
        {
            _plants.Remove(plant);
        }
        public static BotanicController FindClosestPlantController(Vector3 position)
        {
            HabitatController _habitatController = HabitatController.FindClosestHabitat(position);  
            if (_habitatController == null)
            {
                Debug.LogError("Habitat Controller is not found in PlantController");
                return null;
            }
             BotanicController plantController = _habitatController.GetComponent<BotanicController>();

            Debug.Assert(plantController != null, "Plant Controller is not found in Plant");
            return plantController;
        }
    }
}
