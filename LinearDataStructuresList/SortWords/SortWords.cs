namespace SortWords
{
    using System;
    using System.Linq;

    public class SortWords
    {
        public static void Main(string[] args)
        {
            var words = Console.ReadLine().Split().ToList();
            var sortedWords = words.OrderBy(w => w);
            Console.WriteLine(string.Join(" ", sortedWords));
        }
    }
}
