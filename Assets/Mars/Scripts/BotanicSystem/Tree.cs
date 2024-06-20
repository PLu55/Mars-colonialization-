using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Mars.HabitatSystem;
using PLu.Mars.AtmosphereSystem;

namespace PLu.Mars.BotanicSystem
{
    public class Tree : PlantBase
    {
        [Header("Tree Parameters")]
        [Tooltip("Radius of the canopy, m")]
        [SerializeField] private float _canopyRadius;

        [Tooltip("Height of the tree, m")]
        [SerializeField] private float _height;

        [Tooltip("Transpiration rate, l per hour per m2 leaf area")]
        [SerializeField] private float _transpirationRate = 0.1f;

        [SerializeField] private float _nutrientConsumption = 0.1f; // kg per hour
        [SerializeField] private float _oxygenProductionIndex = 0.005f; // l per hour per m2 leaf area
        [SerializeField] private float _leafAreaIndex = 5.0f; // m2 leaf area per m2 ground area, 3-7 for trees
        [SerializeField] private float _photosyntheticRate = 10; // in micromoles CO2 / m²·s
        
        [Header("Debugging")]
        [SerializeField] private bool _debug = false;
        public float CanopyArea => _canopyRadius * _canopyRadius * Mathf.PI;
        public override float LeafArea => CanopyArea * _leafAreaIndex;

        public float OxygenProduction => _oxygenProductionIndex * LeafArea; // l per h

        private HabitatManager _habitatController;

        new void Start()
        {
            base.Start();
            Debug.Assert(PlantController != null, "Plant Controller is not found in Tree");   
            _habitatController = PlantController.HabitatController;
        }
        public override void UpdatePlant(float updateInterval)
        {
            
        }
        public override AtmosphereCompounds AtmosphereBalance(float updateInterval)
        {// oxygen nitrogen carbonDioxide
            if (_habitatController.CurrentSolarIrradiance < 0.1f)
            {
                if (_debug) Debug.Log("Tree is not producing oxygen because of low solar irradiance");
                return new AtmosphereCompounds(0f, 0f, 0f, 0f);
            }
            float oxygen = OxygenProduction * updateInterval / 3600f;
            float transpiration = _transpirationRate * LeafArea * updateInterval / 3600f; 
            if (_debug) Debug.Log("Tree is producing " + oxygen + " l of oxygen");
            return new AtmosphereCompounds(oxygen, 0f, -0f, transpiration);
        }
    }
}