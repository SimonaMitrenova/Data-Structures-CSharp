namespace SweepAndPrune.Interfaces
{
    public interface IGameObject
    {
        string Name { get; }

        int X1 { get; set; }

        int X2 { get; }

        int Y1 { get; set; }

        int Y2 { get; }

        bool Intersects(IGameObject other);
    }
}
