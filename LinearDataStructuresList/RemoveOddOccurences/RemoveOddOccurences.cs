namespace RemoveOddOccurences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RemoveOddOccurences
    {
        public static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToList();
            var result = numbers.ToList();
            for (int i = 0; i < numbers.Count; i++)
            {
                var sameNumbers = new List<int>();
                var currentNumber = numbers[i];
                for (int j = 0; j < numbers.Count; j++)
                {
                    if (numbers[j] == currentNumber)
                    {
                        sameNumbers.Add(numbers[j]);
                    }
                }
                if (sameNumbers.Count % 2 != 0)
                {
                    result.RemoveAll(n => n == currentNumber);
                }
            }

            Console.WriteLine(string.Join(" ", result));
        }
    }
}
