using System;
using FluidHTN;
using FluidHTN.Conditions;

namespace PLu.Mars.AI.HTN
{
    public class HasWorldStateCondition : ICondition
    {
        public string Name { get; }
        public AIWorldState State { get; }
        public byte Value { get; }

        public HasWorldStateCondition(AIWorldState state)
        {
            Name = $"HasState({state})";
            State = state;
            Value = 1;
        }

        public HasWorldStateCondition(AIWorldState state, byte value)
        {
            Name = $"HasState({state})";
            State = state;
            Value = value;
        }

        public bool IsValid(IContext ctx)
        {
            if (ctx is Kernel.Context c)
            {
                var result = c.HasState(State, Value);
                if (ctx.LogDecomposition) ctx.Log(Name, $"HasWorldStateCondition.IsValid({State}:{c.GetState(State)}\\{Value}:{result})", ctx.CurrentDecompositionDepth+1, this);
                return result;
            }

            throw new Exception("Unexpected context type!");
        }
    }
}