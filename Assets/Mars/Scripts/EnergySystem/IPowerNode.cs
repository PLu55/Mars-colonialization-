using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.EnergySystem
{
    public enum PowerNodeType
    {
        PowerProducer,
        PowerConsumer,
        PowerStorage
    }
    public interface IPowerNode
    {
        PowerNodeType PowerNodeType { get; }
        float NominalEffect { get; }
        float CurrentEffectLevel();
    }
}
