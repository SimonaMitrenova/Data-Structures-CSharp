namespace FindTheRootBoolArray
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FindTheRootBoolArray
    {
        public static void Main(string[] args)
        {
            int numberOfNodes = int.Parse(Console.ReadLine());
            int numberOfEdges = int.Parse(Console.ReadLine());

            bool[] hasParent = new bool[numberOfNodes];

            for (int i = 0; i < numberOfEdges; i++)
            {
                int[] edge = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int childValue = edge[1];
                hasParent[childValue] = true;
            }

            List<int> rootNodes = hasParent
                .Select((node, index) => new
            {
                Index = index,
                Value = node
            })
            .Where(n => !n.Value)
            .Select(n => n.Index)
            .ToList();

            if (!rootNodes.Any())
            {
                Console.WriteLine("No root!");
            }
            else if (rootNodes.Count() == 1)
            {
                Console.WriteLine(rootNodes.First());
            }
            else
            {
                Console.WriteLine("Multiple root nodes!");
            }
        }
    }
}
