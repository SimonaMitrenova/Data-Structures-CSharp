namespace FastSearchForStringsInATextFile
{
    using System;
    using System.Collections.Generic;
    using Wintellect.PowerCollections;

    public class FastSearchForStringsInATextFile
    {
        public static void Main(string[] args)
        {
            var wordsCount = new Dictionary<string, int>();
            int numberOfStrings = int.Parse(Console.ReadLine());
            for (int i = 0; i < numberOfStrings; i++)
            {
                string wordToFind = Console.ReadLine().ToLower();
                wordsCount.Add(wordToFind, 0);
            }
            var keys = new List<string>(wordsCount.Keys);

            int numberOfInputLines = int.Parse(Console.ReadLine());
            for (int i = 0; i < numberOfInputLines; i++)
            {
                var buffer = new BigList<char>();
                string inputLine = Console.ReadLine().ToLower();
                buffer.AddRange(inputLine);

                foreach (var word in keys)
                {
                    for (int j = 0; j < buffer.Count - word.Length; j++)
                    {
                        var stringToCompare = string.Concat(buffer.Range(j, word.Length));
                        if (word.Equals(stringToCompare))
                        {
                            wordsCount[word]++;
                        }
                    }
                }
            }

            foreach (var wordCount in wordsCount)
            {
                Console.WriteLine("{0} -> {1}", wordCount.Key, wordCount.Value);
            }
        }
    }
}
