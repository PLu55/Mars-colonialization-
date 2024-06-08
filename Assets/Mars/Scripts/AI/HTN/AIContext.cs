using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace PLu.Mars.AI.HTN
{
    public partial class ContextX
    {
        //public AIAgent1<AIContext> Agent { get; }
        //public AISenses Senses { get; }
        public Transform Head { get; }
        public NavMeshAgent NavAgent { get; }
        //public Mobile Mobile { get; }

        //public Vector3 Position => Agent.transform.position;
        //public Vector3 Forward => Agent.transform.forward;

        public float CurrentTime { get; set; }
        public float DeltaTime { get; set; }
        public float GenericTimer { get; set; }
    
        // public List<Resource> KnownResources = new List<Resource>();
        // public Resource CurrentResource { get; set; }
        // public List<Workshop> KnownWorkshops = new List<Workshop>();
        // public Workshop CurrentWorkshop { get; set; }
        // public List<Recipe> KnownRecipes = new List<Recipe>();
        // public Recipe CurrentRecipe { get; set; }

        
        //public Vector3 CurrentTargetPosition { get; set; }
        public GameObject CurrentTarget { get; set; }
        public Vector3 LastTargetPosition { get; set; }
        public bool CanSense { get; set; }
        /*
        public AIContext(AIAgent1<AIContext> agent)
        {
            Agent = agent;
            Senses = agent.Senses;
            Head = agent.Head;
            //Animator = animator;
            NavAgent = agent.GetComponent<NavMeshAgent>();
            NavAgent.isStopped = true;
            //Mobile = agent.GetComponent<Mobile>();
            //Mobile.Init(this);
            LastTargetPosition = Vector3.zero;
            CanSense = true;

            base.Init();
        }
        public void Update()
        {
            CurrentTime = Time.time;
            DeltaTime = Time.deltaTime;
        }
        */
    }
}