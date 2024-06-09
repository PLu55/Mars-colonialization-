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

        new void Awake()
        {
            base.Awake(); 
            if (_debug) Debug.Log("Solar Power Plant is awake");
            _powerNodeType = PowerNodeType.PowerProducer;  
        }
        public override float UppdateEffectLevel(float updateInterval, float effectBalance)
        {
            _currentEffect = _efficiency * _area * EnergyController.SolarIrradiance;
            return _currentEffect;
        }
    }
}
