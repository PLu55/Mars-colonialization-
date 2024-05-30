using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.EnergySystem
{
    public class PowerStorageNode : PowerNode, IPowerNode
    {
        [Header("Power Storage Settings")]
        [Tooltip("The maximum amount of energy this node can store.")]
        [SerializeField] private float _capacity = 0f;
        [SerializeField] private float _energyLevel = 0f;
        void Awake()
        {
            _powerNodeType = PowerNodeType.PowerStorage;   
        }
        public override float CurrentEffectLevel()
        {
            float energyBalance = EnergyController.EffectBalance * EnergyController.UpdateInterval;
            float outgoingEnergy = 0f;
            if (energyBalance > 0)
            {
                if (_energyLevel + energyBalance > _capacity)
                {
                    outgoingEnergy = _capacity - _energyLevel;
                    _energyLevel = _capacity;
                }
                else
                {
                    outgoingEnergy = energyBalance;
                    _energyLevel += energyBalance;
                }
            }
            else
            {
                if (_energyLevel + energyBalance < 0)
                {
                    outgoingEnergy = energyBalance + _energyLevel;
                    _energyLevel = 0f;
                }
                else
                {
                    _energyLevel += energyBalance;
                }
            }
            return -outgoingEnergy / EnergyController.UpdateInterval;
        }
    }
}
