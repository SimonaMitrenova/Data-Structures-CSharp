namespace SweepAndPrune.Utils
{
    using System.Collections.Generic;
    using Interfaces;

    public static class RepositoryUtils
    {
        public static void InsertionSort(IList<IGameObject> objects)
        {
            for (int i = 0; i < objects.Count - 1; i++)
            {
                int j = i + 1;
                while (j > 0)
                {
                    if (objects[i].X1 > objects[j].X1)
                    {
                        var temp = objects[i];
                        objects[i] = objects[j];
                        objects[j] = temp;
                    }

                    j--;
                }
            }
        }
    }
}
