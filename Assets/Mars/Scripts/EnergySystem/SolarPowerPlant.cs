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
        const float Joule2kWh = 2.77778e-7f;

        new void Awake()
        {
            base.Awake(); 
            if (_debug) Debug.Log("Solar Power Plant is awake");
            _powerNodeType = PowerNodeType.PowerProducer;  
        }
        public override float UpdateEnergyBalance(float updateInterval, float energyBalance = 0f)
        {
            _currentEffect = _efficiency * _area * EnergyManager.SolarIrradiance;
            return _currentEffect * updateInterval  * Joule2kWh;
        }
    }
}
