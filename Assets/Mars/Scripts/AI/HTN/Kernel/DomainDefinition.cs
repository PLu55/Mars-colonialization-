using FluidHTN;
using UnityEngine;
 
namespace PLu.Mars.AI.HTN.Kernel
{
    public abstract class DomainDefinition<T> : ScriptableObject, IDomainDefinition<T> where T : FluidHTN.IContext
    {
        public abstract FluidHTN.Domain<T> Create();
    }
}