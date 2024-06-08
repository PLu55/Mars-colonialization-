using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.AI.HTN.Kernel
{
    public interface IPlanner<T, U> where T : IContext where U : IDomain
    {
        void Tick(U domain, T context);
    }
}
