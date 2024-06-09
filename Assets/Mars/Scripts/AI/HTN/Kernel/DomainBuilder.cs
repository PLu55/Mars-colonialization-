using UnityEngine;
using FluidHTN;
using FluidHTN.Factory;
using FluidHTN.PrimitiveTasks;
using PLu.Mars.AI.HTN.Operators;
using PLu.Mars.AI.HTN.Effects;

namespace PLu.Mars.AI.HTN.Kernel
{
    public class DomainBuilder<T> : BaseDomainBuilder<DomainBuilder<T>,T> 
            where T : IContext
    {
        public DomainBuilder(string domainName) : base(domainName, new DefaultFactory())
        {
        }

        public DomainBuilder<T> HasState(AIWorldState state)
        {
            var condition = new HasWorldStateCondition(state);
            Pointer.AddCondition(condition);
            return this;
        }

        public DomainBuilder<T> HasState(AIWorldState state, byte value)
        {
            var condition = new HasWorldStateCondition(state, value);
            Pointer.AddCondition(condition);
            return this;
        }
        public DomainBuilder<T> HasState(AIWorldState state, bool value)
        {
            var condition = new HasWorldStateCondition(state, (byte)(value ? 1 : 0));
            Pointer.AddCondition(condition);
            return this;
        }
        public DomainBuilder<T> HasStateGreaterThan(AIWorldState state, byte value)
        {
            var condition = new HasWorldStateGreaterThanCondition(state, value);
            Pointer.AddCondition(condition);
            return this;
        }
        public DomainBuilder<T> HasFlagState(AIWorldState state, int index)
        {
            var condition = new HasFlagWorldStateCondition(state, index);
            Pointer.AddCondition(condition);
            return this;
        }
        public DomainBuilder<T> HasFlagState(AIWorldState state, int index, bool value)
        {
            var condition = new HasFlagWorldStateCondition(state, index, value);
            Pointer.AddCondition(condition);
            return this;
        }
        public DomainBuilder<T> SetState(AIWorldState state, EffectType type)
        {
            if (Pointer is IPrimitiveTask task)
            {
                var effect = new SetWorldStateEffect(state, type);
                task.AddEffect(effect);
            }
            return this;
        }

        public DomainBuilder<T> SetState(AIWorldState state, bool value, EffectType type)
        {
            Debug.Log($"Setting state {state} to {value}");
            if (Pointer is IPrimitiveTask task)
            {
                var effect = new SetWorldStateEffect(state, value, type);
                task.AddEffect(effect);
            }
            return this;
        }

        public DomainBuilder<T> SetState(AIWorldState state, byte value, EffectType type)
        {
            if (Pointer is IPrimitiveTask task)
            {
                var effect = new SetWorldStateEffect(state, value, type);
                task.AddEffect(effect);
            }
            return this;
        }
        public DomainBuilder<T> SetFlagState(AIWorldState state, int index, bool value, EffectType type)
        {
            if (Pointer is IPrimitiveTask task)
            {
                var effect = new SetFlagWorldStateEffect(state, index, value, type);
                task.AddEffect(effect);
            }
            return this;
        }
        public DomainBuilder<T> IncrementState(AIWorldState state, EffectType type)
        {
            if (Pointer is IPrimitiveTask task)
            {
                var effect = new IncrementWorldStateEffect(state, type);
                task.AddEffect(effect);
            }
            return this;
        }

        public DomainBuilder<T> IncrementState(AIWorldState state, byte value, EffectType type)
        {
            if (Pointer is IPrimitiveTask task)
            {
                var effect = new IncrementWorldStateEffect(state, value, type);
                task.AddEffect(effect);
            }
            return this;
        }

        public DomainBuilder<T> Wait(float waitTime)
        {
            Action("Wait");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new WaitOperator(waitTime));
            }
            End();
            return this;
        }
        public DomainBuilder<T> MoveTo(AIDestinationTarget destination)
        {
            Action("MoveTo");
            if (Pointer is IPrimitiveTask task)
            {
                task.SetOperator(new MoveToOperator());
            }
            End();
            return this;
        }
        // public AIDomainBuilder ReceivedDamage()
        // {
        //     Action("Received damage");
        //     HasState( U.HasReceivedDamage);
        //     if (Pointer is IPrimitiveTask task)
        //     {
        //         task.SetOperator(new TakeDamageOperator());
        //     }
        //     SetState( U.HasReceivedDamage, false, EffectType.PlanAndExecute);
        //     End();
        //     return this;
        // }

        // public AIDomainBuilder FindEnemy()
        // {
        //     Action("Find enemy");
        //     if (Pointer is IPrimitiveTask task)
        //     {
        //         task.SetOperator(new FindEnemyOperator());
        //     }
        //     End();
        //     return this;
        // }

        // public AIDomainBuilder AttackEnemy()
        // {
        //     Action("Attack enemy");
        //     HasState( U.HasEnemyInMeleeRange);
        //     if (Pointer is IPrimitiveTask task)
        //     {
        //         task.SetOperator(new AttackOperator());
        //     }
        //     IncrementState( U.Stamina, EffectType.PlanAndExecute);
        //     End();
        //     return this;
        //}

//         public AIDomainBuilder MoveToEnemy()
//         {
//             Action("Move to enemy");
//             if (Pointer is IPrimitiveTask task)
//             {
//                 task.SetOperator(new MoveToOperator(AIDestinationTarget.Enemy));
//             }
//             SetState( U.HasEnemyInMeleeRange, EffectType.PlanAndExecute);
//             End();
//             return this;
//         }
//         public AIDomainBuilder FindResource(AIResourceType resourceType)
//         {
//             Action("Find resource");
//             if (Pointer is IPrimitiveTask task)
//             {
//                 task.SetOperator(new FindResourceOperator(resourceType));
//             }
//             End();
//             return this;
//         }
//         public AIDomainBuilder MoveToResource()
//         {
//             Action("Move to resource");
//             if (Pointer is IPrimitiveTask task)
//             {
//                 task.SetOperator(new MoveToOperator(AIDestinationTarget.Resource));
//             }
//             End();
//             return this;
//         }
//         public AIDomainBuilder Patrol()
//         {
//             Action("Patrol");
//             if (Pointer is IPrimitiveTask task)
//             {
//                 task.SetOperator(new PatrolOperator());
//             }
//             End();
//             return this;
//         }
//         public AIDomainBuilder ChaseTarget()
//         {
//             Action("Chase target");
//             if (Pointer is IPrimitiveTask task)
//             {
//                 task.SetOperator(new ChaseTargetOperator());
//             }
//             End();
//             return this;
//         }

//         public  AIDomainBuilder SearchTarget()
//         {
//             Action("Search for enemy");
//             if (Pointer is IPrimitiveTask task)
//             {
//                 task.SetOperator(new SearchTargetOperator());
//                 SetState( U.HasEnemy, false, EffectType.PlanAndExecute);
//             }
//             End();
//             return this;
//         } 
//         public AIDomainBuilder GatherResource(AIResourceType resourceType, float amount, int flagIdx)
//         {
//             Action("Gather resource");
//             if (Pointer is IPrimitiveTask task)
//             {
//                 task.SetOperator(new GatherResourceOperator(resourceType, amount));
//             }
//             SetFlagState( U.HasIngredient, flagIdx, true, EffectType.PlanAndExecute);
//             End();
//             return this;
//         }
//         public AIDomainBuilder FindWorkshop(WorkshopType workshopType)
//         {
//             Action("Find Workshop");
//             if (Pointer is IPrimitiveTask task)
//             {
//                 task.SetOperator(new FindWorkshopOperator(workshopType));
//             }
//             End();
//             return this;
//         }
//         public AIDomainBuilder MoveToWorkshop()
//         {
//             Action("Move to resource");
//             if (Pointer is IPrimitiveTask task)
//             {
//                 task.SetOperator(new MoveToOperator(AIDestinationTarget.Workshop));
//             }
//             End();
//             return this;
//         }
//         public AIDomainBuilder MakeRecipe(AIResourceType resourceType, float amount = 1)
//         {
//             Action("Make Recipe");
//             if (Pointer is IPrimitiveTask task)
//             {
//                 task.SetOperator(new MakeRecipeOperator(resourceType, amount));
//             }
//             End();
//             return this;
//         }

// /*

//         public AIDomainBuilder FindBridge()
//         {
//             Action("Find bridge");
//             if (Pointer is IPrimitiveTask task)
//             {
//                 task.SetOperator(new FindBridgeOperator());
//             }
//             End();
//             return this;
//         }

//         public AIDomainBuilder MoveToBridge()
//         {
//             Action("Move to bridge");
//             if (Pointer is IPrimitiveTask task)
//             {
//                 task.SetOperator(new MoveToOperator(AIDestinationTarget.Bridge));
//             }
//             End();
//             return this;
//         }
//
//         public AIDomainBuilder BeTired(float restTime)
//         {
//             Action("Be Tired");
//             HasStateGreaterThan( U.Stamina, 2);
//             if (Pointer is IPrimitiveTask task)
//             {
//                 task.SetOperator(new WaitOperator(restTime));
//             }
//             SetState( U.Stamina, 0, EffectType.PlanAndExecute);
//             End();
//             return this;
//         }
    }
}