using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluidHTN;
using FluidHTN.Factory;

namespace PLu.Mars.AI.HTN.Kernel
{
    public class DomainBuilder2<T> : BaseDomainBuilder<DomainBuilder2<T>,T> where T : FluidHTN.IContext
    {
        public DomainBuilder2(string name) : base(name, new DefaultFactory())
        {
        }
    }
}
