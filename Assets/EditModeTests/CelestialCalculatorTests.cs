using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using PLu.Mars.Core;

public class CelestialCalculatorTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void DistanceToSunTest()
    {
        float distance = CelestialCalculator.DistanceToSun(CelestialCalculator.PerihelionPhase);
        Assert.Less(distance-  1.378, 0.001f);

        distance = CelestialCalculator.DistanceToSun(CelestialCalculator.SpringEquinox);
        Assert.Less(distance - 1.662f, 0.001f);
        
        int step = 28;
        for (int i = 0; i < 668; i+=step)
        {
            distance = CelestialCalculator.DistanceToSun(i);
            Assert.Less(distance, 1.662f);
            Assert.Greater(distance, 1.378f);
            float solarFlux = CelestialCalculator.SolarFlux(distance);
            Debug.Log($"distance {distance} solarFlux {solarFlux}");
            Assert.Less(solarFlux, 717f);
            Assert.Greater(solarFlux, 492f);
         }
    }

    [Test]
    public void SolarDeclinationAngleTest()
    {
        double t = Calendar.DayLength * 0.0;
        float angle = CelestialCalculator.SolarDeclinationAngle(t);
        Assert.Less(Mathf.Abs(angle + CelestialCalculator.Obliquity * Mathf.Deg2Rad), 0.001f);

        t = Calendar.DayLength * CelestialCalculator.SpringEquinox;
        angle = CelestialCalculator.SolarDeclinationAngle(t);
        Assert.Less(Mathf.Abs(angle), 0.01f);

        angle = CelestialCalculator.SolarDeclinationAngle(2.0 * t);
        Assert.Less(Mathf.Abs(angle - CelestialCalculator.Obliquity * Mathf.Deg2Rad), 0.01f);  
    }
    [Test]
    public void HourAngleTest()
    {
        float angle = CelestialCalculator.HourAngle(0);
        Assert.AreEqual(-Mathf.PI, angle);
        angle = CelestialCalculator.HourAngle(12);
        Assert.AreEqual(0, angle);
        angle = CelestialCalculator.HourAngle(6);
        Assert.AreEqual(-Mathf.PI / 2f, angle);
        angle = CelestialCalculator.HourAngle(18);
        Assert.AreEqual(Mathf.PI / 2f, angle);
    }

    [Test]
    public void SolarZenithAngleTest()
    {
        float latitude = 0;
        float declination = 0;
        float hourAngle = 0;
        float angle = CelestialCalculator.SolarZenithAngle(latitude, declination, hourAngle);
        Assert.AreEqual(0, angle);

        latitude = 0;
        declination = 0;
        hourAngle = Mathf.PI / 2f;
        angle = CelestialCalculator.SolarZenithAngle(latitude, declination, hourAngle);
        Assert.AreEqual(Mathf.PI / 2.0f, angle);

        hourAngle = Mathf.PI;
        angle = CelestialCalculator.SolarZenithAngle(latitude, declination, hourAngle);
        Assert.AreEqual(Mathf.PI, angle);
        
        hourAngle = 0;
        latitude = Mathf.PI / 2;
        angle = CelestialCalculator.SolarZenithAngle(latitude, declination, hourAngle);
        Assert.AreEqual(Mathf.PI / 2, angle);

        latitude = Mathf.PI / 4;
        angle = CelestialCalculator.SolarZenithAngle(latitude, declination, hourAngle);
        Assert.AreEqual(Mathf.PI / 4, angle);

        latitude = 0;
        declination = Mathf.PI / 2;
        hourAngle = Mathf.PI / 2;
        angle = CelestialCalculator.SolarZenithAngle(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(Mathf.PI/2f).Within(0.001f));

        latitude = Mathf.PI / 2;
        declination = Mathf.PI / 2;
        hourAngle = Mathf.PI / 2;
        angle = CelestialCalculator.SolarZenithAngle(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(0f).Within(0.001f));
        
        latitude = Mathf.PI / 4;
        declination = Mathf.PI / 4;
        hourAngle = Mathf.PI / 4;
        angle = CelestialCalculator.SolarZenithAngle(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(0.548028469f).Within(0.001f));
    }

    [Test]
    public void SolarAltitudeTest()
    {
        float latitude = 0;
        float declination = 0;
        float hourAngle = 0;
        float angle = CelestialCalculator.SolarAltitude(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(Mathf.PI / 2f).Within(0.001f));

        latitude = 0;
        declination = 0;
        hourAngle = Mathf.PI / 2f;
        angle = CelestialCalculator.SolarAltitude(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(0f).Within(0.001f));

        hourAngle = Mathf.PI;
        angle = CelestialCalculator.SolarAltitude(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(-Mathf.PI / 2f).Within(0.001f));
        
        hourAngle = 0;
        latitude = Mathf.PI / 2;
        angle = CelestialCalculator.SolarAltitude(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(0f).Within(0.001f));

        latitude = Mathf.PI / 4;
        angle = CelestialCalculator.SolarAltitude(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(Mathf.PI / 4f).Within(0.001f));

        latitude = 0;
        declination = Mathf.PI / 2;
        hourAngle = Mathf.PI / 2;
        angle = CelestialCalculator.SolarAltitude(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(0f).Within(0.001f));

        latitude = Mathf.PI / 2;
        declination = Mathf.PI / 2;
        hourAngle = Mathf.PI / 2;
        angle = CelestialCalculator.SolarAltitude(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(Mathf.PI / 2).Within(0.001f));
        
        latitude = Mathf.PI / 4;
        declination = Mathf.PI / 4;
        hourAngle = Mathf.PI / 4;
        angle = CelestialCalculator.SolarAltitude(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(1.0227679f).Within(0.001f));
    }

    [Test]
    public void SubSolarAzimuth()
    {
        float latitude = 0;
        float declination = 0;
        float globalTime = 0;
        float hourAngle = 0;
        float angle = CelestialCalculator.SubSolarAzimuth(latitude, declination, hourAngle, globalTime);

        for(int i = 0; i < 24; i+=3)
        {

            globalTime = i * 3600;
            hourAngle = CelestialCalculator.HourAngle(globalTime/3600f);
            declination = CelestialCalculator.SolarDeclinationAngle(globalTime);
            angle = CelestialCalculator.SubSolarAzimuth(latitude, declination, hourAngle, globalTime);
            Debug.Log($"globalTime {globalTime / 3600f} hourAngle {hourAngle} declination {declination} angle {angle}");
        }

        Assert.That(angle, Is.EqualTo(0f).Within(0.001f));

        latitude = 0;
        declination = 0;
        hourAngle = Mathf.PI / 2f;
        angle = CelestialCalculator.SubSolarAzimuth(latitude, declination, hourAngle,  globalTime);
        Assert.That(angle, Is.EqualTo(Mathf.PI / 2f).Within(0.001f));

        latitude = 0;
        declination = 0;
        hourAngle = -Mathf.PI / 2f;
        angle = CelestialCalculator.SubSolarAzimuth(latitude, declination, hourAngle, globalTime);
        Assert.That(angle, Is.EqualTo(Mathf.PI * 3f / 2f).Within(0.001f));

        hourAngle = Mathf.PI;
        angle = CelestialCalculator.SubSolarAzimuth(latitude, declination, hourAngle, globalTime);
        Assert.That(angle, Is.EqualTo(Mathf.PI).Within(0.001f));
        
        hourAngle = 0;
        latitude = Mathf.PI / 2;
        angle = CelestialCalculator.SubSolarAzimuth(latitude, declination, hourAngle, globalTime);
        Assert.That(angle, Is.EqualTo(0f).Within(0.001f));

        latitude = Mathf.PI / 4;
        angle = CelestialCalculator.SubSolarAzimuth(latitude, declination, hourAngle, globalTime);
        Assert.That(angle, Is.EqualTo(0f).Within(0.001f));

        latitude = 0;
        declination = Mathf.PI / 2;
        hourAngle = Mathf.PI / 2;
        angle = CelestialCalculator.SubSolarAzimuth(latitude, declination, hourAngle, globalTime);
        Assert.That(angle, Is.EqualTo(2f * Mathf.PI).Within(0.001f));

        latitude = Mathf.PI / 2;
        declination = Mathf.PI / 2;
        hourAngle = Mathf.PI / 2;
        angle = CelestialCalculator.SubSolarAzimuth(latitude, declination, hourAngle, globalTime);
        Assert.That(angle, Is.EqualTo(0f).Within(0.001f));
        
        latitude = Mathf.PI / 4;
        declination = Mathf.PI / 4;
        hourAngle = Mathf.PI / 4;

    }
    [Test]
    public void SolarAzmuthTest()
    {
        float latitude = 0;
        float declination = 0;
        float hourAngle = 0;
        float angle = CelestialCalculator.SolarAzimuth(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(0f).Within(0.001f));

        latitude = 0;
        declination = 0;
        hourAngle = Mathf.PI / 2f;
        angle = CelestialCalculator.SolarAzimuth(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(Mathf.PI / 2f).Within(0.001f));

        latitude = 0;
        declination = 0;
        hourAngle = -Mathf.PI / 2f;
        angle = CelestialCalculator.SolarAzimuth(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(Mathf.PI * 3f / 2f).Within(0.001f));

        hourAngle = Mathf.PI;
        angle = CelestialCalculator.SolarAzimuth(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(Mathf.PI).Within(0.001f));
        
        hourAngle = 0;
        latitude = Mathf.PI / 2;
        angle = CelestialCalculator.SolarAzimuth(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(0f).Within(0.001f));

        latitude = Mathf.PI / 4;
        angle = CelestialCalculator.SolarAzimuth(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(0f).Within(0.001f));

        latitude = 0;
        declination = Mathf.PI / 2;
        hourAngle = Mathf.PI / 2;
        angle = CelestialCalculator.SolarAzimuth(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(2f * Mathf.PI).Within(0.001f));

        latitude = Mathf.PI / 2;
        declination = Mathf.PI / 2;
        hourAngle = Mathf.PI / 2;
        angle = CelestialCalculator.SolarAzimuth(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(0f).Within(0.001f));
        
        latitude = Mathf.PI / 4;
        declination = Mathf.PI / 4;
        hourAngle = Mathf.PI / 4;
        angle = CelestialCalculator.SolarAzimuth(latitude, declination, hourAngle);
        Assert.That(angle, Is.EqualTo(0f).Within(0.001f));
    }

    private void Zenit(float latitude, float hourAngle, float globalTime)
    {
        float declination = CelestialCalculator.SolarDeclinationAngle(globalTime);
        float zenitAngle = CelestialCalculator.SolarZenithAngle(latitude, declination, hourAngle);
        float dayNumber = Calendar.DayNumber(globalTime);
        Debug.Log($"latitude {latitude} declination {declination} hourAngle {hourAngle} zenitAngle {zenitAngle} dayNumber {dayNumber}");
    }
    [Test]
    public void SolarIrradianceTest()
    {
        float latitude = 0;
        float hourAngle = 0;
        float globalTime = 0;
        float irradiance = CelestialCalculator.SolarIrradiance(latitude, hourAngle, globalTime);
        Assert.Less(Mathf.Abs(irradiance - 523.523f), 0.01f);

        latitude = 0f;
        hourAngle = 0f;
        globalTime = CelestialCalculator.PerihelionPhase * (float)Calendar.DayLength;
        irradiance = CelestialCalculator.SolarIrradiance(latitude, hourAngle, globalTime);
        float declination = CelestialCalculator.SolarDeclinationAngle(globalTime);
        Assert.Less(Mathf.Abs(irradiance - 716.70f), 0.01f);

        latitude = 0f;
        hourAngle = Mathf.PI;
        globalTime = CelestialCalculator.SpringEquinox * (float)Calendar.DayLength;
        irradiance = CelestialCalculator.SolarIrradiance(latitude, hourAngle, globalTime);
        Assert.Less(Mathf.Abs(irradiance) , 0.01f);

        latitude = 0f;
        hourAngle = Mathf.PI / 2f;
        globalTime = CelestialCalculator.SpringEquinox * (float)Calendar.DayLength;
        irradiance = CelestialCalculator.SolarIrradiance(latitude, hourAngle, globalTime);
        Assert.Less(Mathf.Abs(irradiance) , 0.01f);
    
        latitude = 0f;
        hourAngle = Mathf.PI / 4f;
        globalTime = (CelestialCalculator.PerihelionPhase + 334f) * (float)Calendar.DayLength; 
        irradiance = CelestialCalculator.SolarIrradiance(latitude, hourAngle, globalTime);
        Zenit( latitude, hourAngle, globalTime);     
        Debug.Log($"at line 152 irradiance {irradiance}");
        Assert.Less(Mathf.Abs(irradiance - 348.416f), 0.001f);
    
        latitude = 0f;
        hourAngle = 0f;
        globalTime = (CelestialCalculator.PerihelionPhase + 334f - 10f) * (float)Calendar.DayLength; 
        irradiance = CelestialCalculator.SolarIrradiance(latitude, hourAngle, globalTime);
        Zenit( latitude, hourAngle, globalTime);
        Debug.Log($"at line 159 irradiance {irradiance}");
        Assert.Less(Mathf.Abs(irradiance - 492.607f) , 0.001f);


        latitude = Mathf.PI / 4;
        hourAngle = Mathf.PI / 4;
        globalTime = 0;
        irradiance = CelestialCalculator.SolarIrradiance(latitude, hourAngle, globalTime);
        Debug.Log($"at line 161 irradiance {irradiance}");
        Assert.Less(Mathf.Abs(irradiance - 87.644f), 0.001f);

        latitude = Mathf.PI / 4;
        hourAngle = Mathf.PI / 4;
        globalTime = (float)Calendar.DayLength * 0;
        irradiance = CelestialCalculator.SolarIrradiance(latitude, hourAngle, globalTime);
        Assert.Less(irradiance - 491.93f, 0.01f);

        latitude = Mathf.PI / 4;
        hourAngle = Mathf.PI / 4;
        globalTime = (float)Calendar.DayLength * 668;
        irradiance = CelestialCalculator.SolarIrradiance(latitude, hourAngle, globalTime);
        Assert.Less(irradiance - 491.93f, 0.01f);

        latitude = Mathf.PI / 4;
        hourAngle = Mathf.PI / 4;
        globalTime = (float)Calendar.DayLength * 668 + 3600;
        irradiance = CelestialCalculator.SolarIrradiance(latitude, hourAngle, globalTime);
        Assert.Less(irradiance - 491.93f, 0.01f);

        latitude = Mathf.PI / 4;
        hourAngle = Mathf.PI / 4;
        globalTime = (float)Calendar.DayLength * 668 + 86400 - 3600;
        irradiance = CelestialCalculator.SolarIrradiance(latitude, hourAngle, globalTime);
        Assert.Less(irradiance - 491.93f, 0.01f);
    }
 
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator CelestialCalculatorWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
