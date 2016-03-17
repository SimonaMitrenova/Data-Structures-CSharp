namespace RideTheHorse
{
    using System;
    using System.Collections.Generic;

    public class RideTheHorse
    {
        private static int[,] matrix;

        public static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int columns = int.Parse(Console.ReadLine());
            int startRow = int.Parse(Console.ReadLine());
            int startCol = int.Parse(Console.ReadLine());

            matrix = new int[rows, columns];
            var startCell = new Cell(startRow, startCol, 1);
            matrix[startRow, startCol] = startCell.Step;
            var queue = new Queue<Cell>();
            queue.Enqueue(startCell);
            while (queue.Count > 0)
            {
                var currentCell = queue.Dequeue();

                if (ShouldTraverse(currentCell.Row -1, currentCell.Col - 2))
                {
                    matrix[currentCell.Row - 1, currentCell.Col - 2] = currentCell.Step + 1;
                    queue.Enqueue(new Cell(currentCell.Row -1, currentCell.Col -2, currentCell.Step + 1));
                }

                if (ShouldTraverse(currentCell.Row - 2, currentCell.Col - 1))
                {
                    matrix[currentCell.Row - 2, currentCell.Col - 1] = currentCell.Step + 1;
                    queue.Enqueue(new Cell(currentCell.Row - 2, currentCell.Col - 1, currentCell.Step + 1));
                }

                if (ShouldTraverse(currentCell.Row - 2, currentCell.Col + 1))
                {
                    matrix[currentCell.Row - 2, currentCell.Col + 1] = currentCell.Step + 1;
                    queue.Enqueue(new Cell(currentCell.Row - 2, currentCell.Col + 1, currentCell.Step + 1));
                }

                if (ShouldTraverse(currentCell.Row - 1, currentCell.Col + 2))
                {
                    matrix[currentCell.Row - 1, currentCell.Col + 2] = currentCell.Step + 1;
                    queue.Enqueue(new Cell(currentCell.Row - 1, currentCell.Col + 2, currentCell.Step + 1));
                }

                if (ShouldTraverse(currentCell.Row + 1, currentCell.Col + 2))
                {
                    matrix[currentCell.Row + 1, currentCell.Col + 2] = currentCell.Step + 1;
                    queue.Enqueue(new Cell(currentCell.Row + 1, currentCell.Col + 2, currentCell.Step + 1));
                }

                if (ShouldTraverse(currentCell.Row + 2, currentCell.Col + 1))
                {
                    matrix[currentCell.Row + 2, currentCell.Col + 1] = currentCell.Step + 1;
                    queue.Enqueue(new Cell(currentCell.Row + 2, currentCell.Col + 1, currentCell.Step + 1));
                }

                if (ShouldTraverse(currentCell.Row + 2, currentCell.Col - 1))
                {
                    matrix[currentCell.Row + 2, currentCell.Col - 1] = currentCell.Step + 1;
                    queue.Enqueue(new Cell(currentCell.Row + 2, currentCell.Col - 1, currentCell.Step + 1));
                }

                if (ShouldTraverse(currentCell.Row + 1, currentCell.Col - 2))
                {
                    matrix[currentCell.Row + 1, currentCell.Col - 2] = currentCell.Step + 1;
                    queue.Enqueue(new Cell(currentCell.Row + 1, currentCell.Col - 2, currentCell.Step + 1));
                }
            }

            int colToPrint = columns/2;
            for (int row = 0; row < rows; row++)
            {
                Console.WriteLine(matrix[row, colToPrint]);
            }
        }

        private static bool ShouldTraverse(int row, int col)
        {
            return row >= 0 &&
                row < matrix.GetLength(0) &&
                col >= 0 && 
                col < matrix.GetLength(1) &&
                matrix[row, col] == 0;
        }
    }
}
