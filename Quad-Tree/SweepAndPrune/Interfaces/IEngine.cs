namespace SweepAndPrune.Interfaces
{
    public interface IEngine
    {
        ICommandExecutor Executor { get; }

        IReader Reader { get; }

        IWriter Writer { get; }

        void Run();
    }
}
