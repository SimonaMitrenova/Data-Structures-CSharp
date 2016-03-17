using System;
using System.Collections.Generic;
using System.Linq;

public class GraphConnectedComponents
{
    public static List<int>[] graph;

    public static bool[] visited;

    public static void Main()
    {
        //visited = new bool[graph.Length];
        //DFS(0);
        //Console.WriteLine();

        graph = ReadGraph();
        FindGraphConnectedComponents();
    }

    public static List<int>[] ReadGraph()
    {
        int nodeCount = int.Parse(Console.ReadLine());
        List<int>[] graph = new List<int>[nodeCount];
        for (int i = 0; i < nodeCount; i++)
        {
            var newList = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            graph[i] = newList;
        }

        return graph;
    }

    public static void DFS(int node)
    {
        if (!visited[node])
        {
            visited[node] = true;
            foreach (var childNode in graph[node])
            {
                DFS(childNode);
            }

            Console.Write(" {0}", node);
        }

    }

    public static void FindGraphConnectedComponents()
    {
        visited = new bool[graph.Length];
        for (int strtNode = 0; strtNode < graph.Length; strtNode++)
        {
            if (!visited[strtNode])
            {
                Console.Write("Connected component:");
                DFS(strtNode);
                Console.WriteLine();
            }
        }
    }
}
