namespace SweepAndPrune.Interfaces
{
    using System.Collections.Generic;

    public interface ICommandExecutor
    {
        IRepository Repository { get; }

        IList<IGameObject> ExecuteCommand(string[] command);
    }
}
