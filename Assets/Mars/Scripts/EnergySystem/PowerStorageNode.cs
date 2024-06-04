using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.EnergySystem
{
    // TODO: implement maximun charge and discharge rates
    public class PowerStorageNode : PowerNode, IPowerNode
    {
        [Header("Power Storage Settings")]
        [Tooltip("The maximum amount of energy in kWh this node can store.")]
        [SerializeField] private float _capacity = 0f;

        [Tooltip("The current energy level in kWh")]
        [SerializeField] private float _energyLevel = 0f;

        [Tooltip("The maximun charge rate in kW")]
        [SerializeField] private float _maxChargeRate = 0f;

        [Tooltip("The maximun discharge rate in kW")]
        [SerializeField] private float _maxDischargeRate = 0f;

        public float CapacityRation => _energyLevel / _capacity;
        private const float Wh2Joule = 3600f; 
        private const float kWh2Joule = 3600000f; 

        new void Awake()
        {
            _powerNodeType = PowerNodeType.PowerStorage;
            base.Awake();
        }
        // TODO: FIX: Balance is wrong negative total effect balance in EnergyController
        public override float UppdateEffectLevel(float updateInterval, float effectBalance)
        {
            float energyBalance;
            effectBalance /= 1000f; // effectBalance in kW

            if (effectBalance == 0)
            {
                return 0f;
            }
            else if (effectBalance > 0)
            {
                effectBalance = effectBalance > _maxChargeRate ?  _maxChargeRate : effectBalance;
                energyBalance = effectBalance * updateInterval / Wh2Joule;
                
                energyBalance = _energyLevel + energyBalance > _capacity ? _capacity - _energyLevel : energyBalance;
                _energyLevel += energyBalance;
            }
            else
            {
                effectBalance = -effectBalance > _maxDischargeRate ?  -_maxDischargeRate : effectBalance;
                energyBalance = effectBalance * updateInterval / Wh2Joule;
                energyBalance = _energyLevel + energyBalance < 0 ?  -_energyLevel : energyBalance;

                _energyLevel += energyBalance;
            }

            return -energyBalance / updateInterval * Wh2Joule * 1000f;
        }
    }
}
