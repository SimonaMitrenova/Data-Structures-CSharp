namespace OrderedSet
{
    using System;

    public class OrderedSetMain
    {
        public static void Main(string[] args)
        {
            var orderedSet = new OrderedSet<int>();
            orderedSet.Add(17);
            orderedSet.Add(9);
            orderedSet.Add(12);
            orderedSet.Add(19);
            orderedSet.Add(6);
            orderedSet.Add(25);

            foreach (var element in orderedSet)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine(orderedSet.Contains(6));
            Console.WriteLine(orderedSet.Contains(-1));

            orderedSet.Remove(12);
            foreach (var element in orderedSet)
            {
                Console.WriteLine(element);
            }
        }
    }
}
