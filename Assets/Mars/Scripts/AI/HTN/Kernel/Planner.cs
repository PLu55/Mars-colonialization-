using System.Collections;
using System.Collections.Generic;
using FluidHTN;
using UnityEngine;
 
namespace PLu.Mars.AI.HTN.Kernel
{
    public class Planner<T> : FluidHTN.Planner<T> where T : IContext
    {
	    public virtual void Tick(Domain<T> domain, T context)
        {
            //Debug.Log("Planner Tick");
            base.Tick(domain, context);
        }
    }
}
