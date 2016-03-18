namespace SweepAndPrune
{
    using Core;
    using UserInterface;

    public class SweepAndPrune
    {
        public static void Main(string[] args)
        {
            var repository = new GameObjectsRepository();
            var executor = new CommandExecutor(repository);
            var reader = new ConsoleReader();
            var writer = new ConsoleWriter();
            var engine = new Engine(executor, reader, writer);
            engine.Run();
        }
    }
}
