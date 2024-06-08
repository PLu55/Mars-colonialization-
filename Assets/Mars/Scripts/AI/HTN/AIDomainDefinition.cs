using FluidHTN;
using UnityEngine;

namespace PLu.Mars.AI.HTN
{
    public abstract class AIDomainDefinition : ScriptableObject
    {
        public abstract Domain<IContext> Create();
    }
}