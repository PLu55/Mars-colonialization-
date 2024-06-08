using System;
using UnityEngine;
using FluidHTN;
using FluidHTN.Conditions;

namespace PLu.Mars.AI.HTN
{
    public class HasFlagWorldStateCondition : ICondition
    {
        public string Name { get; }
        public AIWorldState State { get; }
        public int Index { get; }
        public byte Value { get; }

        public HasFlagWorldStateCondition(AIWorldState state, int index)
        {
            Name = $"HasFlagState({state})";
            State = state;
            Index = index & 0x_FF;
            Value = 1;
        }

        public HasFlagWorldStateCondition(AIWorldState state, int index, bool value)
        {
            Name = $"HasFlagState({state})";
            State = state;
            Index = index & 0x_FF;
            Value = (byte) (value ? 1 : 0);
        }

        public bool IsValid(IContext ctx)
        {
            if (ctx is Kernel.Context c)
            {   
                var result = (c.GetState(State) << Index & 1) == Value;
                if (ctx.LogDecomposition) ctx.Log(Name, $"HasFlagWorldStateCondition.IsValid({State}:{Index}:{Value}:{result})", ctx.CurrentDecompositionDepth+1, this);
                return result;
            }

            throw new Exception("Unexpected context type!");
        }
    }
}