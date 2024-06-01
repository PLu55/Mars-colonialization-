using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Mars.Kernel;
using PLu.Mars.HabitatSystem;

namespace PLu.Mars.EnergySystem
{
    public class PowerNode : MonoBehaviour, IPowerNode
    {
        [SerializeField] protected PowerNodeType _powerNodeType = PowerNodeType.PowerConsumer;
        [SerializeField] private HabitatController _habitat;
        [SerializeField] private float _nominalEffect = 0f;


        private EnergyController _energyController;
        public HabitatController Habitat => _habitat;
        public PowerNodeType PowerNodeType => _powerNodeType;
        public float NominalEffect => _nominalEffect;
    
        public EnergyController EnergyController => _energyController;
        protected void Awake()
        {
            Debug.Log($"Power Node({this.GetType().Name}) is awake");
    
            _habitat = HabitatController.FindClosestHabitat(this.transform.position);
            Debug.Assert(_habitat != null, "Habitat is not found in PowerNode");
        }
        void Start()
        {
            Debug.Log("Power Node is started");
            _energyController = _habitat.GetComponent<EnergyController>();
            Debug.Assert(_energyController != null, "Energy Controller is not found in PowerNode");
            _energyController.AddPowerNode(this);
        }
        public virtual float UppdateEffectLevel(float updateInterval, float effectBalance)
        {
            return _nominalEffect;
        }
    }
}
