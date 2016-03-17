namespace CustomLinkedList
{
    using System;

    public class CustomLinkedListMain
    {
        public static void Main(string[] args)
        {
            var myList = new CustomLinkedList<int>();
            for (int i = 1; i <= 20; i++)
            {
                myList.Add(i);
            }
            Console.WriteLine(string.Join(" ", myList));
            Console.WriteLine(myList.Count);
            Console.WriteLine(myList.FirstIndexOf(5));
            int element = myList.Remove(0);
            Console.WriteLine(element);
            Console.WriteLine(string.Join(" ", myList));
        }
    }
}
