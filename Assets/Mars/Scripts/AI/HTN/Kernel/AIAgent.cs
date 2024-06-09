using System;
using UnityEngine;
using FluidHTN;
using UnityEngine.AI;

namespace PLu.Mars.AI.HTN.Kernel
{
    // TODO: where do we initialze the WorldState?
    public abstract class AIAgent<T>: MonoBehaviour, IAIAgent where T : IContext
    {
        [Tooltip("The domain definition for this agent")]
        [SerializeField] private DomainDefinition<T> _domainDefinition;
        [Header("Debug")]
        [SerializeField] private bool _logDecomposition = false;
        public AgentFactory<T> AIAgentFactory { get; protected set;}
        public T Context { get; private set;}
        public Planner<T> Planner { get; private set;}
        FluidHTN.Domain<T> Domain { get; set;}
        public DomainDefinition<T> DomainDefinition => _domainDefinition;
         
        protected void Awake()
        {
            Debug.Assert(DomainDefinition != null, $"DomainDefinition is not set in {name}!");  
            if (DomainDefinition == null)
            {
                Debug.LogError($"Missing domain definition in {name}!");
                gameObject.SetActive(false);
                return;
            }
            Debug.Assert(AIAgentFactory != null, $"AIAgentFactory is not set in {name}!");
            Context = AIAgentFactory.CreateContext(this);
            Debug.Assert(Context != null, "Could not create context!");
            Planner = AIAgentFactory.CreatePlanner();
            Debug.Assert(Planner != null, "Could not create planner!");
            Domain = DomainDefinition.Create();
            Debug.Assert(Domain != null, "Could not create domain!");
            if (Context is Context ctx)
            {
                ctx.NavAgent = GetComponent<NavMeshAgent>();
                ctx.NavAgent.isStopped = true;
                Debug.Log($"Context.NavAgent: {ctx.NavAgent}");
            }

            Debug.Assert(Context != null, "Could not create context!");
            if (Planner == null || Domain == null || Context == null)
            {
                Debug.LogError($"Could not initialize agent {name}!");
                gameObject.SetActive(false);
            }
            Debug.Log($"Context.LogDecomposition: {Context.LogDecomposition}");
        }

        public virtual void Update()
        {
            Debug.Log("AIAgent.Update()");
            if (Context is Context ctx)
            {
                ctx.Update();
            }

            Planner.Tick(Domain, Context);

            if (Context.LogDecomposition)
            {
                Debug.Log("--- LogDecomposition");
                UpdateDecompositionLog();
            }
        }
        private void UpdateDecompositionLog()
        {
            while (Context.DecompositionLog?.Count > 0)
            {
                var entry = Context.DecompositionLog.Dequeue();
                var depth = FluidHTN.Debug.Debug.DepthToString(entry.Depth);
                Console.ForegroundColor = entry.Color;
                Console.WriteLine($"{depth}{entry.Name}: {entry.Description}");
                Debug.Log($"{depth}{entry.Name}: {entry.Description}");
            }
            Console.ResetColor();
        }

        private void OnDrawGizmos()
        {
            if (Context == null)
                return;

            //_senses?.DrawGizmos(_context);
            //_sensory?.DrawGizmos(_context);

    #if UNITY_EDITOR
            var task = Planner.GetCurrentTask();
            if (task != null)
            {
                //Handles.Label(_context.Head.transform.position + Vector3.up, task.Name);
            }
    #endif
        }
        
    }
}
