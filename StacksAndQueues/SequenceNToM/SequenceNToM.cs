namespace SequenceNToM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SequenceNtoM
    {
        public static void Main(string[] args)
        {
            var queue = new Queue<Node<int>>();
            int n = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());

            var newNode = new Node<int>(n);
            queue.Enqueue(newNode);
            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                if (currentNode.Value < m)
                {
                    queue.Enqueue(new Node<int>(currentNode.Value + 1, currentNode));
                    queue.Enqueue(new Node<int>(currentNode.Value + 2, currentNode));
                    queue.Enqueue(new Node<int>(currentNode.Value * 2, currentNode));
                }
                else if (currentNode.Value == m && n != m)
                {
                    PrintSolution(currentNode);
                    return;
                }
            }
            Console.WriteLine("No solution.");
        }

        private static void PrintSolution(Node<int> node)
        {
            var results = new List<int>();
            var item = node;
            while (item != null)
            {
                results.Add(item.Value);
                item = item.PrevNode;
            }

            Console.WriteLine(string.Join(" -> ", results.OrderBy(n => n)));
        }
    }
}
