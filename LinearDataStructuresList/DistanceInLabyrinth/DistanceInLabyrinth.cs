namespace DistanceInLabyrinth
{
    using System;
    using System.Collections.Generic;

    public class DistanceInLabyrinth
    {
        private static readonly string[,] Labyrinth =
        {
            {"0", "0", "0", "x", "0", "x"},
            {"0", "x", "0", "x", "0", "x"},
            {"0", "*", "x", "0", "x", "0"},
            {"0", "x", "0", "0", "0", "0"},
            {"0", "0", "0", "x", "x", "0"},
            {"0", "0", "0", "x", "0", "x"},
        };
        
        public static void Main(string[] args)
        {
            var startCell = FindStartPosition();
            Queue<Cell> positions = new Queue<Cell>();
            positions.Enqueue(startCell);

            while (positions.Count > 0)
            {
                var currentCell = positions.Dequeue();

                if (ShouldBeTraversed(currentCell.Row - 1, currentCell.Col))
                {
                    var newCell = new Cell(currentCell.Row - 1, currentCell.Col, currentCell.Step + 1);
                    positions.Enqueue(newCell);
                    Labyrinth[currentCell.Row - 1, currentCell.Col] = (currentCell.Step + 1).ToString();
                }

                if (ShouldBeTraversed(currentCell.Row, currentCell.Col + 1))
                {
                    var newCell = new Cell(currentCell.Row, currentCell.Col + 1, currentCell.Step + 1);
                    positions.Enqueue(newCell);
                    Labyrinth[currentCell.Row, currentCell.Col + 1] = (currentCell.Step + 1).ToString();
                }

                if (ShouldBeTraversed(currentCell.Row + 1, currentCell.Col))
                {
                    var newCell = new Cell(currentCell.Row + 1, currentCell.Col, currentCell.Step + 1);
                    positions.Enqueue(newCell);
                    Labyrinth[currentCell.Row + 1, currentCell.Col] = (currentCell.Step + 1).ToString();
                }

                if (ShouldBeTraversed(currentCell.Row, currentCell.Col - 1))
                {
                    var newCell = new Cell(currentCell.Row, currentCell.Col - 1, currentCell.Step + 1);
                    positions.Enqueue(newCell);
                    Labyrinth[currentCell.Row, currentCell.Col - 1] = (currentCell.Step + 1).ToString();
                }
            }
           
            PrintLabyrinth();
        }

        private static bool ShouldBeTraversed(int row, int col)
        {
            if (row >= 0 && row < Labyrinth.GetLength(0) &&
                col >= 0 && col < Labyrinth.GetLength(1))
            {
                if (Labyrinth[row, col] == "0")
                {
                    return true;
                }
            }

            return false;
        }

        private static void PrintLabyrinth()
        {
            for (int row = 0; row < Labyrinth.GetLength(0); row++)
            {
                for (int col = 0; col < Labyrinth.GetLength(1); col++)
                {
                    Console.Write("{0, 3}", Labyrinth[row, col] == "0" ? "U" : Labyrinth[row, col]);
                }

                Console.WriteLine();
            }
        }

        private static Cell FindStartPosition()
        {
            for (int row = 0; row < Labyrinth.GetLength(0); row++)
            {
                for (int col = 0; col < Labyrinth.GetLength(1); col++)
                {
                    if (Labyrinth[row, col] == "*")
                    {
                        return new Cell(row, col, 0);
                    }
                }
            }

            return new Cell(0, 0, 0);
        }
    }
}
