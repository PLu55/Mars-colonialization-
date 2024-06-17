namespace PLu.BehaviourTrees
{
    public static class Policies {
        public static readonly IPolicy RunForever = new RunForeverPolicy();
        public static readonly IPolicy RunUntilSuccess = new RunUntilSuccessPolicy();
        public static readonly IPolicy RunUntilFailure = new RunUntilFailurePolicy();
        public static readonly IPolicy RunUntilSuccessOrFailure = new RunUntilSuccessOrFailurePolicy();
        class RunForeverPolicy : IPolicy {
            public bool ShouldReturn(Node.Status status) => false;
        }
        
        class RunUntilSuccessPolicy : IPolicy {
            public bool ShouldReturn(Node.Status status) => status == Node.Status.Success;
        }
        
        class RunUntilFailurePolicy : IPolicy {
            public bool ShouldReturn(Node.Status status) => status == Node.Status.Failure;
        }
        class RunUntilSuccessOrFailurePolicy : IPolicy {
            public bool ShouldReturn(Node.Status status) => status == Node.Status.Failure || status == Node.Status.Success;
        }
   }
}