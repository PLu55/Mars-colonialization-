using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.Mars.HabitatSystem;


namespace PLu.Mars.AtmosphereSystem
{
    public class AtmosphereCalculator
    {
        public const float UniversalGasConstant = 8.314f; // Universal gas constant in J/(mol·K)
        public const float OxygenMolarMass = 0.032f; // Molar mass of oxygen in kg/mol
        public const float MolarMassOxygen = 32.00f; // g/mol
        public const float FaradayConstant = 96485.33212f; // C/mol
        public const float Voltage = 1.23f; // V (ideal voltage for water electrolysis)
        public const float kWh2J = 3.6e6f; // 1 kWh = 3.6 million Joules
        public const  float OxygenDensity = 1.429f; // g/L
        public const float OxygenMolarVolume = 22.4f; // L/mol
        public const float HumanOxygenConsumption = 30f; // L/hour (at rest) up to factor 8 during exercise
        private float _pressure = 60.0f; // in kPa, normal range 50 to 70 kPa
        private float _oxygenLevel = 0.0f; // ratio of 0.2-0.3

        // Leaf Area Index (LAI) 3 to 7 per ground area 

        public static float CalculateEnergyForOxygen(float gramsOfOxygen)
        {
            // Convert grams of oxygen to moles
            float molesOfOxygen = gramsOfOxygen / MolarMassOxygen;

            // Calculate the total charge required (2 moles of electrons per mole of O2)
            float totalCharge = 2 * molesOfOxygen * FaradayConstant; // in Coulombs

            // Calculate the energy required (Energy = Charge * Voltage)
            float energyInJoules = totalCharge * Voltage; // in Joules

            // Convert energy to kilowatt-hours (1 kWh = 3.6 million Joules)
            float energyInKWh = energyInJoules / kWh2J;

            return energyInKWh;
        }

        public static float CalculatePlantOxygenProduction(int numberOfPlants, float hours)
        {
            float oxygenProductionPerPlantPerHour = 5.0f; // mL of O2 per hour per square meter of leaf area
            float leafAreaPerPlant = 1.0f; // square meters
            
            float totalLeafArea = numberOfPlants * leafAreaPerPlant;
            float totalOxygenProduction = totalLeafArea * oxygenProductionPerPlantPerHour * hours; // in mL
            return totalOxygenProduction / 1000; // Convert mL to liters
        }

        // mass in kg, volume in m^3, pressure in kPa, temperature in K, molar mass in kg/mol
        public static float CalculateOxygenMass(float habitatVolume, float ppO2, float totalPressure, float temperature, float molarMass)
        {
            float oxygenVolumeRatio = ppO2 / totalPressure;
            float oxygenVolume = habitatVolume * oxygenVolumeRatio;
            float n = (ppO2 * oxygenVolume) / (UniversalGasConstant * temperature); // Number of moles
            float mass = n * molarMass; // Mass in kilograms
            return mass;
        }
    }
}
