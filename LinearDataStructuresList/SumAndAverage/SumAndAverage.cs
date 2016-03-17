namespace SumAndAverage
{
    using System;
    using System.Linq;

    public class SumAndAverage
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Sun=0; Average=0");
                return;
            }

            var numbers = input.Split().Select(int.Parse).ToList();
            int sum = numbers.Sum();
            double average = numbers.Average();
            Console.WriteLine("Sun={0}; Average={1}", sum, average);
        }
    }
}
