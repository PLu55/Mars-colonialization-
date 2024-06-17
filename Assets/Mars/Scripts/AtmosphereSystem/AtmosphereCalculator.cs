using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Mars.HabitatSystem;


namespace PLu.Mars.AtmosphereSystem
{
    public class AtmosphereCalculator
    {
            /// <summary>
            /// Universal gas constant in J/(mol·K).
            /// </summary>
            public const float UniversalGasConstant = 8.31446262f;

            /// <summary>
            /// Molar mass of oxygen in g/mol. Actual it is for O2
            /// </summary>
            public const float OxygenMolarMass = 32.00f;

            /// <summary>
            /// Density of oxygen in g/L at standard conditions.
            /// </summary>
            public const float OxygenDensity = 1.429f;

            /// <summary>
            /// Molar volume of oxygen in L/mol.
            /// </summary>
            public const float OxygenMolarVolume = 22.4f;

            /// <summary>
            /// Oxygen consumption rate of a human at rest in L/hour.
            /// </summary>
            public const float HumanOxygenConsumption = 30f;

            /// <summary>
            /// Carbon dioxide production rate of a human in kg/hour.
            /// </summary>
            public const float HumanCarbonDioxideProduction = 0.0417f;

            /// <summary>
            /// Molar mass of water in g/mol.
            /// </summary>
            public const float WaterMolarMass = 18.015f;

            /// <summary>
            /// Density of water in kg/L.
            /// </summary>
            public const float WaterDensity = 1f;

            /// <summary>
            /// Faraday constant in C/mol.
            /// </summary>
            public const float FaradayConstant = 96485.33212f;

            /// <summary>
            /// Ideal voltage for water electrolysis in V.
            /// </summary>
            public const float Voltage = 1.23f;

            /// <summary>
            /// Conversion factor from kilowatt-hours to joules.
            /// </summary>
            public const float kWh2J = 3.6e6f;

            public const float C2K = 273.15f;

            private float _pressure = 60.0f; // in kPa, normal range 50 to 70 kPa
            private float _oxygenLevel = 0.0f; // ratio of 0.2-0.3

            // Leaf Area Index (LAI) 3 to 7 per ground area 
            // 1 m² of leaf area can produce 5 mL of O2 per hour
            // 1 m² of leaf area can produce 1.5 g of dry biomass per hour
            // 1 m² of leaf area can consume 1.2 g of CO2 per hour
            // 1 m² of leaf area can consume 1.5 g of H2O per hour

            
            /// <summary>
            /// Calculates the density of oxygen in kg/m^3 given the pressure and temperature.
            /// </summary>
            /// <param name="pressure">The pressure in kPa.</param>
            /// <param name="temperature">The temperature in Celsius.</param>
            /// <returns>The density of oxygen in kg/m^3.</returns>
            public static float CalculateOxygenDensity(float pressure, float temperature)
            {
                // Convert pressure from kPa to Pa
                pressure *= 1000;
                // Convert temperature from Celsius to Kelvin
                temperature += 273.15f;
                // Calculate the density
                float density = pressure * OxygenMolarMass * 1e-3f / (UniversalGasConstant * temperature);
                return density; // Density in kg/m^3
            }

            /// <summary>
            /// Calculates the energy requirements for producing the given amount of oxygen using electrolysis.
            /// </summary>
            /// <param name="gramsOfOxygen">The amount of oxygen in grams.</param>
            /// <returns>The energy required in kWh.</returns>
            public static float CalculateEnergyForOxygen(float gramsOfOxygen)
            {
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

            /// <summary>
            /// Calculates the total oxygen production by a certain number of plants in a given time period.
            /// </summary>
            /// <param name="leafArea">The total leaf area in square meters.</param>
            /// <param name="hours">The time period in hours.</param>
            /// <returns>The total oxygen production in liters.</returns>
            public static float CalculatePlantOxygenProduction(float leafArea, float hours)
            {
                // Calculate and estimate how much oxygen a certain number of plants can produce 
                // in a given time period, leafArea is in m^2, result is in liters.
                float oxygenProductionIndex = 5.0f; // mL of O2 per hour per square meter of leaf area
                
                float totalOxygenProduction = leafArea * oxygenProductionIndex * hours; // in mL
                return totalOxygenProduction / 1000; // Convert mL to liters
            }

            /// <summary>
            /// Calculates the mass of oxygen in kg given the volume, partial pressure of oxygen, total pressure, temperature, and molar mass.
            /// </summary>
            /// <param name="volume">The volume in m^3.</param>
            /// <param name="ppO2">The partial pressure of oxygen in kPa.</param>
            /// <param name="totalPressure">The total pressure in kPa.</param>
            /// <param name="temperature">The temperature in C.</param>
            /// <param name="molarMass">The molar mass of oxygen in kg/mol.</param>
            /// <returns>The mass of oxygen in kg.</returns>
            public static float CalculateOxygenMass(float volume, float ppO2, float totalPressure, float temperature)
            {
                // mass in kg, volume in m^3, pressure in kPa, temperature in K, molar mass in kg/mol
                
                float oxygenVolumeRatio = ppO2 / totalPressure;
                float oxygenVolume = volume * oxygenVolumeRatio;
                float n = (ppO2 * oxygenVolume) / (UniversalGasConstant * (temperature + C2K)); // Number of moles
                float mass = n * OxygenMolarMass * 1e-3f; // Mass in kilograms
                return mass; 
            }

            /// <summary>
            /// Calculates the volume of oxygen in a given volume of gas mixture, given the partial pressure of oxygen, total pressure, and temperature.
            /// </summary>
            /// <param name="volume">The volume of the gas mixture in m^3.</param>
            /// <param name="ppO2">The partial pressure of oxygen in kPa.</param>
            /// <param name="totalPressure">The total pressure in kPa.</param>
            /// <returns>The volume of oxygen in m^3.</returns>
            public static float CalculateOxygenVolume(float volume, float ppO2, float totalPressure)
            {
                float oxygenVolumeRatio = ppO2 / totalPressure;
                float oxygenVolume = volume * oxygenVolumeRatio;
                return oxygenVolume;
            }

            /// <summary>
            /// Calculates the change in pressure based on the mass of oxygen, volume, and temperature.
            /// </summary>
            /// <param name="massOxygen">The mass of oxygen in kilograms.</param>
            /// <param name="volume">The volume in cubic meters.</param>
            /// <param name="temperature">The temperature in C.</param>
            /// <returns>The change in pressure in kPa.</returns>
            public static float CalculatePressureChange(float massOxygen, float volume, float temperature)
            {
                float M = OxygenMolarMass * 1e-3f; // Molar mass of oxygen in kg/mol

                // Calculate the number of moles of oxygen added
                float molesOxygen = massOxygen / M;

                // Calculate the change in partial pressure of oxygen
                float pressureChange = (molesOxygen * UniversalGasConstant * (temperature + C2K))  / volume;

                return pressureChange * 1e-3f; // Pressure change in kPa
            }

            /// <summary>
            /// Calculates the increase in vapor pressure of water in a closed volume of air.
            /// </summary>
            /// <param name="volumeOfWater">The volume of water in liters.</param>
            /// <param name="temperature">The temperature in Kelvin.</param>
            /// <param name="volumeOfAir">The volume of air in m^3.</param>
            /// <returns>The increase in vapor pressure in kPa.</returns>
            public static float CalculateVaporPressureIncrease(float volumeOfWater, float temperature, float volumeOfAir)
            {
                // Calculate the increase in vapor pressure of water in a closed volume of air
                // Volume of water in liters, temperature in Kelvin, volume of air in m³
                // Convert volume of water to mass in grams
                float massOfWater = volumeOfWater * WaterDensity * 1000; // 1 kg/L * 1000 g/kg
                // Calculate the increase in vapor pressure
                float deltaP = (massOfWater * UniversalGasConstant * temperature) / (WaterMolarMass * volumeOfAir);
                return deltaP;
            }

            /// <summary>
            /// Calculates the relative humidity given the water vapor pressure and saturation vapor pressure.
            /// </summary>
            /// <param name="waterVaporPressure">The water vapor pressure in kPa.</param>
            /// <param name="saturationVaporPressure">The saturation vapor pressure in kPa.</param>
            /// <returns>The relative humidity as a percentage.</returns>
            public static float CalculateRelativeHumidity(float waterVaporPressure, float saturationVaporPressure)
            {
                float relativeHumidity = (waterVaporPressure / saturationVaporPressure) * 100;
                return relativeHumidity;
            }

            /// <summary>
            /// Calculates the saturation vapor pressure in kPa at a given temperature in degrees Celsius.
            /// </summary>
            /// <param name="temperature">The temperature in degrees Celsius.</param>
            /// <returns>The saturation vapor pressure in kPa.</returns>
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

            /// <summary>
            /// Calculates the dew point temperature in degrees Celsius given the temperature in degrees Celsius and relative humidity.
            /// </summary>
            /// <param name="temperature">The temperature in degrees Celsius.</param>
            /// <param name="relativeHumidity">The relative humidity as a percentage.</param>
            /// <returns>The dew point temperature in degrees Celsius.</returns>
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
