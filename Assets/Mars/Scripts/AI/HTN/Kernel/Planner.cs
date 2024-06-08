using System.Collections;
using System.Collections.Generic;
using FluidHTN;
using UnityEngine;
 
namespace PLu.Mars.AI.HTN.Kernel
{
    public class Planner<T> : FluidHTN.Planner<T> where T : FluidHTN.IContext
    {
	    public virtual void Tick(Domain<T> domain, T context)
        {
            base.Tick(domain, context);
        }
    }
}
