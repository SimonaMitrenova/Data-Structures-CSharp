namespace SweepAndPrune.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    public class CommandExecutor : ICommandExecutor
    {
        public CommandExecutor(IRepository repository)
        {
            this.Repository = repository;
        }

        public IRepository Repository { get; private set; }

        public IList<IGameObject> ExecuteCommand(string[] command)
        {
            var results = new List<IGameObject>();

            switch (command[0])
            {
                case "add":
                    string name = command[1];
                    int x1 = int.Parse(command[2]);
                    int y1 = int.Parse(command[3]);
                    this.Repository.Add(name, x1, y1);
                    break;

                case "start":
                    break;

                case "tick":
                    results = this.Repository.SweepAndPrune().ToList();
                    break;

                case "move":
                    string nameMove = command[1];
                    int newX = int.Parse(command[2]);
                    int newY = int.Parse(command[3]);
                    this.Repository.Move(nameMove, newX, newY);
                    results = this.Repository.SweepAndPrune().ToList();
                    break;
            }

            return results;
        }
    }
}
