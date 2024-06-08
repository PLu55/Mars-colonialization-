using UnityEngine;
using PLu.Mars.AI.HTN.Kernel;

namespace PLu.Mars.AI.HTN.Domains.Transporter
{
    public class TransporterAgentFactory : AgentFactory<TransporterContext>
    {
        public override TransporterContext CreateContext(AIAgent<TransporterContext> agent)
        {
            TransporterContext context = new TransporterContext(agent);
            Debug.Log($"TransporterAgentFactory.CreateContext() => {context}");
            Debug.Log($"TransporterAgentFactory.CreateContext() => {context} {context.GetType().Name}");
            return context;
        }
        public override Planner<TransporterContext> CreatePlanner()
        {
            Planner<TransporterContext> planner = new Planner<TransporterContext>();
            Debug.Log($"TransporterAgentFactory.CreatePlanner() => {planner}");
            return planner;
        }
    }

    public class TransporterAgent : AIAgent<TransporterContext>
    {
        public TransporterAgent()
        {
            Debug.Log("TransporterAgent()");
        }
        new void Awake()
        {
            Debug.Log("TransporterContext.Awake()");
            AIAgentFactory = new TransporterAgentFactory();
            base.Awake();
        }
    }
}
