using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.AI.HTN.Kernel
{
    public interface IContext : FluidHTN.IContext
    {
        IAIAgent AIAgent { get; set;}
        //bool LogDecomposition { get; }
        void Update();
    }
}
