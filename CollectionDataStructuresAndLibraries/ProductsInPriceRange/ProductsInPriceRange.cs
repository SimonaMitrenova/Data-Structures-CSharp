namespace ProductsInPriceRange
{
    using System;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class ProductsInPriceRange
    {
        public static void Main(string[] args)
        {
            var products = new OrderedMultiDictionary<decimal, string>(true);
            int numberfProducts = int.Parse(Console.ReadLine());
            for (int i = 0; i < numberfProducts; i++)
            {
                string[] product = Console.ReadLine().Split();
                string productName = product[0];
                decimal productPrice = decimal.Parse(product[1]);
                products.Add(productPrice, productName);
            }

            decimal[] priceRange = Console.ReadLine().Split().Select(decimal.Parse).ToArray();

            var productsInrange = products.Range(priceRange[0], true, priceRange[1], true).Take(20);
            foreach (var element in productsInrange)
            {
                foreach (var product in element.Value)
                {
                    Console.WriteLine("{0} {1}", element.Key, product);
                }
            }
        }
    }
}
