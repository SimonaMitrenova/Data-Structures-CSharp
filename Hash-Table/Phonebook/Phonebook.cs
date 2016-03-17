namespace Phonebook
{
    using System;

    public class Phonebook
    {
        public static void Main(string[] args)
        {
            var phonebook = new HashTable<string, string>();

            string commandLine = Console.ReadLine();
            while (commandLine != "search")
            {
                string[] parts = commandLine.Split('-');
                string name = parts[0];
                string phoneNumber = parts[1];

                if (!phonebook.ContainsKey(name))
                {
                    phonebook[name] = phoneNumber;
                }

                commandLine = Console.ReadLine();
            }

            string searchName = Console.ReadLine();
            while (!string.IsNullOrEmpty(searchName))
            {
                var phone = phonebook.Find(searchName);
                if (phone != null)
                {
                    Console.WriteLine("{0} -> {1}", searchName, phone.Value);
                }
                else
                {
                    Console.WriteLine("Contact {0} does not exist.", searchName);
                }

                searchName = Console.ReadLine();
            }
        }
    }
}
