using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Mars.HabitatSystem;


namespace PLu.Mars.AtmosphereSystem
{
    public class AtmosphereCalculator
    {
        public const float GasConstant = 8.314f; // Universal gas constant in J/(mol·K)
        public const float OxygenMolarMass = 32.00f; // g/mol
        public const float OxygenDensity = 1.429f; // g/L
        public const float OxygenMolarVolume = 22.4f; // L/mol
        public const float HumanOxygenConsumption = 30f; // L/hour (at rest) up to factor 8 during exercise
        public const float WaterMolarMass = 18.015f; // g/mol
        public const float WaterDensity = 1f; // in kg/L
        public const float FaradayConstant = 96485.33212f; // C/mol
        public const float Voltage = 1.23f; // V (ideal voltage for water electrolysis)
        public const float kWh2J = 3.6e6f; // 1 kWh = 3.6 million Joules

        private float _pressure = 60.0f; // in kPa, normal range 50 to 70 kPa
        private float _oxygenLevel = 0.0f; // ratio of 0.2-0.3

        // Leaf Area Index (LAI) 3 to 7 per ground area 

        public static float CalculateEnergyForOxygen(float gramsOfOxygen)
        {
            // calculat the energy requirements for producing the given amount of oxygen using electrolysis.

            // Convert grams of oxygen to moles
            float molesOfOxygen = gramsOfOxygen / OxygenMolarMass;

            // Calculate the total charge required (2 moles of electrons per mole of O2)
            float totalCharge = 2 * molesOfOxygen * FaradayConstant; // in Coulombs

            // Calculate the energy required (Energy = Charge * Voltage)
            float energyInJoules = totalCharge * Voltage; // in Joules

            // Convert energy to kilowatt-hours (1 kWh = 3.6 million Joules)
            float energyInKWh = energyInJoules / kWh2J;

            return energyInKWh;
        }

        public static float CalculatePlantOxygenProduction(float leafArea, float hours)
        {
            // Calculate and estimate how much oxygen a certain number of plants can produce 
            // in a given time period, leafArea is in m^2, result is in liters.
            float oxygenProductionIndex = 5.0f; // mL of O2 per hour per square meter of leaf area
            
            float totalOxygenProduction = leafArea * oxygenProductionIndex * hours; // in mL
            return totalOxygenProduction / 1000; // Convert mL to liters
        }
        public static float CalculateVaporPressureIncrease(float volumeOfWater, float temperature, float volumeOfAir)
        {
            // Calculate the increase in vapor pressure of water in a closed volume of air
            // Volume of water in liters, temperature in Kelvin, volume of air in m³
            // Convert volume of water to mass in grams
            float massOfWater = volumeOfWater * WaterDensity * 1000; // 1 kg/L * 1000 g/kg
            // Calculate the increase in vapor pressure
            float deltaP = (massOfWater * GasConstant * temperature) / (WaterMolarMass * volumeOfAir);
            return deltaP;
        }
        public static float CalculateOxygenMass(float habitatVolume, float ppO2, float totalPressure, float temperature, float molarMass)
        {
            // mass in kg, volume in m^3, pressure in kPa, temperature in K, molar mass in kg/mol
            
            float oxygenVolumeRatio = ppO2 / totalPressure;
            float oxygenVolume = habitatVolume * oxygenVolumeRatio;
            float n = (ppO2 * oxygenVolume) / (GasConstant * temperature); // Number of moles
            float mass = n * molarMass; // Mass in kilograms
            return mass;
        }

        public static float CalculateOxygenVolume(float habitatVolume, float ppO2, float totalPressure, float temperature)
        {
            float oxygenVolumeRatio = ppO2 / totalPressure;
            float oxygenVolume = habitatVolume * oxygenVolumeRatio;
            return oxygenVolume;
        }

        public static float CalculateRelativeHumidity(float waterVaporPressure, float saturationVaporPressure)
        {
            float relativeHumidity = (waterVaporPressure / saturationVaporPressure) * 100;
            return relativeHumidity;
        }

        public static float CalculateSaturationVaporPressure(float temperature)
        {
            // Returns the saturation vapor pressure in kPa at a given temperature in degrees Celsius
            // Calculate saturation vapor pressure using the Tetens equation, for T >= 0 degrees Celsius
            // Murray's version used for T < 0 degrees Celsius.
            // The actual Vapor Pressure is the saturation vapor pressure times the relative humidity
            // and it can be calculated using Td the dew point temprature as temprature.
            // Reference: https://en.wikipedia.org/wiki/Tetens_equation

            float saturationVaporPressure;

            if (temperature >= 0)
            {
                 saturationVaporPressure = 0.6108f * Mathf.Exp(17.27f * temperature / (temperature + 237.3f));
            }
            else
            {
                saturationVaporPressure = 0.6108f * Mathf.Exp(21.875f * temperature / (temperature + 265.5f));
            }
            return saturationVaporPressure;
        }
        public static float CalculateDewPoint(float temperature, float relativeHumidity)
        {
            // Calculate the dew point temperature in degrees Celsius given the temperature in degrees Celsius.
            float a = 17.27f;
            float b = 237.7f;
            
            float gamma = (a * temperature) / (b + temperature) + Mathf.Log(relativeHumidity / 100.0f);
            float dewPoint = (b * gamma) / (a - gamma);
            
            return dewPoint;
        }
    }
}
