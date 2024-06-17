using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Mars.Core;
using PLu.Mars.HabitatSystem;

namespace PLu.Mars.EnergySystem
{
    public class PowerNode : MonoBehaviour, IPowerNode
    {
        [SerializeField] protected PowerNodeType _powerNodeType = PowerNodeType.PowerConsumer;
        [SerializeField] private HabitatManager _habitat;
        [SerializeField] private float _nominalEffect = 0f;
        [Header("Debugging")]
        [SerializeField] protected bool _debug = false;


        private EnergyManager _energyManager;
        public HabitatManager Habitat => _habitat;
        public PowerNodeType PowerNodeType => _powerNodeType;
        public float NominalEffect => _nominalEffect;
    
        public EnergyManager EnergyManager => _energyManager;
        protected void Awake()
        {
            if (_debug) Debug.Log($"Power Node({this.GetType().Name}) is awake");
            if (_habitat == null) 
            {
                _habitat = HabitatManager.FindClosestHabitat(this.transform.position);
            }

            Debug.Assert(_habitat != null, $"No habitat is found in {name}");
        }
        protected void Start()
        {
            if (_debug) Debug.Log("Power Node is started");
            _energyManager = _habitat.GetComponent<EnergyManager>();
            Debug.Assert(_energyManager != null, $"Energy Controller is not found in {name}");
            _energyManager.AddPowerNode(this);
        }       
        public virtual float UpdateEnergyBalance(float updateInterval, float energyBalance = 0f)
        {
            return NominalEffect * updateInterval / 3600f;
        }
    }
}
