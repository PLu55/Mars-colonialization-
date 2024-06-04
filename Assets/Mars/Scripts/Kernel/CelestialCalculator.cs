using System;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UIElements;

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
        public const float OrbitalPeriodSidereal = 687f; // days
        public const float OrbitalPeriodSynodic = 780f; // days
        public const float PerihelionPhase = 501f; // The phase of the perihelion, when in the year Mars is at the closest point to the sun

        public static float DegreesToRadians (float degrees) => degrees * Mathf.Deg2Rad;

        public static float DistanceToSun(float dayOfYear)  //(float daysSincePerihelion)
        {   
            float day = dayOfYear - PerihelionPhase % OrbitalPeriodSidereal;         
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
            // Compute solar declination, the angle between the rays of the sun and the plane of the Earth's equator.

            return (float)(Obliquity * Mathf.Deg2Rad * Math.Sin(2.0 * Math.PI * (Calendar.DayNumber(globalTime) - SpringEquinox) / Calendar.DaysPerYear));
        }

        public static float HourAngle(float h) // h is the hour of the day in local solar time
        {
            return Mathf.Deg2Rad * 15f * (h - 12f);
        }

        public static float SolarTime(double globalTime, float longitude)
        {
            double timeZone = longitude / 15.0;
            double solarTime = globalTime+ timeZone * 3600.0 % Calendar.DayLength;
            return (float)solarTime;
        }
        public static float SolarAltitude(float latitude, float declination, float hourAngle)
        {
            // Calculate the solar altitude, the angle between the sun and the horizon. 
            
            
            return Mathf.Asin(Mathf.Sin(latitude) * Mathf.Sin(declination) + Mathf.Cos(latitude) * Mathf.Cos(declination) * Mathf.Cos(hourAngle));
        }

        public static float SubSolarAzimuth(float latitude, float declination, float hourAngle, float globalTime)
        {
            // Calculate the solar azimuth using subsolar point, the compass direction from which the sunlight is coming, clockwise
            // https://en.wikipedia.org/wiki/Solar_azimuth_angle

            // Leaving out Emin, the equation of time, it's not needed in the application
            float subsolarHourAngle = -Mathf.Deg2Rad * 15f * (float)(globalTime % Calendar.DayLength / 3660f - 12.0);
            float sx = Mathf.Cos(declination) * Mathf.Sin(subsolarHourAngle - hourAngle);
            float sy = Mathf.Cos(latitude) * Mathf.Sin(declination) - Mathf.Sin(latitude) * Mathf.Cos(declination) * Mathf.Cos(subsolarHourAngle - hourAngle);


            float sz = Mathf.Sin(latitude) * Mathf.Sin(declination) - Mathf.Cos(latitude) * Mathf.Cos(declination) * Mathf.Cos(subsolarHourAngle - hourAngle);
            //float sz = 
            //float angle = Mathf.Atan2(-sx, -sy); // South-Clockwise
            float angle = Mathf.Atan2(sx, sy); // North-Clockwise
            // float angle = Mathf.Atan2(sy, sx); // East-CounterClockwise
            return angle;
        }
        public static float SolarAzimuth(float latitude, float declination, float hourAngle)
        {
            // Calculate the solar azimuth,  the compass direction from which the sunlight is coming, clockwise
            // https://en.wikipedia.org/wiki/Solar_azimuth_angle

            float zenitAngle = SolarZenithAngle(latitude, declination, hourAngle);
            if (zenitAngle < 1e-6) // Mathf.PI / 2)
            {
                return 0f;
            }
            Debug.Log($"Zenit: {zenitAngle} ");
            //float angle = Mathf.Asin(Mathf.Sin(hourAngle) * Mathf.Cos(declination) / Mathf.Sin(zenitAngle));
            float angle = Mathf.Acos((Mathf.Sin(declination) - Mathf.Cos(zenitAngle) * Mathf.Sin(latitude)) / (Mathf.Sin(zenitAngle) * Mathf.Cos(latitude)));
            if (zenitAngle >= Mathf.PI / 2)
            {
                angle = Mathf.PI - angle;
            }

            else if (angle > 2f * Mathf.PI)
            {
                angle -= 2f * Mathf.PI;
            }
            // float angle =  Mathf.Acos((Mathf.Sin(declination) * Mathf.Cos(latitude) - Mathf.Cos(hourAngle) * Mathf.Cos(declination) * Mathf.Sin(latitude)) / (Mathf.Sin(zenitAngle)));
 
            if (angle < 0)
            {
                angle += 2f * Mathf.PI;
            }
            else if (angle > 2f * Mathf.PI)
            {
                angle -= 2f * Mathf.PI;
            }
            return angle;
        }
        public static float SolarAzimuth2(float latitude, float declination, float hourAngle)
        {
            // Calculate the solar azimuth,  the compass direction from which the sunlight is coming, clockwise
            // https://en.wikipedia.org/wiki/Solar_azimuth_angle

            float zenitAngle = SolarZenithAngle(latitude, declination, hourAngle);
            return Mathf.Acos((Mathf.Sin(declination) * Mathf.Cos(latitude) - Mathf.Cos(hourAngle) * Mathf.Cos(declination) * Mathf.Sin(latitude)) / (Mathf.Sin(zenitAngle)));
        }
        public static float SolarAzimuthXX(float latitude, float declination, float hourAngle, float altitude)
        {
            // Calculate the solar azimuth,  the compass direction from which the sunlight is coming, clockwise
            // https://en.wikipedia.org/wiki/Solar_azimuth_angle

            float sinAzimuth = (Mathf.Cos(declination) * Mathf.Sin(hourAngle)) / Mathf.Cos(altitude);
            float cosAzimuth = (Mathf.Sin(altitude) - Mathf.Sin(latitude) * Mathf.Sin(declination)) / (Mathf.Cos(latitude) * Mathf.Cos(declination));
            float azimuth =  Mathf.Atan2(sinAzimuth, cosAzimuth);
            if (azimuth < 0)
            {
                azimuth += 2f * Mathf.PI;
            }
            return azimuth;
        }
        public static float SolarAzimuthX(float latitude, float declination, float hourAngle, float altitude)
        {
            // Calculate the solar azimuth, the angle between the direction of the sun and true north, measured clockwise on the horizontal plane.
            
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
