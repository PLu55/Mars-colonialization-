using System;
using UnityEngine;
using FluidHTN;

namespace PLu.Mars.AI.HTN.Effects
{

    public class SetFlagWorldStateEffect : IEffect
    {
        public string Name { get; }
        public EffectType Type { get; }
        public AIWorldState State { get; }
        public int Index { get; }
        public byte Value { get; }

        public SetFlagWorldStateEffect(AIWorldState state, int index, bool value, EffectType type)
        {
            Name = $"SetFlagState({state})";
            Type = type;
            State = state;
            Index = index & 0x_FF;
            Value = (byte) (value ? 1 : 0);
        }

        public void Apply(IContext ctx)
        {
            if (ctx is Kernel.Context c)
            {
                byte v = c.GetState(State);
                c.SetState(State, (byte)((Value << Index) | v) , Type);
                if (ctx.LogDecomposition) ctx.Log(Name, $"SetFlagWorldStateEffect.Apply({State}:{Index}:{(Value)}\\{c.GetState(State)}:{Type})", ctx.CurrentDecompositionDepth+1, this);
                return;
            }

            throw new Exception("Unexpected context type!");
        }
    }
}