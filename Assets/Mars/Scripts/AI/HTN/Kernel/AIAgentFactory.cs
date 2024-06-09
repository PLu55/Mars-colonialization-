using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluidHTN;

 namespace PLu.Mars.AI.HTN.Kernel
{
    public abstract class AgentFactory<T> where T : IContext
    {
        public abstract T CreateContext(AIAgent<T> agent);

        public abstract Planner<T> CreatePlanner();

    }
}


