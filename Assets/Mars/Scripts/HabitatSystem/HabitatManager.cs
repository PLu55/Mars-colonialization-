using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Utilities;
using PLu.Mars.Core;

namespace PLu.Mars.HabitatSystem
{
    public class HabitatManager : MonoBehaviour
    {
        [Header("Habitat Location")]
        [SerializeField] private float _longitude = 0.0f; // Decimal degrees
        [SerializeField] private float _latitude = 0.0f; // Decimal degrees
        [SerializeField] private float _altitude = 0.0f;
        [Header("Updating")]
        [SerializeField] private float _updateInterval = 60f;

        [Header("Debugging")]
        [SerializeField] private bool _debug = false;

        public float Longitude => (_longitude % 360f + 360f) % 360f; // 0 to 360
        public float Latitude => _latitude;
        public float Altitude => _altitude;

        public int TimeZone => Mathf.RoundToInt(Longitude / 15f);
        public double LocalTime => WorldManager.Instance.GlobalTime + TimeZone * 3600f; // local time in seconds
        public double LocalSolarTime => LocalTime + (Longitude - TimeZone * 15f) * 240f; // local solar time in seconds

        public double LocalSolarTimeNow => _localSolarTimeNow;
        public float CurrentSolarIrradiance => _currentSolarIrradiance; // W/m^2
        public float DomeRadius => _domeRadius;
        public float DomeArea => 2.0f * Mathf.PI * Mathf.Pow(DomeRadius, 2); // Surface area of a hemisphere
        public float DomeVolume => (4.0f / 6.0f) * Mathf.PI * Mathf.Pow(DomeRadius, 3); // Volume of a hemisphere
        public float DomeGroundArea => Mathf.PI * Mathf.Pow(DomeRadius, 2);
     
        private float _nominalTemprature = 20.0f;
        private float _temprature = 20.0f; // Celsius
        private float _domeRadius = 35.0f;
        private double _localSolarTimeNow;
        private float _currentSolarIrradiance;
        CountdownTimer _TickTimer;

        void Awake()
        {

            _localSolarTimeNow = LocalSolarTime;
        }
        void Start()
        {
            _TickTimer = new CountdownTimer(_updateInterval, true);
            _TickTimer.Start();
            _localSolarTimeNow = LocalSolarTime;
            _domeRadius = transform.localScale.x / 2.0f;
            UpdateHabitat();
        }
        void Update()
        {
            _localSolarTimeNow = LocalSolarTime;
            _TickTimer.Tick(WorldManager.Instance.deltaTime);
            if (_TickTimer.IsFinished)
            {
                UpdateHabitat();
                _TickTimer.Reset();
                _TickTimer.Start();
                if (_debug) Debug.Log("Local Time: " + LocalTime);
                if (_debug) Debug.Log("Local Solar Time: " + LocalSolarTime);
            }
        }
        void UpdateHabitat()
        {
            if (_debug) Debug.Log("Updating Habitat");
            UpdateSolarIrradiance();
        }
        void UpdateSolarIrradiance()
        {
            float timeOfDay = Calendar.TimeOfDay(LocalTime);
            float hourAngle = CelestialCalculator.HourAngle((float)(LocalSolarTimeNow / 3600.0));
            _currentSolarIrradiance = CelestialCalculator.SolarIrradiance(Latitude, hourAngle, timeOfDay);
            if (_debug) Debug.Log($"--- Current Solar Irradiance: {_currentSolarIrradiance}");
        }

        public static HabitatManager FindClosestHabitat(Vector3 position)
        {
            GameObject[] habitats = GameObject.FindGameObjectsWithTag("Habitat");

            GameObject closestHabitat = null;
            float closestDistance = float.MaxValue;
            foreach (var habitat in habitats)
            {
                float distance = Vector3.Distance(habitat.transform.position, position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestHabitat = habitat;
                }
            }
            if (closestHabitat == null)
            {
                Debug.LogError("Not Habitat: found ");
                return null;
            }
            HabitatManager habitatController = closestHabitat.GetComponent<HabitatManager>();
            Debug.Assert(habitatController != null, "Habitat Controller is not found in Habitat");
            return habitatController;
        }
    }
}
