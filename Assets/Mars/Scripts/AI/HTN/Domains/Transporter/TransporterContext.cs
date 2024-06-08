using UnityEngine;
using UnityEngine.AI;
using FluidHTN;
using PLu.Mars.AI.HTN.Kernel;

namespace PLu.Mars.AI.HTN.Domains.Transporter
{
    
    public class TransporterContext : Context
    {
        public TransporterContext(IAIAgent agent) : base(agent)
        {
        }
 
        //public NavMeshAgent NavAgent { get; }
        public Transform PickupLocation { get; set; }
        public Transform DropoffLocation { get; set; }

        public void SetTransporterJob(Transform pickupLocation, Transform dropoffLocation)
        {
            PickupLocation = pickupLocation;
            DropoffLocation = dropoffLocation;
            SetState(TransporterWorldState.HasJob, true, EffectType.PlanAndExecute);
        }
        public bool HasState(TransporterWorldState state, bool value)
        {
            return HasState((int) state, (byte) (value ? 1 : 0));
        }

        public bool HasState(TransporterWorldState state, byte value)
        {
            return HasState((int)state, value);
        }

        public bool HasState(TransporterWorldState state)
        {
            return HasState((int) state, 1);
        }

        public void SetState(TransporterWorldState state, bool value, EffectType type)
        {
            UnityEngine.Debug.Log($"AIContext.SetState: {state} to {value}");
            SetState((int) state, (byte) (value ? 1 : 0), true, type);
        }

        public void SetState(TransporterWorldState state, byte value, EffectType type)
        {
            SetState((int)state, value, true, type);
        }
    }
}
