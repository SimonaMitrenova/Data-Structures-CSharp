namespace SweepAndPrune.UserInterface
{
    using System;
    using Interfaces;

    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string format, params object[] arguments)
        {
            Console.WriteLine(format, arguments);
        }
    }
}
