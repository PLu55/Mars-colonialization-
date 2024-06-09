using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluidHTN;
using FluidHTN.Factory;
using FluidHTN.Debug;
using UnityEngine.AI;


namespace PLu.Mars.AI.HTN.Kernel
{
    public class Context : FluidHTN.Contexts.BaseContext, IContext
    {
        public Context(IAIAgent agent)
        {
            if (agent == null) throw new ArgumentNullException(nameof(agent));
            // TODO: Doesen't work, fix it!
            if (agent is AIAgent<IContext> a)
            {
                NavAgent = a.GetComponent<NavMeshAgent>();
                NavAgent.isStopped = true;
            }
            //UnityEngine.Debug.Assert(NavAgent != null, "Coulden't find NavAgent");
            UnityEngine.Debug.Log("Context.ctor");
            AIAgent = agent;
            LastTargetPosition = Vector3.zero;
            CurrentTargetPosition = Vector3.zero;
            base.Init();
        }
        
        //----------------------------------------------------------------------------------------------------
        // Implements BaseContext

        public override IFactory Factory { get; set; } = new DefaultFactory();
        public override List<string> MTRDebug { get; set; }
        public override List<string> LastMTRDebug { get; set; }
        public override bool DebugMTR { get; }
        public override Queue<IBaseDecompositionLogEntry> DecompositionLog { get; set; }
        public override bool LogDecomposition { get; } = true;
        public override byte[] WorldState { get; } = new byte[Enum.GetValues(typeof(AIWorldState)).Length];

        //----------------------------------------------------------------------------------------------------
        // Implements IContext (PLu.Mars.AI.HTN.Kernel.IContext)

        public IAIAgent AIAgent { get; set;}

        public virtual void Update()
        {
            //UnityEngine.Debug.Log("Context.Update()");
            
            CurrentTime = Time.time;
            DeltaTime = Time.deltaTime;
        }

        //----------------------------------------------------------------------------------------------------
        // Movable agents
        public Vector3 CurrentTargetPosition { get; set; }
        public Vector3 LastTargetPosition { get; set; }
        public NavMeshAgent NavAgent { get; set; }

        //----------------------------------------------------------------------------------------------------
        // Generic Timer

        public float CurrentTime { get; set; }
        public float DeltaTime { get; set; }
        public float GenericTimer { get; set; }

        //----------------------------------------------------------------------------------------------------
        // Custom state handling methods
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


