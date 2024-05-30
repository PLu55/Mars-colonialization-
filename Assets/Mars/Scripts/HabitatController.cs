using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Mars.Kernel;
using PLu.Utilities;

namespace PLu.Mars
{
    public class HabitatController : MonoBehaviour
    {
        [Header("Habitat Location")]
        [SerializeField] private float _longitude = 0.0f; // Decimal degrees
        [SerializeField] private float _latitude = 0.0f; // Decimal degrees
        [SerializeField] private float _altitude = 0.0f;
        [SerializeField] private float _updateInterval = 60f;

        public float Longitude => (_longitude % 360f + 360f) % 360f; // 0 to 360
        public float Latitude => _latitude;
        public float Altitude => _altitude;

        public int TimeZone => Mathf.RoundToInt(Longitude / 15f);
        public double LocalTime => Calendar.GlobalTime + TimeZone * 3600f; // local time in seconds
        public double LocalSolarTime => LocalTime + (Longitude - TimeZone * 15f) * 240f; // local solar time in seconds

        
        public double LocalSolarTimeNow => _localSolarTimeNow;
        public float CurrentSolarIrradiance => _currentSolarIrradiance;

        private double _localSolarTimeNow;
        private float _currentSolarIrradiance;
        CountdownTimer _TickTimer;

        void Start()
        {
            _TickTimer = new CountdownTimer(_updateInterval, true);
            _TickTimer.Start();
            _localSolarTimeNow = LocalSolarTime;
            UpdateHabitat();
        }
        void Update()
        {
            _localSolarTimeNow = LocalSolarTime;
            _TickTimer.Tick(Time.deltaTime);
            if (_TickTimer.IsFinished)
            {
                UpdateHabitat();
                _TickTimer.Reset();
                _TickTimer.Start();
                Debug.Log("Local Time: " + LocalTime);
                Debug.Log("Local Solar Time: " + LocalSolarTime);
            }
        }
        void UpdateHabitat()
        {
            Debug.Log("Updating Habitat");
            UpdateSolarIrradiance();
        }
        void UpdateSolarIrradiance()
        {
            float timeOfDay = Calendar.TimeOfDay;
            float hourAngle = CelestialCalculator.HourAngle((float)(LocalSolarTimeNow / 3600.0));
            _currentSolarIrradiance = CelestialCalculator.SolarIrradiance(Latitude, hourAngle, timeOfDay);
            Debug.Log($"--- Current Solar Irradiance: {_currentSolarIrradiance}");
        }
    }
}
