namespace CollectionOfProducts
{
    using System;

    public class Product : IComparable<Product>
    {
        public Product(int id, string title, string supplier, decimal price)
        {
            this.Id = id;
            this.Title = title;
            this.Supplier = supplier;
            this.Price = price;
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Supplier { get; private set; }

        public decimal Price { get; private set; }

        public int CompareTo(Product other)
        {
            return this.Id.CompareTo(other.Id);
        }
    }
}
