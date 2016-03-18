namespace SweepAndPrune.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using Models;
    using Utils;

    public class GameObjectsRepository : IRepository
    {
        private IList<IGameObject> gameObjects;

        public GameObjectsRepository()
        {
            this.gameObjects = new List<IGameObject>();
        }

        public void Add(string name, int x1, int y1)
        {
            var gameObject = new GameObject(name, x1, y1);
            this.gameObjects.Add(gameObject);
        }

        public IList<IGameObject> SweepAndPrune()
        {
            RepositoryUtils.InsertionSort(this.gameObjects);
            var resultObjects = new List<IGameObject>();
            for (int i = 0; i < this.gameObjects.Count; i++)
            {
                var currentObj = this.gameObjects[i];
                for (int j = i + 1; j < this.gameObjects.Count; j++)
                {
                    var candidateColisionObj = this.gameObjects[j];
                    if (currentObj.X2 < candidateColisionObj.X1)
                    {
                        break;
                    }
                    if (currentObj.Intersects(candidateColisionObj))
                    {
                        resultObjects.Add(currentObj);
                        resultObjects.Add(candidateColisionObj);
                    }
                }
            }

            return resultObjects;
        }

        public void Move(string name, int newX, int newY)
        {
            var gameObject = this.gameObjects.FirstOrDefault(o => o.Name == name);
            if (gameObject == null)
            {
                return;
            }

            gameObject.X1 = newX;
            gameObject.Y1 = newY;
        }
    }
}
