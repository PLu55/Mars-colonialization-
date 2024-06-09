using FluidHTN;
using PLu.Mars.AI.HTN.Kernel;

namespace PLu.Mars.AI.HTN.Operators
{
    public class MoveToOperator : IOperator
    {
        public TaskStatus StartNavigation(IContext context)
        { 
            if (context is Context ctx)
            {
                ctx.NavAgent.isStopped = false;
                if (ctx.NavAgent.SetDestination(ctx.CurrentTargetPosition))
                {
                    return TaskStatus.Continue;
                }

                return TaskStatus.Failure;
            }  
            
            return TaskStatus.Failure;
        }

        public TaskStatus UpdateNavigation(IContext context)
        {
            if (context is Context ctx)
            {
                if (ctx.NavAgent.remainingDistance <= ctx.NavAgent.stoppingDistance)
                {
                    ctx.NavAgent.isStopped = true;
                    return TaskStatus.Success;
                }

                return TaskStatus.Continue;
            }

            return FluidHTN. TaskStatus.Failure;
        }

        public TaskStatus Update(IContext context)
        {
            if (context is Context ctx)
            {
                if (ctx.NavAgent.isStopped)
                {
                    return StartNavigation(ctx);
                }
                else
                {
                    return UpdateNavigation(ctx);
                }
            }

            return TaskStatus.Failure;
        }
        public void Stop(IContext context)
        {
            if (context is Context ctx)
            {
                ctx.NavAgent.isStopped = true;
            }
        }

        public void Aborted(IContext context)
        {
            if (context is Context ctx)
            {
                ctx.NavAgent.isStopped = true;
            }
        }
    }
}