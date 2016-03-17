namespace PlayWithTrees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PlayWithTreesMain
    {
        public static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();
        private static int longestPath;
        private static Tree<int> longestPathLeaf;
        private static int pathSum;
        private static ICollection<IEnumerable<int>> pathsOfGivenSum = new List<IEnumerable<int>>();
        private static int subtreeSum;

        public static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            for (int i = 1; i < nodesCount; i++)
            {
                int[] edge = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int parentValue = edge[0];
                Tree<int> parentNode = GetTreeNodeByValue(parentValue);
                int childValue = edge[1];
                Tree<int> childNode = GetTreeNodeByValue(childValue);
                parentNode.Children.Add(childNode);
                childNode.Parent = parentNode;
            }

            pathSum = int.Parse(Console.ReadLine());
            subtreeSum = int.Parse(Console.ReadLine());

            var rootNode = FindRootNode();
            Console.WriteLine("Root node: {0}", rootNode.Value);

            var leafNodes = FindLeafNodes();
            Console.WriteLine("Leaf nodes: {0}", string.Join(", ", leafNodes.Select(n => n.Value).OrderBy(n => n)));

            var middleNodes = FindMiddleNodes();
            Console.WriteLine("Middle nodes: {0}", string.Join(", ", middleNodes.Select(n => n.Value).OrderBy(n => n)));

            var longestPath = FindLongestPath(rootNode);
            Console.WriteLine("Longest path: {0}", string.Join(" -> ", longestPath));
            Console.WriteLine("(length = {0})", longestPath.ToList().Count);

            FindPathsOfGivenSum(rootNode, rootNode.Value);
            Console.WriteLine("Paths of sum {0}:", pathSum);
            foreach (var paths in pathsOfGivenSum)
            {
                Console.WriteLine(string.Join(" -> ", paths));
            }

            Console.WriteLine("Subtrees of sum {0}:", subtreeSum);
            FindAllSubTreesOfSum(rootNode);
        }

        public static void FindAllSubTreesOfSum(Tree<int> tree)
        {
            if (CalculateSubTreeSum(tree) == subtreeSum)
            {
                var subTree = new List<int>();
                DfsTraverse(tree, subTree);
                Console.WriteLine(string.Join(" + ", subTree));
            }
            else
            {
                foreach (var child in tree.Children)
                {
                    FindAllSubTreesOfSum(child);
                }
            }
        }

        private static void DfsTraverse(Tree<int> tree, List<int> subTree)
        {
            subTree.Add(tree.Value);
            foreach (var child in tree.Children)
            {
                DfsTraverse(child, subTree);
            }
        }

        public static long CalculateSubTreeSum(Tree<int> tree)
        {
            long subTreeSum = 0L;
            subTreeSum += tree.Value;
            foreach (var child in tree.Children)
            {
                subTreeSum += CalculateSubTreeSum(child);
            }

            return subTreeSum;
        }

        public static void FindPathsOfGivenSum(Tree<int> tree, int sum)
        {
            if (sum == pathSum)
            {
                var result = FindBackPath(tree);
                pathsOfGivenSum.Add(result.ToList());
            }
            else
            {
                foreach (var child in tree.Children)
                {
                    FindPathsOfGivenSum(child, sum + child.Value);
                }
            }
        }

        private static IEnumerable<int> FindBackPath(Tree<int> tree)
        {
            var result = new List<int>();
            while (tree != null)
            {
                result.Add(tree.Value);
                tree = tree.Parent;
            }

            result.Reverse();
            return result;
        }


        public static IEnumerable<int> FindLongestPath(Tree<int> tree)
        {
            DfsLongestPath(tree, 0);

            var result = new List<int>();
            while (longestPathLeaf != null)
            {
                result.Add(longestPathLeaf.Value);
                longestPathLeaf = longestPathLeaf.Parent;
            }

            result.Reverse();

            return result;
        }

        private static void DfsLongestPath(Tree<int> node, int depth)
        {
            foreach (var child in node.Children)
            {
                DfsLongestPath(child, depth + 1);
            }

            if (depth > longestPath)
            {
                longestPath = depth;
                longestPathLeaf = node;
            }
        }

        public static Tree<int> GetTreeNodeByValue(int value)
        {
            if (!nodeByValue.ContainsKey(value))
            {
                nodeByValue[value] = new Tree<int>(value);
            }

            return nodeByValue[value];
        }

        public static Tree<int> FindRootNode()
        {
            var rootNode = nodeByValue.Values.FirstOrDefault(n => n.Parent == null);
            return rootNode;
        }

        public static IEnumerable<Tree<int>> FindMiddleNodes()
        {
            var middleNodes = nodeByValue.Values.Where(node => node.Children.Count > 0 && node.Parent != null);
            return middleNodes;
        }

        public static IEnumerable<Tree<int>> FindLeafNodes()
        {
            var leafNodes = nodeByValue.Values.Where(node => node.Children.Count == 0);
            return leafNodes;
        }
    }
}
