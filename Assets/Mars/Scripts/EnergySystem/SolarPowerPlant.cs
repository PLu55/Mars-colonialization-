using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PLu.Mars.EnergySystem
{
    public class SolarPowerPlant : PowerNode
    {
        [Header("Solar Power Plant Settings")]
        [SerializeField] private float _area = 1f;
        [SerializeField] private float _efficiency = 0.2f;
        [SerializeField] private float _currentEffect = 0f;

        void Awake()
        {
            _powerNodeType = PowerNodeType.PowerProducer;   
        }
        public override float CurrentEffectLevel()
        {
            _currentEffect = _efficiency * _area * EnergyController.SolarIrradiance;
            return _currentEffect;
        }
    }
}
