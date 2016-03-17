namespace RoundDance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RoundDance
    {
        public static Dictionary<int, List<int>> nodesByValue = new Dictionary<int, List<int>>();
        private static int longestRoundDance;

        public static void Main(string[] args)
        {
            int numberOfFriendShips = int.Parse(Console.ReadLine());
            int leader = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfFriendShips; i++)
            {
                int[] edge = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int firstNode = edge[0];
                int secondNode = edge[1];
                AddFriendShip(firstNode, secondNode);
                AddFriendShip(secondNode, firstNode);
            }

            Dfs(leader, leader, 1);
            Console.WriteLine(longestRoundDance);
        }

        public static void Dfs(int node, int prevNode, int depth)
        {
            foreach (var childNode in nodesByValue[node])
            {
                if (childNode != prevNode)
                {
                    Dfs(childNode, node, depth + 1);
                }
            }

            if (depth > longestRoundDance)
            {
                longestRoundDance = depth;
            }
        }

        public static void AddFriendShip(int node, int friendNode)
        {
            if (!nodesByValue.ContainsKey(node))
            {
                nodesByValue[node] = new List<int>();
            }
            nodesByValue[node].Add(friendNode);
        }
    }
}
