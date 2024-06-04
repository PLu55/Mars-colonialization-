using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Utilities;
using PLu.Mars.Kernel;
using PLu.Mars.HabitatSystem;
 
namespace PLu.Mars.EnergySystem
{
    public class EnergyController : MonoBehaviour
    {
        [Header("Energy Settings")]
        [SerializeField] private float _effectBalance = 0f;

        [Header("Updating Settings")]
        [SerializeField] private float _updateInterval = 1f;
        [Header("Debugging")]
        [SerializeField] private bool _debug = false;

        public float EffectBalance=> _effectBalance;
        public float SolarIrradiance => _solarIrradiance;
        public float UpdateInterval => _updateInterval;
        private HabitatController _habitatController;
        private CountdownTimer _tickTimer;
        private float _solarIrradiance;

        private List<IPowerNode> _powerProducerNodes = new List<IPowerNode>();
        private List<IPowerNode> _powerConsumerNodes = new List<IPowerNode>();
        private List<IPowerNode> _powerStorageNodes = new List<IPowerNode>();

        void Awake()
        {
            _habitatController = GetComponent<HabitatController>();
            Debug.Assert(_habitatController != null, "Habitat Controller is not found in EnergyController");
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
        void UpdateEnergyBalance()
        {            
            if (_debug) Debug.Log("Updating Energy Balance");
            _solarIrradiance = _habitatController.CurrentSolarIrradiance;

            _effectBalance = 0f;
            float effectProduced = 0f;
            float effectConsumed = 0f;
            float effectStored = 0f;
            foreach(var node in _powerProducerNodes)
            {
                effectProduced += node.UppdateEffectLevel(_updateInterval, _effectBalance);
                
            }
            _effectBalance += effectProduced;
            if (_debug) Debug.Log($"--- Effect produced: {effectProduced}, Solar Irradiance: {_solarIrradiance}");
  
            foreach(var node in _powerConsumerNodes)
            {
                effectConsumed += node.UppdateEffectLevel(_updateInterval, _effectBalance);
            }
            _effectBalance += effectConsumed;
            if (_debug) Debug.Log($"--- Effect consumed: {effectConsumed}");
            if (_effectBalance < 0f)
            {

            }

            foreach(var node in _powerStorageNodes)
            {
                float currentEffect = node.UppdateEffectLevel(_updateInterval, _effectBalance);
                effectStored += currentEffect;
                _effectBalance += currentEffect;
            }

            if (_debug) Debug.Log($"--- Effect Stored: {-effectStored}");
            if (_debug) Debug.Log($"--- Effect Balance: {_effectBalance}");
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
