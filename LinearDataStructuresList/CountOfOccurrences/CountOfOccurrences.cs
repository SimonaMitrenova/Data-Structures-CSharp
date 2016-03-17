namespace CountOfOccurrences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CountOfOccurrences
    {
        public static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToList();
            var groups = numbers
                .GroupBy(num => num)
                .OrderBy(num => num.Key)
                .Select(num => new
            {
                Number = num.Key,
                Count = num.Count()
            });

            foreach (var @group in groups)
            {
                Console.WriteLine("{0} -> {1} times", group.Number, group.Count);
            }
        }
    }
}
