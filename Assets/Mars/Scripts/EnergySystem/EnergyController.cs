using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Utilities;
using PLu.Mars.Kernel;
 
namespace PLu.Mars.EnergySystem
{
    public class EnergyController : MonoBehaviour
    {
        [Header("Energy Settings")]
        [SerializeField] private float _effectBalance = 0f;


        [Header("Updating Settings")]
        [SerializeField] private float _updateInterval = 1f;
        public float EffectBalance=> _effectBalance;
        public float SolarIrradiance => _solarIrradiance;
        public float UpdateInterval => _updateInterval;
        private HabitatController _habitatController;
        private CountdownTimer _TickTimer;
        private float _solarIrradiance;

        private List<IPowerNode> _powerProducerNodes = new List<IPowerNode>();
        private List<IPowerNode> _powerConsumerNodes = new List<IPowerNode>();
        private List<IPowerNode> _powerStorageNodes = new List<IPowerNode>();

        void Awake()
        {
            _habitatController = FindObjectOfType<HabitatController>(); // TODO: not correct, fix it!
            Debug.Assert(_habitatController != null, "Habitat Controller is not found in EnergyController");
        }
        void Start()
        {
            _TickTimer = new CountdownTimer(_updateInterval);
            _TickTimer.Start();
        }
        void Update()
        {
            _TickTimer.Tick(Time.deltaTime);
            if (_TickTimer.IsFinished)
            {
                _TickTimer.Reset();
                _TickTimer.Start();
                UpdateEnergyBalance();
            }
        }
        void UpdateEnergyBalance()
        {            
            Debug.Log("Updating Energy Balance");
            _solarIrradiance = _habitatController.CurrentSolarIrradiance;

            _effectBalance = 0f;
            float effectProduced = 0f;
            float effectConsumed = 0f;
            float effectStored = 0f;
            foreach(var node in _powerProducerNodes)
            {
                effectProduced += node.CurrentEffectLevel();
                
            }
            _effectBalance += effectProduced;
            Debug.Log($"--- Effect produced: {effectProduced}, Solar Irradiance: {_solarIrradiance}");
  
            foreach(var node in _powerConsumerNodes)
            {
                effectConsumed += node.CurrentEffectLevel();
            }
            _effectBalance += effectConsumed;
            Debug.Log($"--- Effect consumed: {effectConsumed}");
            if (_effectBalance < 0f)
            {

            }

            foreach(var node in _powerStorageNodes)
            {
                effectStored += node.CurrentEffectLevel();
            }
            _effectBalance += effectStored;
            Debug.Log($"---    Effect Stored: {effectStored}");
            Debug.Log($"---    Effect Balance: {_effectBalance}");
        }

        public void AddPowerNode(IPowerNode node)
        {
            switch(node.PowerNodeType)
            {
                case PowerNodeType.PowerProducer:
                    _powerProducerNodes.Add(node);
                    Debug.Log($"Power producer added");
                    break;
                case PowerNodeType.PowerConsumer:
                    Debug.Log($"Power consumer added");
                    _powerConsumerNodes.Add(node);
                    break;
                case PowerNodeType.PowerStorage:
                    Debug.Log($"Power storage added");
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
