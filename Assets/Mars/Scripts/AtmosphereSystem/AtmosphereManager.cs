using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Utilities;
 
namespace PLu.Mars.AtmosphereSystem
{
    public class AtmosphereManager : MonoBehaviour
    {
        [Header("Atmosphere")]
        [Tooltip("Wanted atmospheric pressure in kPa, normal range 50 to 70 kPa")]
        [SerializeField] private float _nominalTotalPressure = 60.0f;
        [Tooltip("Wanted partial oxygen pressure")]
        [SerializeField] private float _nominalOxygenFraction = 0.35f;
        [SerializeField] private float _nominalCarbonDioxideFraction = 0.001f;
        [SerializeField] private float _nominalNitrogenFraction = 0.65f;
        [SerializeField] private float _nominalTemprature = 20f;
        [SerializeField] private float _nominalRelativHumility = 55f;

        [Header("Update Interval")]
        [SerializeField] private float _updateInterval = 60f;

        [Header("Debugging")]
        [SerializeField] private bool _debug = false;


        public float NominalTotalPressure => _nominalTotalPressure;
        public float NominalOxygenFraction => _nominalOxygenFraction;
        public float NominalCarbonDioxideFraction => _nominalCarbonDioxideFraction;
        public float NominalNitrogenFraction => _nominalNitrogenFraction;
        public float PartialOxygenPressure => _nominalOxygenFraction * _nominalTotalPressure; // in kPa, normal range 20 to 21 kPa
        public float PartialNitrogenPressure => _nominalNitrogenFraction * _nominalTotalPressure;
        public float PartialCarbonDioxidePressure => _nominalCarbonDioxideFraction * _nominalTotalPressure;

        private float _temprature;
        private float _totalPressure;        
        private float _partialOxygenPressure;
        private float _partialNitrogenPressure;
        private float _partialCarbonDioxidePressure;


        [SerializeField] private float _nominalPartialOxygenPressure = 21f;
        [SerializeField] private float _nominalNitrogenPartialPressure = 39f;
        [SerializeField] private float _nominalCarbonDioxidePartialPressure = 0.06f;
        public float TotalPressure => _totalPressure;
        public float OxygenLevel => _oxygenLevel;
       
        public float OxygenVolumeRatio =>  TotalPressure / PartialOxygenPressure;
        
        private float _oxygenLevel = 0.0f; // ratio of 0.2-0.3
        
        private float _oxygenVolumeRatio;

        private RepeatTimer _updateTimer;
        private List<IAtmosphereNode> _atmosphereNodes;

        private void Awake()
        {
            _atmosphereNodes = new List<IAtmosphereNode>();
        }

        void Start()
        {
            Initialization();
            _updateTimer = new RepeatTimer(_updateInterval);
            _updateTimer.Start();
            _updateTimer.OnTimerRepeat += UpdateAtmosphere;
        }
        void Update()
        {
            _updateTimer.Tick(Time.deltaTime);
        }

        private void Initialization()
        {
            _temprature = _nominalTemprature;
            _totalPressure = _nominalTotalPressure;
            _partialOxygenPressure = _nominalOxygenFraction * _nominalTotalPressure;
            _partialNitrogenPressure = _nominalNitrogenFraction * _nominalTotalPressure;
            _partialCarbonDioxidePressure = _nominalCarbonDioxideFraction * _nominalTotalPressure;
        }
        public void UpdateAtmosphere(float deltaTime)
        {
            if (_debug) Debug.Log($"UpdateAtmosphere {deltaTime}");

            //_oxygenVolumeRatio = TotalPressure / PartialOxygenPressure;
            //_oxygenLevel = _oxygenVolumeRatio / (1 + _oxygenVolumeRatio);

            foreach (IAtmosphereNode node in _atmosphereNodes)
            {
                AtmosphereCompounds compounds = node.AtmosphereBalance(_updateInterval);
                _totalPressure += compounds.TotalPressure;
                _partialOxygenPressure += compounds.PartialOxygenPressure;
                _partialNitrogenPressure += compounds.PartialNitrogenPressure;
                _partialCarbonDioxidePressure += compounds.PartialCarbonDioxidePressure;
            }
        }

        public void UpdateGases(float updateInterval, float totalPressure, float oxygenLevel)
        {
            _totalPressure = totalPressure;
            _oxygenLevel = oxygenLevel;
        }

        public void AddAtmosphereNode(IAtmosphereNode node)
        {
            if (_debug) Debug.Log($"AddAtmosphereNode {node.ToString()}");
        }
        public void RemoveAtmosphereNode(IAtmosphereNode node)
        {
            if (_debug) Debug.Log($"RemoveAtmosphereNode {node.ToString()}");
        }
    }
}
