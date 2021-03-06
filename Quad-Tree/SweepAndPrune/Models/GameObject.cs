﻿namespace SweepAndPrune.Models
{
    using Interfaces;

    public class GameObject : IGameObject
    {
        private const int Width = 10;
        private const int Height = 10;

        public GameObject(string name, int x1, int y1)
        {
            this.Name = name;
            this.X1 = x1;
            this.Y1 = y1;
        }

        public string Name { get; set; }

        public int X1 { get; set; }

        public int X2
        {
            get
            {
                return this.X1 + Width;
            }
        }

        public int Y1 { get; set; }

        public int Y2
        {
            get
            {
                return this.Y1 + Height;
            }
        }

        public bool Intersects(IGameObject other)
        {
            return this.X1 <= other.X2 &&
                    other.X1 <= this.X2 &&
                    this.Y1 <= other.Y2 &&
                    other.Y1 <= this.Y2;
        }
    }
}
