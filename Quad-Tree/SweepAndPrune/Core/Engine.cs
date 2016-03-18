namespace SweepAndPrune.Core
{
    using System.Collections.Generic;
    using Interfaces;

    public class Engine : IEngine
    {
        private int tickCounter = 0;

        public Engine(ICommandExecutor executor, IReader reader, IWriter writer)
        {
            this.Executor = executor;
            this.Reader = reader;
            this.Writer = writer;
        }

        public ICommandExecutor Executor { get; private set; }

        public IReader Reader { get; private set; }

        public IWriter Writer { get; private set; }

        public void Run()
        {
            string[] command = this.Reader.ReadLine().Split();
            while (command[0] == "add")
            {
                this.Executor.ExecuteCommand(command);
                command = this.Reader.ReadLine().Split();
            }

            command = this.Reader.ReadLine().Split();
            while (command[0] != "end")
            {
                var result = this.Executor.ExecuteCommand(command);
                this.tickCounter++;
                this.PrintResult(result);
                command = this.Reader.ReadLine().Split();
            }
        }

        private void PrintResult(IList<IGameObject> objects)
        {
            for (int i = 0; i < objects.Count; i+= 2)
            {
                this.Writer.WriteLine("({0}) {1} collides with {2}", this.tickCounter, objects[i].Name, objects[i + 1].Name);
            }
        }
    }
}
