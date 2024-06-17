using System.Collections.Generic;
using System.Linq;
using PLu.BehaviourTrees;

namespace PLu.Mars.JobSystem
{
    public class JobSequence : Sequence 
    {
        public JobSequence(string name, int priority = 0) : base(name, priority) { }
        
        public override Status Process() 
        {
            foreach (var child in children) 
            {
                switch (child.Process()) 
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Failure:
                        return Status.Failure;
                    default:
                        continue;
                }
            }

            return Status.Success;
        }
    }
    public class JobSelector : Selector 
    {
        List<Node> sortedChildren;
        List<Node> SortedChildren => sortedChildren ??= SortChildren();
        
        protected virtual List<Node> SortChildren() => children.OrderByDescending(child => child.priority).ToList();
        
        public JobSelector(string name, int priority = 0) : base(name, priority) { }
        
        public override void Reset() 
        {
            sortedChildren = null;
        }

        public void RemoveChild(Node child) 
        {
            children.Remove(child);
            sortedChildren.Remove(child);
        }
        public override Status Process() 
        {
            foreach (var child in SortedChildren) 
            {
                switch (child.Process()) 
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Success:
                        RemoveChild(child);
                        Reset();
                        return Status.Success;
                    default:
                        RemoveChild(child);
                        continue;
                }
            }

            Reset();
            return Status.Failure;
        }
    }
}