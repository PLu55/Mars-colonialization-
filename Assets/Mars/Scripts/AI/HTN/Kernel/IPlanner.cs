using FluidHTN;
 
namespace PLu.Mars.AI.HTN.Kernel
{
    public interface IPlanner<T, U> where T : IContext where U : IDomain
    {
        void Tick(U domain, T context);
    }
}
