using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using FluidHTN;
using FluidHTN.Contexts;
using FluidHTN.Factory;

namespace PLu.Mars.AI.HTN
{
    public partial class ContextX : BaseContext
    {

        public override IFactory Factory { get; set; } = new DefaultFactory();
        public override List<string> MTRDebug { get; set; }
        public override List<string> LastMTRDebug { get; set; }
        public override bool DebugMTR { get; } = false;
        public override Queue<FluidHTN.Debug.IBaseDecompositionLogEntry> DecompositionLog { get; set; }
        public override bool LogDecomposition { get; set; } = true;

        public override byte[] WorldState { get; } = new byte[Enum.GetValues(typeof(AIWorldState)).Length];

        public ContextX() {}
        // public AIContext(AIAgent1<IAIContext> agent)
        // {
        //     Agent = agent;
        //     LastTargetPosition = Vector3.zero;
        //     CanSense = true;
        // }

        public bool HasState(AIWorldState state, bool value)
        {
            return HasState((int) state, (byte) (value ? 1 : 0));
        }

        public bool HasState(AIWorldState state, byte value)
        {
            return HasState((int)state, value);
        }

        public bool HasState(AIWorldState state)
        {
            return HasState((int) state, 1);
        }

        public void SetState(AIWorldState state, bool value, EffectType type)
        {
            UnityEngine.Debug.Log($"AIContext.SetState: {state} to {value}");
            SetState((int) state, (byte) (value ? 1 : 0), true, type);
        }

        public void SetState(AIWorldState state, byte value, EffectType type)
        {
            SetState((int)state, value, true, type);
        }

        public byte GetState(AIWorldState state)
        {
            return GetState((int) state);
        }
    }
}