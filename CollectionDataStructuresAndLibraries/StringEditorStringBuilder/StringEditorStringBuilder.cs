namespace StringEditorStringBuilder
{
    using System;
    using System.Text;

    public class StringEditorStringBuilder
    {
        public static void Main(string[] args)
        {
            var text = new StringBuilder();
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
                            text.Append(commandParams[textIndex]);
                            textIndex++;
                        }

                        Console.WriteLine("OK");
                        break;

                    case "INSERT":
                        int index = int.Parse(commandParams[1]);
                        if (index < 0 || index >= text.Length)
                        {
                            Console.WriteLine("ERROR");
                        }
                        else
                        {
                            text.Insert(index, commandParams[2]);
                            Console.WriteLine("OK");
                        }
                        break;

                    case "DELETE":
                        int startIndex = int.Parse(commandParams[1]);
                        int count = int.Parse(commandParams[2]);
                        if (startIndex < 0 || startIndex + count >= text.Length || count < 0)
                        {
                            Console.WriteLine("ERROR");
                        }
                        else
                        {
                            text.Remove(startIndex, count);
                            Console.WriteLine("OK");
                        }
                        break;

                    case "REPLACE":
                        int startIndexReplace = int.Parse(commandParams[1]);
                        int countReplace = int.Parse(commandParams[2]);
                        if (startIndexReplace < 0 || startIndexReplace + countReplace >= text.Length || countReplace < 0)
                        {
                            Console.WriteLine("ERROR");
                        }
                        else
                        {
                            string oldValue = text.ToString().Substring(startIndexReplace, countReplace);
                            text.Replace(oldValue, commandParams[3], startIndexReplace, countReplace);
                            Console.WriteLine("OK");
                        }
                        break;

                    case "PRINT":
                        Console.WriteLine(text);
                        break;
                }

                command = Console.ReadLine();
            }
        }
    }
}
