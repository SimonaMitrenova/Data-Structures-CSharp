namespace LongestSubsequence
{
    using System;
    using System.Linq;

    public class LongestSubsequence
    {
        public static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToList();
            var longestSubsequence = numbers.GroupBy(num => num).OrderByDescending(num => num.Count()).First();
            Console.WriteLine(string.Join(" ", longestSubsequence));
        }
    }
}
