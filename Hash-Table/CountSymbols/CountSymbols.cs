namespace CountSymbols
{
    using System;
    using System.Linq;

    public class CountSymbols
    {
        public static void Main(string[] args)
        {
            string text = Console.ReadLine();
            var charCounts = new HashTable<char, int>();
            for (int i = 0; i < text.Length; i++)
            {
                if (!charCounts.ContainsKey(text[i]))
                {
                    charCounts[text[i]] = 0;
                }

                charCounts[text[i]]++;
            }

            var keys = charCounts.Keys.OrderBy(n => n);
            foreach (var key in keys)
            {
                Console.WriteLine("{0}: {1} time/s", key, charCounts[key]);
            }
        }
    }
}
