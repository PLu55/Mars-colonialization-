using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.AI.HTN.Kernel
{
    public interface IAIAgentFactory<T> where T : IContext
    {
        //IContext CreateContext(IAIAgent<T> agent);
        //public IPlanner CreatePlanner();
    }

}
