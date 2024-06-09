using FluidHTN;
using FluidHTN.Operators;
//using Utilities;
using PLu.Mars.AI.HTN.Kernel;

namespace PLu.Mars.AI.HTN.Operators
{
    public class WaitOperator : IOperator
    {
        public float WaitTime { get; set; }
        public WaitOperator(float waitTime)
        {
            WaitTime = waitTime;
        }

        public TaskStatus Update(IContext context)
        {
            if (context is Context ctx)
            {
                if (ctx.GenericTimer <= 0f)
                {
                    ctx.GenericTimer = ctx.CurrentTime + WaitTime;
                    return TaskStatus.Continue;
                }

                if (ctx.CurrentTime < ctx.GenericTimer)
                {
                    return TaskStatus.Continue;
                }

                ctx.GenericTimer = -1f;
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        public void Stop(IContext context)
        {
            if (context is Context ctx)
            {
                ctx.GenericTimer = -1f;
            }
        }

        public void Aborted(IContext context)
        {
            if (context is Context ctx)
            {
                ctx.GenericTimer = -1f;
            }
        }
    }
}