using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.AtmosphereSystem
{
    public class AtmosphereController : MonoBehaviour
    {
        [Header("Atmosphere")]
        [Tooltip("Total pressure in kPa")]
        [SerializeField] private float _nominalTotalPressure = 60.0f; // in kPa, normal range 50 to 70 kPa
        [SerializeField] private float _nominalOxygenFraction = 0.35f;
        [SerializeField] private float _nominalCarbonDioxideFraction = 0.001f;
        [SerializeField] private float _nominalNitrogenFraction = 0.65f;

        public float NominalTotalPressure => _nominalTotalPressure;
        public float NominalOxygenFraction => _nominalOxygenFraction;
        public float NominalCarbonDioxideFraction => _nominalCarbonDioxideFraction;
        public float NominalNitrogenFraction => _nominalNitrogenFraction;
        public float PartialOxygenPressure => _nominalOxygenFraction * _nominalTotalPressure; // in kPa, normal range 20 to 21 kPa
        public float PartialNitrogenPressure => _nominalNitrogenFraction * _nominalTotalPressure;
        public float PartialCarbonDioxidePressure => _nominalCarbonDioxideFraction * _nominalTotalPressure;

        private float _partialOxygenPressure;

        [SerializeField] private float _nominalPartialOxygenPressure = 21f;
        [SerializeField] private float _nominalNitrogenPartialPressure = 39f;
        [SerializeField] private float _nominalCarbonDioxidePartialPressure = 0.06f;
        public float TotalPressure => _totalPressure;
        public float OxygenLevel => _oxygenLevel;
       
        public float OxygenVolumeRatio =>  TotalPressure / PartialOxygenPressure;
        private float _totalPressure = 60.0f; // in kPa, normal range 50 to 70 kPa
        private float _oxygenLevel = 0.0f; // ratio of 0.2-0.3
        
        private float _oxygenVolumeRatio;


        void Start()
        {
            
        }
        void Update()
        {
            
        }

        public void UpdateAtmosphere()
        {
            _oxygenVolumeRatio = TotalPressure / PartialOxygenPressure;
            _oxygenLevel = _oxygenVolumeRatio / (1 + _oxygenVolumeRatio);
        }
        public void UpdateGases(float updateInterval, AtmosphereCompounds gases)
        {
            Debug.Log($"UpdateGases {gases.ToString()}");
        }
        public void UpdateGases(float updateInterval, float totalPressure, float oxygenLevel)
        {
            _totalPressure = totalPressure;
            _oxygenLevel = oxygenLevel;
        }
    }
}
