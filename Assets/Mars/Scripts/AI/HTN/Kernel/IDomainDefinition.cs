using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.RepresentationModel;
using UnityEngine;
 

namespace PLu.Mars.AI.HTN.Kernel
{    
    public interface IDomainDefinition<T> where T : FluidHTN.IContext
    {
        FluidHTN.Domain<T> Create();
    }   
}
