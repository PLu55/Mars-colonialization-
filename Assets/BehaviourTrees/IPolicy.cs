namespace PLu.BehaviourTrees
{
    public interface IPolicy {
        bool ShouldReturn(Node.Status status);
    }
}