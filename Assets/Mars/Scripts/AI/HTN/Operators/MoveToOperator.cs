//using FluidHTN;
using FluidHTN.Operators;
using UnityEngine;
using PLu.Mars.AI.HTN.Kernel;

namespace PLu.Mars.AI.HTN.Operators
{
    public class MoveToOperator : IOperator
    {
        public FluidHTN.TaskStatus StartNavigation(FluidHTN.IContext context)
        { 
            if (context is Context ctx)
            {
                ctx.NavAgent.isStopped = false;
                if (ctx.NavAgent.SetDestination(ctx.CurrentTargetPosition))
                {
                    return FluidHTN.TaskStatus.Continue;
                }

                return FluidHTN.TaskStatus.Failure;
            }  
            
            return FluidHTN.TaskStatus.Failure;
        }

        public FluidHTN.TaskStatus UpdateNavigation(FluidHTN.IContext context)
        {
            if (context is Context ctx)
            {
                if (ctx.NavAgent.remainingDistance <= ctx.NavAgent.stoppingDistance)
                {
                    ctx.NavAgent.isStopped = true;
                    return FluidHTN.TaskStatus.Success;
                }

                return FluidHTN.TaskStatus.Continue;
            }

            return FluidHTN. TaskStatus.Failure;
        }

        public FluidHTN.TaskStatus Update(FluidHTN.IContext context)
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

            return FluidHTN.TaskStatus.Failure;
        }
        public void Stop(FluidHTN.IContext context)
        {
            if (context is Context ctx)
            {
                ctx.NavAgent.isStopped = true;
            }
        }

        public void Aborted(FluidHTN.IContext context)
        {
            if (context is Context ctx)
            {
                ctx.NavAgent.isStopped = true;
            }
        }
    }
}