
namespace PLu.Mars.AI.HTN.Kernel
{
    public interface ISensor
    {
        float TickInterval { get; }
        public IContext Context { get; }
        void Tick(IContext context);
        void DrawGizmos(IContext context);
    }
}