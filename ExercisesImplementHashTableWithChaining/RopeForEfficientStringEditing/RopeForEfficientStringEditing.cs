namespace RopeForEfficientStringEditing
{
    using System;
    using Wintellect.PowerCollections;

    public class RopeForEfficientStringEditing
    {
        public static void Main(string[] args)
        {
            var text = new BigList<char>();

            string command = Console.ReadLine();
            while (string.IsNullOrEmpty(command))
            {
                string[] commandParts = Console.ReadLine().Split();
                switch (commandParts[0])
                {
                    case "INSERT":
                        text.AddRangeToFront(commandParts[1]);
                        Console.WriteLine("OK");
                        break;

                    case "APPEND":
                        text.AddRange(commandParts[1]);
                        Console.WriteLine("OK");
                        break;

                    case "DELETE":
                        int startIndex = int.Parse(commandParts[1]);
                        int count = int.Parse(commandParts[2]);
                        if (startIndex < 0 || startIndex >= text.Count || startIndex + count >= text.Count)
                        {
                            Console.WriteLine("ERROR");
                        }
                        else
                        {
                            text.RemoveRange(startIndex, count);
                            Console.WriteLine("OK");
                        }
                        break;

                    case "PRINT":
                        Console.WriteLine(text.ToString());
                        break;
                }

                command = Console.ReadLine();
            }
            
        }
    }
}
