namespace PLu.UtilityAI
{
    public interface IAction
    {
        void Execute();
        float Consider(IContext context);
    }
}
