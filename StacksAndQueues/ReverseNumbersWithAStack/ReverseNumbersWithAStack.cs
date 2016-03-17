namespace ReverseNumbersWithAStack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReverseNumbersWithAStack
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                return;
            }
            
            var numbers = input.Split().Select(int.Parse).ToArray();
            var stackNumbers = new Stack<int>(numbers);
            Console.WriteLine(string.Join(" ", stackNumbers));
        }
    }
}
