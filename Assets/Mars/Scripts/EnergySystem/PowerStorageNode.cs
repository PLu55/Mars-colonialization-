using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.EnergySystem
{
    // TODO: Charge and discharge rates are proportional to capacity.
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

        public float CapacityRatio => _energyLevel / _capacity;
        private const float Wh2Joule = 3600f; 
        private const float kWh2Joule = 3600000f; 

        new void Awake()
        {
            _powerNodeType = PowerNodeType.PowerStorage;
            base.Awake();
        }
        public override float UpdateEnergyBalance(float updateInterval, float energyBalance)
        {
            if (energyBalance == 0f)
            {
                return 0f;
            }
            if (energyBalance > 0f)
            {
                energyBalance = _energyLevel + energyBalance > _capacity ?  _capacity - _energyLevel : energyBalance;

                float maxChargeEnergy = _maxChargeRate * updateInterval;
                energyBalance = energyBalance > maxChargeEnergy  ?  maxChargeEnergy : energyBalance;
                _energyLevel += energyBalance;
                return -energyBalance;
            }
            else
            {
                energyBalance = _energyLevel + energyBalance < 0f ? -_energyLevel : energyBalance;

                float maxChargeEnergy = _maxDischargeRate * updateInterval;
                energyBalance = -energyBalance > maxChargeEnergy  ?  -maxChargeEnergy : energyBalance;
                _energyLevel += energyBalance;
                return -energyBalance;
            }
        }
    }
}
