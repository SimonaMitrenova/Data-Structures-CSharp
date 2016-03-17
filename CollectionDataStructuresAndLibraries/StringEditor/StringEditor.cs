namespace StringEditor
{
    using System;
    using Wintellect.PowerCollections;

    public class StringEditor
    {
        public static void Main(string[] args)
        {
            var text = new BigList<char>();

            string command = Console.ReadLine();
            while (command != "END")
            {
                string[] commandParams = command.Split();

                switch (commandParams[0])
                {
                    case "APPEND":
                        int textIndex = 1;
                        while (textIndex < commandParams.Length)
                        {
                            text.AddRange(commandParams[textIndex]);
                            textIndex++;
                        }
                        
                        Console.WriteLine("OK");
                        break;

                    case "INSERT":
                        int index = int.Parse(commandParams[1]);
                        if (index < 0 || index >= text.Count)
                        {
                            Console.WriteLine("ERROR");
                        }
                        else
                        {
                            text.InsertRange(index, commandParams[2]);
                            Console.WriteLine("OK");
                        }
                        break;

                    case "DELETE":
                        int startIndex = int.Parse(commandParams[1]);
                        int count = int.Parse(commandParams[2]);
                        if (startIndex < 0 || startIndex + count >= text.Count || count < 0)
                        {
                            Console.WriteLine("ERROR");
                        }
                        else
                        {
                            text.RemoveRange(startIndex, count);
                            Console.WriteLine("OK");
                        }
                        break;

                    case "REPLACE":
                        int startIndexReplace = int.Parse(commandParams[1]);
                        int countReplace = int.Parse(commandParams[2]);
                        if (startIndexReplace < 0 || startIndexReplace + countReplace >= text.Count || countReplace < 0)
                        {
                            Console.WriteLine("ERROR");
                        }
                        else
                        {
                            text.RemoveRange(startIndexReplace, countReplace);
                            text.InsertRange(startIndexReplace, commandParams[3]);
                            Console.WriteLine("OK");
                        }
                        break;

                    case "PRINT":
                        for (int i = 0; i < text.Count; i++)
                        {
                            Console.Write(text[i]);
                        }

                        Console.WriteLine();
                        break;
                }

                command = Console.ReadLine();
            }
        }
    }
}
