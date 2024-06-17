namespace PLu.BehaviourTrees
{
    // UntilSuccess
    // Repeat
    public class Fail : Node {
        public Fail(int priority = 0) : base("Fail", priority) { }
        
        public override Status Process() { 
            return Status.Failure;
        }
    }
}