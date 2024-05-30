using System;
using UnityEngine;

namespace PLu.Mars.Kernel
{

    // TODO: implement Sidereal time
    public class CelestialCalculator
    {
        //public const float SolarConstant = 589f; // W/m^2
        public const float SolarConstant = 1361f; // W/m^2 at a distance of 1 AU
        public const float MeanDistanceToSun = 1.52f; // Distance to the sum in AU
        public const float Eccentricity = 0.0934f; // Eccentricity of the orbit
        public const float Obliquity = 25.19f; // Obliquity of the planet, the axis tilt.
        public const float SemiMajorAxis = 1.52f; // AU
        public const float SpringEquinox = 167f;  // The spring equinox on the northen hemispher, ~ 1/4 of the year
        public const float OrbitalInclination = 1.85f; // degrees
        public const float OrbitalPeriod = 687f; // days
        public const float PerihelionPhase = 501f; // The phase of the perihelion, when in the year Mars is at the closest point to the sun

        public static float DegreesToRadians (float degrees) => degrees * Mathf.Deg2Rad;

        public static float DistanceToSun(float dayOfYear)  //(float daysSincePerihelion)
        {   
            float day = dayOfYear - PerihelionPhase % OrbitalPeriod;         
            float meanAnomaly = CalculateMeanAnomaly(day);
            float eccentricAnomaly = SolveKeplerEquation(meanAnomaly);
            float trueAnomaly = CalculateTrueAnomaly(eccentricAnomaly);
            float distanceToSun = CalculateDistanceToSun(trueAnomaly);
            return distanceToSun;
        }

        public static float SolarFlux(float distance)
        {
            return SolarConstant / (distance * distance);
        }
        public static float SolarDeclinationAngle(double globalTime)
        {
            return (float)(Obliquity * Mathf.Deg2Rad * Math.Sin(2.0 * Math.PI * (Calendar.DayNumber(globalTime) - SpringEquinox) / Calendar.DaysPerYear));
        }

        public static float HourAngle(float h) // h is the hour of the day in local solar time
        {
            return Mathf.Deg2Rad * 15f * (h - 12f);
        }

        public static float SolarAltitude(float latitude, float declination, float hourAngle)
        {
            return Mathf.Asin(Mathf.Sin(latitude) * Mathf.Sin(declination) + Mathf.Cos(latitude) * Mathf.Cos(declination) * Mathf.Cos(hourAngle));
        }

        public static float SolarAzimuth(float latitude, float declination, float hourAngle, float altitude)
        {
            return Mathf.Acos((Mathf.Sin(declination) - Mathf.Sin(latitude) * Mathf.Sin(altitude)) / (Mathf.Cos(latitude) * Mathf.Cos(altitude)));
        }

        public static float SolarZenithAngle(float latitude, float declination, float hourAngle)
        {

            return Mathf.Acos(Mathf.Sin(latitude) * Mathf.Sin(declination) + Mathf.Cos(latitude) * Mathf.Cos(declination) * Mathf.Cos(hourAngle));
        }

        public static float SolarIrradiance(float latitude, float hourAngle, float globalTime)
        {
            float distance = DistanceToSun(Calendar.DayNumber(globalTime));
            float declination = SolarDeclinationAngle(globalTime);
            float zenitAngle = SolarZenithAngle(latitude, declination, hourAngle);

            return SolarFlux(distance) * Mathf.Clamp01(Mathf.Cos(zenitAngle));
        }
        private static float CalculateMeanAnomaly(float daysSincePerihelion)
        {
            return (float)(2f * Mathf.PI / Calendar.DaysPerYear * daysSincePerihelion);
        }
        // Method to solve Kepler's Equation iteratively to find Eccentric Anomaly (E)
        private static float SolveKeplerEquation(float meanAnomaly)
        {
            float E = meanAnomaly; // Initial guess
            float delta = 1e-6f; // Convergence tolerance
            float difference = 1;
            
            while (difference > delta)
            {
                float newE = meanAnomaly + Eccentricity * Mathf.Sin(E);
                difference = Mathf.Abs(newE - E);
                E = newE;
            }
            
            return E;
        }
        // Method to calculate True Anomaly (nu)
        private static float CalculateTrueAnomaly(float eccentricAnomaly)
        {
            float eccentricity = Eccentricity;
            float tanNuOver2 = Mathf.Sqrt((1 + eccentricity) / (1 - eccentricity)) * Mathf.Tan(eccentricAnomaly / 2);
            return 2 * Mathf.Atan(tanNuOver2);
        }
        // Method to calculate the distance from Mars to the Sun
        private static float CalculateDistanceToSun(float trueAnomaly)
        {
            float eccentricity = Eccentricity;
            float semiMajorAxis = SemiMajorAxis;
            return semiMajorAxis * (1 - Mathf.Pow(eccentricity, 2)) / (1 + eccentricity * Mathf.Cos(trueAnomaly));
        }
    }
}
