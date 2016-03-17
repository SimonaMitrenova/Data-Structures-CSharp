using System;
using System.Collections.Generic;
using System.Text;
using Escape_from_Labyrinth;

public class EscapeFromLabyrinth
{
    private const char VisitedCell = 's';
    private static int width;
    private static int height;
    private static char[,] labyrinth;

    public static void Main()
    {
        width = int.Parse(Console.ReadLine());
        height = int.Parse(Console.ReadLine());

        labyrinth = new char[height, width];
        for (int x = 0; x < height; x++)
        {
            string input = Console.ReadLine();
            for (int y = 0; y < width; y++)
            {
                labyrinth[x, y] = input[y];
            }
        }

        string shortestPath = FindShortestPathToExit();
        if (shortestPath == null)
        {
            Console.WriteLine("No exit!");
        }
        else if (shortestPath == string.Empty)
        {
            Console.WriteLine("Start is at the exit.");
        }
        else
        {
            Console.WriteLine("Shortest exit: {0}", shortestPath);
        }
    }

    public static string FindShortestPathToExit()
    {
        var queue = new Queue<Point>();
        var startPosition = FindStartPosition();
        if (startPosition == null)
        {
            return null;
        }

        queue.Enqueue(startPosition);
        while (queue.Count > 0)
        {
            var currentCell = queue.Dequeue();
            if (IsExit(currentCell))
            {
                return TrasePathBack(currentCell);
            }

            TryDirection(queue, currentCell, "U", -1, 0);
            TryDirection(queue, currentCell, "R", 0, 1);
            TryDirection(queue, currentCell, "D", 1, 0);
            TryDirection(queue, currentCell, "L", 0, -1);
        }

        return null;
    }

    private static void TryDirection(Queue<Point> queue, Point currentCell, string direction, int deltaX, int deltaY)
    {
        int newX = currentCell.X + deltaX;
        int newY = currentCell.Y + deltaY;

        if (newX >= 0 && newX < height && newY >= 0 && newY < width && labyrinth[newX, newY] == '-')
        {
            labyrinth[newX, newY] = VisitedCell;
            var nextCell = new Point(newX, newY, direction, currentCell);
            queue.Enqueue(nextCell);
        }
    }

    private static string TrasePathBack(Point currentCell)
    {
        var path = new StringBuilder();
        while (currentCell.PreviousPoint != null)
        {
            path.Append(currentCell.Direction);
            currentCell = currentCell.PreviousPoint;
        }
        var pathReversed = new StringBuilder(path.Length);
        for (int i = path.Length - 1; i >= 0; i--)
        {
            pathReversed.Append(path[i]);
        }

        return pathReversed.ToString();
    }

    public static Point FindStartPosition()
    {
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                if (labyrinth[x, y] == VisitedCell)
                {
                    var startPoint = new Point(x, y, null, null);
                    return startPoint;
                }
            }
        }

        return null;
    }

    public static bool IsExit(Point currentCell)
    {
        bool IsOnBorderX = currentCell.X == 0 || currentCell.X == height - 1;
        bool IsOnBorderY = currentCell.Y == 0 || currentCell.Y == width - 1;
        return IsOnBorderY || IsOnBorderX;
    }
}
