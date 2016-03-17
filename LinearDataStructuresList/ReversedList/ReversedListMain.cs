namespace ReversedList
{
    using System;

    public class ReversedListMain
    {
        public static void Main(string[] args)
        {
            var myList = new ReversedList<int>();
            for (int i = 1; i <= 20; i++)
            {
                myList.Add(i);
            }

            Console.WriteLine(string.Join(" ", myList));

            foreach (var number in myList)
            {
                Console.Write("{0} ", number);
            }
            Console.WriteLine();

            myList[2] = 66;
            Console.WriteLine(string.Join(" ", myList));
            int myNumber = myList[4];
            Console.WriteLine(myNumber);

            myList.Remove(3);
            Console.WriteLine(string.Join(" ", myList));
            Console.WriteLine(myList.Count);
            //Console.WriteLine(myList[-1]);
        }
    }
}
