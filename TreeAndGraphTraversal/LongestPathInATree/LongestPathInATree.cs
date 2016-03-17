namespace LongestPathInATree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LongestPathInATree
    {
        public static Dictionary<int, Tree> nodesByValue = new Dictionary<int, Tree>(); 
        public static int longestPath;

        public static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < edgesCount; i++)
            {
                int[] edge = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int parentValue = edge[0];
                var parentNode = GetNodeByValue(parentValue);
                int childValue = edge[1];
                var childNode = GetNodeByValue(childValue);
                childNode.Parent = parentNode;
                parentNode.Children.Add(childNode);
            }

            SetSumToTheRoot();
            FindLongestPath();

            Console.WriteLine(longestPath);
        }

        private static void FindLongestPath()
        {
            foreach (var tree in nodesByValue)
            {
                foreach (var other in nodesByValue)
                {
                    int currentPath = tree.Value.SumToTheRoot - other.Value.SumToTheRoot + other.Key;

                    if (currentPath > longestPath)
                    {
                        longestPath = currentPath;
                    }
                }
            }
        }

        private static void SetSumToTheRoot()
        {
            foreach (var tree in nodesByValue)
            {
                tree.Value.SumToTheRoot = SumToTheRoot(tree.Value);
            }
        }

        public static int SumToTheRoot(Tree node)
        {
            var currentNode = node;
            int sum = currentNode.Value;
            while (currentNode.Parent != null)
            {
                sum += currentNode.Parent.Value;
                currentNode = currentNode.Parent;
            }

            return sum;
        }

        public static Tree GetNodeByValue(int value)
        {
            if (!nodesByValue.ContainsKey(value))
            {
                nodesByValue[value] = new Tree(value);
            }

            return nodesByValue[value];
        }
    }
}
