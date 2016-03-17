namespace CalculateSequenceWithAQueue
{
    using System;
    using System.Collections.Generic;

    public class CalculateSequenceWithAQueue
    {
        public static void Main(string[] args)
        {
            int startNumber = int.Parse(Console.ReadLine());
            var results = new List<int>();
            var numbersToCalculate = new Queue<int>();
            numbersToCalculate.Enqueue(startNumber);

            while (results.Count < 50)
            {
                var currentNumber = numbersToCalculate.Dequeue();
                results.Add(currentNumber);

                numbersToCalculate.Enqueue(currentNumber + 1);
                numbersToCalculate.Enqueue(currentNumber * 2 + 1);
                numbersToCalculate.Enqueue(currentNumber + 2);
            }

            Console.WriteLine(string.Join(", ", results));
        }
    }
}
