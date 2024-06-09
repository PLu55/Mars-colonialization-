using FluidHTN;

namespace PLu.Mars.AI.HTN.Kernel
{    
    public interface IDomainDefinition<T> where T : IContext
    {
        FluidHTN.Domain<T> Create();
    }   
}
