namespace SweepAndPrune.Interfaces
{
    using System.Collections.Generic;

    public interface IRepository
    {
        void Add(string name, int x1, int y1);

        IList<IGameObject> SweepAndPrune();

        void Move(string name, int newX, int newY);
    }
}
