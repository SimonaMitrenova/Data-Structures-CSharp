namespace SweepAndPrune.Interfaces
{
    public interface IWriter
    {
        void WriteLine(string format, params object[] arguments);
    }
}
