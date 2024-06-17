using NUnit.Framework;
using PLu.Mars.AtmosphereSystem;
using UnityEngine;

public class AtmosphereCalculatorTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void CalculateOxygenDensityTest()
    {
        // Arrange
        float pressure = 100;
        float temperature = 0f;
        float expectedDensity = 1.41f;

        // Act
        float density = AtmosphereCalculator.CalculateOxygenDensity(pressure, temperature);

        // Assert
        Assert.AreEqual(expectedDensity, density, 0.001f);        
        
        pressure = 101.325f;
        temperature = 25f;
        expectedDensity = 1.308f;
        density = AtmosphereCalculator.CalculateOxygenDensity(pressure, temperature);
        Assert.AreEqual(expectedDensity, density, 0.001f); 
        
        pressure = 60f;
        temperature = 20f;
        expectedDensity = 1.308f;
        density = AtmosphereCalculator.CalculateOxygenDensity(pressure, temperature);
        Debug.Log($"Oxygen density at 60kPa 20 C {density}"); // 0.7877296f
    }
    [Test]
    public void CalculateEnergyForOxygenTest()
    {
        float weight = 1000.0f;
        float expectedEnergy = 2.06f;
        float energy = AtmosphereCalculator.CalculateEnergyForOxygen(weight);
        Assert.AreEqual(expectedEnergy, energy, 0.001f);
    }
    [Test]
    public void CalculateOxygenMassTest()
    {
        float volume = 1000.0f;
        float totalPressure = 60.0f;
        float ppO2 = 21f;
        float temperature = 20f;
        float expectedMass = 1.41440296f;
        float mass = AtmosphereCalculator.CalculateOxygenMass(volume, ppO2, totalPressure, temperature);
        Assert.AreEqual(expectedMass, mass, 1e-6f);
    }
    [Test]
    public void CalculateOxygenVolumeTest()
    {
        float volume = 1000.0f;
        float ppO2 = 21f;
        float totalPressure = 60.0f;
        float expectedVolume = 350.0f;
        float oxygenVolume = AtmosphereCalculator.CalculateOxygenVolume(volume, ppO2, totalPressure);
        Assert.AreEqual(expectedVolume, oxygenVolume, 1e-6f);
    }
    [Test]
    public void CalculatePressureChangeTest()
    {
        float volume = 1000.0f;
        float massOxygen = 1.41440296f;
        float temperature = 20f;
        float expectedPressureChange = 107.73262f;
        float pressureChange = AtmosphereCalculator.CalculatePressureChange(massOxygen, volume, temperature);
        Assert.AreEqual(expectedPressureChange, pressureChange, 1e-6f);
        Debug.Log($"Partial Pressure  {21f + pressureChange}");
    }
 
}
