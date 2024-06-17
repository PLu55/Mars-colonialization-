using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLu.BehaviourTrees;
using System;

namespace PLu.Mars.JobSystem
{
    public enum Urgency
    {
        Low,
        Medium,
        High,
        Critical,
        Emergency
    }

    public enum JobStatus
    {
        Active,
        Completed,
        Assigned,
        Started,
        Paused,
        Cancelled,
        Failed,
        Aborted,
        Interrupted,
        Resumed,
        Restarted,
        Reassigned,
        Reactivated
    }

    //[CreateAssetMenu(fileName = "New Job", menuName = "Mars/Job")]

    public class JobCondition : Condition
    {
        public string Name => _name;
        private string _name;
        public JobCondition(string name, Func<bool> predicate) : base(predicate)
        {
            _name = name;
        }
    }
    public class Job : ScriptableObject
    {
        [SerializeField] private Urgency _urgency;
        public Urgency Urgency => _urgency;
        public List<JobCondition> PreConditions = new();
        public List<Action> Actions = new();

        private JobStatus _status;
        private JobSequence _jobSequence;

        public JobSequence GetJobSequence()
        {
            if (_jobSequence == null)
            {
                BuildJobSequence();
            }
            return _jobSequence;
        }   
        private void BuildJobSequence()
        {
            JobSequenceBuilder builder =  new JobSequenceBuilder(name, Urgency);
            foreach (var condition in PreConditions)
            {
                builder.WithCondition(condition);
            }
            foreach (var action in Actions)
            {
                builder.Action(action);
            }
            _jobSequence = builder.Build();
        }
    }
    public class JobSequenceBuilder
    {
        private JobSequence _jobSequence;
        public JobSequenceBuilder(string name, Urgency urgency)
        {
            _jobSequence = new JobSequence(name, (int) urgency);
        }

        public JobSequenceBuilder WithCondition(JobCondition condition)
        {
            Leaf leaf = new Leaf(condition.Name, condition);
            _jobSequence.AddChild(leaf);
            return this;
        }

        public JobSequenceBuilder Action(Action action)
        {
            Leaf leaf = new Leaf("Action", new ActionStrategy(action));
            _jobSequence.AddChild(leaf);
            return this;
        }
        public JobSequence Build()
        {
            return _jobSequence;
        }
    }
}
