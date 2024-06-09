using FluidHTN;
using UnityEngine;
 
namespace PLu.Mars.AI.HTN.Kernel
{
    public abstract class DomainDefinition<T> : ScriptableObject, IDomainDefinition<T> where T : IContext
    {
        public abstract Domain<T> Create();
    }
}