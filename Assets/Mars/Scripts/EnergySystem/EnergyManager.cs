using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Utilities;
using PLu.Mars.Core;
using PLu.Mars.HabitatSystem;
 
namespace PLu.Mars.EnergySystem
{
    public class EnergyManager : MonoBehaviour
    {
        [Header("Energy Settings")]
        [Tooltip("The total energy balance in kWh of the habitat")]
        [SerializeField] private float _energyBalance = 0f;

        [Header("Updating Settings")]
        [SerializeField] private float _updateInterval = 1f;
        [Header("Debugging")]
        [SerializeField] private bool _debug = false;

        public float EnergyBalance=> _energyBalance;
        public float SolarIrradiance => _solarIrradiance;
        public float UpdateInterval => _updateInterval;
        private HabitatManager _habitatController;
        private CountdownTimer _tickTimer;
        private float _solarIrradiance;

        private List<IPowerNode> _powerProducerNodes = new List<IPowerNode>();
        private List<IPowerNode> _powerConsumerNodes = new List<IPowerNode>();
        private List<IPowerNode> _powerStorageNodes = new List<IPowerNode>();

        void Awake()
        {
            _habitatController = GetComponent<HabitatManager>();
            Debug.Assert(_habitatController != null, "HabitatManager is not found in EnergyManager");
        }
        void Start()
        {
            _tickTimer = new CountdownTimer(_updateInterval);
            _tickTimer.Start();
        }
        void Update()
        {
            _tickTimer.Tick(Time.deltaTime);
            if (_tickTimer.IsFinished)
            {
                _tickTimer.Reset();
                _tickTimer.Start();
                UpdateEnergyBalance();
            }
        }

        // TODO: When there is a power shortage, the energy should be taken from the
        //      storage nodes and distributed proportionally or prioritized.
        void UpdateEnergyBalance()
        {            
            if (_debug) Debug.Log("Updating Energy Balance");
            _solarIrradiance = _habitatController.CurrentSolarIrradiance;

            _energyBalance = 0f;

            float energyProduced = 0f;
            float energyConsumed = 0f;
            float energyStored = 0f;
            foreach(var node in _powerProducerNodes)
            {
                energyProduced += node.UpdateEnergyBalance(_updateInterval);
            }
            _energyBalance += energyProduced;
            if (_debug) Debug.Log($"--- Energy produced: {energyProduced} kWh, Solar Irradiance: {_solarIrradiance} W/m2");
  
            foreach(var node in _powerConsumerNodes)
            {
                float requestedEnergy = node.RequestedEnergy(_updateInterval);
                if (requestedEnergy > _energyBalance)
                {
                    float energyShortage = requestedEnergy - _energyBalance;
                    float energyTaken = 0f;
                    foreach(var storageNode in _powerStorageNodes)
                    {
                        energyTaken = storageNode.UpdateEnergyBalance(_updateInterval, energyShortage);
                        energyShortage -= energyTaken;
                        _energyBalance += energyTaken;
                        energyStored -= energyTaken;
                        if ( energyShortage <= 0f)
                        {
                            break;
                        }
                    }
                }
                energyConsumed += node.UpdateEnergyBalance(_updateInterval, _energyBalance);
                _energyBalance += energyConsumed;
            }

            if (_debug) Debug.Log($"--- Energy consumed: {energyConsumed} kWh");
            if (_energyBalance > 0f)
            {
                foreach(var node in _powerStorageNodes)
                {
                    float storedEnergy = node.UpdateEnergyBalance(_updateInterval, _energyBalance);
                    energyStored += storedEnergy;
                    _energyBalance -= storedEnergy;
                }
            }

            if (_debug) Debug.Log($"--- Energy Stored: {-energyStored} kWh");
            if (_debug) Debug.Log($"--- Energy Balance: {_energyBalance} kWh");
        }

        public void AddPowerNode(IPowerNode node)
        {
            switch(node.PowerNodeType)
            {
                case PowerNodeType.PowerProducer:
                    _powerProducerNodes.Add(node);
                    if (_debug) Debug.Log($"Power producer added");
                    break;
                case PowerNodeType.PowerConsumer:
                    if (_debug) Debug.Log($"Power consumer added");
                    _powerConsumerNodes.Add(node);
                    break;
                case PowerNodeType.PowerStorage:
                    if (_debug) Debug.Log($"Power storage added");
                    _powerStorageNodes.Add(node);
                    break;
            }  
        }
        public void RemovePowerNode(IPowerNode node)
        {
            switch(node.PowerNodeType)
            {
                case PowerNodeType.PowerProducer:
                    _powerProducerNodes.Remove(node);
                    break;
                case PowerNodeType.PowerConsumer:
                    _powerConsumerNodes.Remove(node);
                    break;
                case PowerNodeType.PowerStorage:
                    _powerStorageNodes.Remove(node);
                    break;
            }
        }
    }
}
