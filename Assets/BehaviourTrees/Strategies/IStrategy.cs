namespace PLu.BehaviourTrees
{
    public interface IStrategy {
        Node.Status Process();

        void Reset() {
            // Noop
        }
    }
}
