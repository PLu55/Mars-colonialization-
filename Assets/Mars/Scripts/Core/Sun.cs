using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Utilities;
 
namespace PLu.Mars.Core
{
    public class Sun : MonoBehaviour
    {
        private CountdownTimer _timer;
        //localSolarTime
        [SerializeField] private float _longitude;
        [SerializeField] private float _latitude;
        
        void Start()
        {
            _timer = new CountdownTimer(1f);
            _timer.Start();
        }            
        
        void Update()
        {
            _timer.Tick(WorldManager.Instance.deltaTime);
            if (_timer.IsFinished)
            {
                _timer.Reset();
                _timer.Start();
                UpdateSun();
             }
        }
        void UpdateSun()
        {
            Debug.Log("Updating Sun");
            int timeZone = Mathf.RoundToInt(_longitude / 15f);
            double globalTime = WorldManager.Instance.GlobalTime;
            float solarTime = CelestialCalculator.SolarTime(globalTime, _longitude);

            //float solarTime = Calendar.TimeOfDay(GameController.Instance.GlobalTime + timeZone * 3600f);
            float hourAngle = CelestialCalculator.HourAngle(solarTime/3600f);
            float declination = CelestialCalculator.SolarDeclinationAngle( WorldManager.Instance.GlobalTime);
            float altitude = CelestialCalculator.SolarAltitude(_latitude, declination, hourAngle);
            float azimuth =  CelestialCalculator.SolarAzimuth(_latitude, declination, hourAngle);
            transform.localRotation = Quaternion.Euler(altitude * Mathf.Rad2Deg, azimuth * Mathf.Rad2Deg, 0f);
            Debug.Log($"--- GlobalTime: {globalTime:F1} TimeZone: {timeZone} SolarTime: {solarTime:F1}");
            Debug.Log($"--- Altitude: {altitude} Azimuth: {azimuth}");
        }
    }
}
