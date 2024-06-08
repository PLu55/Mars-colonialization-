using System;
using FluidHTN;
using FluidHTN.Conditions;

namespace PLu.Mars.AI.HTN
{
    public class HasWorldStateGreaterThanCondition : ICondition
    {
        public string Name { get; }
        public AIWorldState State { get; }
        public byte Value { get; }

        public HasWorldStateGreaterThanCondition(AIWorldState state, byte value)
        {
            Name = $"HasStateGreaterThan({state})";
            State = state;
            Value = value;
        }

        public bool IsValid(IContext ctx)
        {
            if (ctx is Kernel.Context c)
            {
                var currentValue = c.GetState(State);
                var result = currentValue > Value;
                if (ctx.LogDecomposition) ctx.Log(Name, $"HasWorldStateGreaterThanCondition.IsValid({State}:{currentValue} > {Value} = {result})", ctx.CurrentDecompositionDepth+1, this);
                return result;
            }

            throw new Exception("Unexpected context type!");
        }
    }
}