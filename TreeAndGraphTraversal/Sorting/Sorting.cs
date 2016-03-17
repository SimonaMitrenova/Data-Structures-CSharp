namespace Sorting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Sorting
    {
        public static readonly HashSet<string> Permutations = new HashSet<string>();
        public static int numbersCount;
        public static int numberOfConsecutiveElements;

        public static void Main(string[] args)
        {
            numbersCount = int.Parse(Console.ReadLine());
            int[] sequence = Console.ReadLine().Split().Select(int.Parse).ToArray();
            numberOfConsecutiveElements = int.Parse(Console.ReadLine());

            int numberOfReverses = GetNumberOfReverses(sequence);
            Console.WriteLine(numberOfReverses);
        }

        private static int GetNumberOfReverses(int[] sequence)
        {
            var queue = new Queue<Permutation<int>>();
            var startPermutation = new Permutation<int>(sequence);
            queue.Enqueue(startPermutation);

            while (queue.Count > 0)
            {
                var currentPermutation = queue.Dequeue();
                if (currentPermutation.IsOrdered())
                {
                    return currentPermutation.StepsToOrder;
                }

                for (int i = 0; i <= numbersCount - numberOfConsecutiveElements; i++)
                {
                    var currentSequence = new int[numbersCount];
                    Array.Copy(currentPermutation.Sequence, currentSequence, numbersCount);
                    int[] reversedPart = currentPermutation.Sequence.Skip(i).Take(numberOfConsecutiveElements).Reverse().ToArray();
                    for (int j = i, index = 0; j < i + numberOfConsecutiveElements; j++, index++)
                    {
                        currentSequence[j] = reversedPart[index];
                    }

                    var nextPermutation = new Permutation<int>(currentSequence, currentPermutation.StepsToOrder + 1);
                    if (!Permutations.Contains(nextPermutation.ToString()))
                    {
                        queue.Enqueue(nextPermutation);
                        Permutations.Add(nextPermutation.ToString());
                    }
                }
            }

            return -1;
        }
    }
}
