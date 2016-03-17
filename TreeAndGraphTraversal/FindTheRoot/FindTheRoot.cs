namespace FindTheRoot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FindTheRoot
    {
        public static Dictionary<int, Node<int>> nodeByValue = new Dictionary<int, Node<int>>();

        public static void Main(string[] args)
        {
            int numberOfNodes = int.Parse(Console.ReadLine());
            int numberOfEdges = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfEdges; i++)
            {
                int[] edge = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int parentValue = edge[0];
                Node<int> parentNode = GetNodeByValue(parentValue);
                int childValue = edge[1];
                Node<int> childNode = GetNodeByValue(childValue);
                parentNode.Children.Add(childNode);
                childNode.Parent = parentNode;
            }

            var rootNodes = FinfRoots();
            if (!rootNodes.Any())
            {
                Console.WriteLine("No root!");
            }
            else if (rootNodes.Count() == 1)
            {
                Console.WriteLine(rootNodes.FirstOrDefault().Value);
            }
            else
            {
                Console.WriteLine("Multiple root nodes!");
            }
        }

        public static IEnumerable<Node<int>> FinfRoots()
        {
            var rootNodes = nodeByValue.Values.Where(n => n.Parent == null);
            return rootNodes;
        }

        public static Node<int> GetNodeByValue(int value)
        {
            if (!nodeByValue.ContainsKey(value))
            {
                nodeByValue[value] = new Node<int>(value);
            }

            return nodeByValue[value];
        } 
    }
}
