using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.AI.HTN.Kernel
{
    public class DomainX<T> :  FluidHTN.Domain<T> where T : FluidHTN.IContext
    {
        public DomainX(string name) : base(name)
        {
        }
    }
}
