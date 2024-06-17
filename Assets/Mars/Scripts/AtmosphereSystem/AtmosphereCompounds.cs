using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.AtmosphereSystem
{
    /// <summary>
    /// Represents the compounds present in the atmosphere.
    /// </summary>
    public class AtmosphereCompounds
    {
        /// <summary>
        /// Gets the amount of oxygen in the atmosphere.
        /// </summary>
        public float Oxygen { get; }

        /// <summary>
        /// Gets the amount of nitrogen in the atmosphere.
        /// </summary>
        public float Nitrogen { get; }

        /// <summary>
        /// Gets the amount of carbon dioxide in the atmosphere.
        /// </summary>
        public float CarbonDioxide { get; }

        /// <summary>
        /// Gets the amount of water vapor in the atmosphere.
        /// </summary>
        public float Water { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AtmosphereCompounds"/> class with default values.
        /// </summary>
        public AtmosphereCompounds() 
        {
            Oxygen = 0;
            Nitrogen = 0;
            CarbonDioxide = 0;
            Water = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AtmosphereCompounds"/> class with the specified amounts of compounds.
        /// </summary>
        /// <param name="oxygen">The amount of oxygen in the atmosphere.</param>
        /// <param name="nitrogen">The amount of nitrogen in the atmosphere.</param>
        /// <param name="carbonDioxide">The amount of carbon dioxide in the atmosphere.</param>
        /// <param name="water">The amount of water vapor in the atmosphere.</param>
        public AtmosphereCompounds(float oxygen, float nitrogen, float carbonDioxide, float water)
        {
            Oxygen = oxygen;
            Nitrogen = nitrogen;
            CarbonDioxide = carbonDioxide;
            Water = water;
        }

        /// <summary>
        /// Adds two instances of <see cref="AtmosphereCompounds"/> together.
        /// </summary>
        /// <param name="a">The first <see cref="AtmosphereCompounds"/> instance.</param>
        /// <param name="b">The second <see cref="AtmosphereCompounds"/> instance.</param>
        /// <returns>A new <see cref="AtmosphereCompounds"/> instance with the combined amounts of compounds.</returns>
        public static AtmosphereCompounds operator +(AtmosphereCompounds a, AtmosphereCompounds b)
        {
            return new AtmosphereCompounds(a.Oxygen + b.Oxygen, a.Nitrogen + b.Nitrogen, a.CarbonDioxide + b.CarbonDioxide, a.Water + b.Water);
        }

        /// <summary>
        /// Returns a string representation of the <see cref="AtmosphereCompounds"/> object.
        /// </summary>
        /// <returns>A string that represents the current <see cref="AtmosphereCompounds"/> object.</returns>
        public new string ToString()
        {
            return $"Gases(Oxygen: {Oxygen}, Nitrogen: {Nitrogen}, CarbonDioxide: {CarbonDioxide}, Water: {Water})";
        }
    }
}