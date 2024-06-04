using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu
{
    public class AtmosphereCompounds
    {
        public float Oxygen { get; }
        public float Nitrogen { get; }
        public float CarbonDioxide { get; }
        public float Water { get; }

        public AtmosphereCompounds() 
        {
            Oxygen = 0;
            Nitrogen = 0;
            CarbonDioxide = 0;
            Water = 0;
        }

        public AtmosphereCompounds(float oxygen, float nitrogen, float carbonDioxide, float water)
        {
            Oxygen = oxygen;
            Nitrogen = nitrogen;
            CarbonDioxide = carbonDioxide;
            Water = water;
        }

        public static AtmosphereCompounds operator +(AtmosphereCompounds a, AtmosphereCompounds b)
        {
            return new AtmosphereCompounds(a.Oxygen + b.Oxygen, a.Nitrogen + b.Nitrogen, a.CarbonDioxide + b.CarbonDioxide, a.Water + b.Water);
        }

        public new string ToString()
        {
            return $"Gases(Oxygen: {Oxygen}, Nitrogen: {Nitrogen}, CarbonDioxide: {CarbonDioxide}, Water: {Water})";
        }
    }
}