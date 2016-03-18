namespace CollectionOfProducts
{
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class ProductsRepository
    {
        private Dictionary<int, Product> products; 
        private OrderedDictionary<decimal, SortedSet<Product>> productsByPrice;
        private Dictionary<string, SortedSet<Product>> productsByTitle;
        private Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>> productsByTitleAndPrice;
        private Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>> productsBySupplierAndPrice;

        public ProductsRepository()
        {
            this.products = new Dictionary<int, Product>();
            this.productsByPrice = new OrderedDictionary<decimal, SortedSet<Product>>();
            this.productsByTitle = new Dictionary<string, SortedSet<Product>>();
            this.productsByTitleAndPrice = new Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>>();
            this.productsBySupplierAndPrice = new Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>>();
        }


        public void Add(int id, string title, string supplier, decimal price)
        {
            var product = new Product(id, title, supplier, price);
            this.products[id] = product;
            if (!this.productsByPrice.ContainsKey(price))
            {
                this.productsByPrice[price] = new SortedSet<Product>();
            }
            this.productsByPrice[price].Add(product);

            if (!this.productsByTitle.ContainsKey(title))
            {
                this.productsByTitle[title] = new SortedSet<Product>();
            }
            this.productsByTitle[title].Add(product);

            if (!this.productsByTitleAndPrice.ContainsKey(title))
            {
                this.productsByTitleAndPrice[title] = new OrderedDictionary<decimal, SortedSet<Product>>();
            }
            if (!this.productsByTitleAndPrice[title].ContainsKey(price))
            {
                this.productsByTitleAndPrice[title][price] = new SortedSet<Product>();
            }
            this.productsByTitleAndPrice[title][price].Add(product);

            if (!this.productsBySupplierAndPrice.ContainsKey(supplier))
            {
                this.productsBySupplierAndPrice[supplier] = new OrderedDictionary<decimal, SortedSet<Product>>();
            }
            if (!this.productsBySupplierAndPrice[supplier].ContainsKey(price))
            {
                this.productsBySupplierAndPrice[supplier][price] = new SortedSet<Product>();
            }
            this.productsBySupplierAndPrice[supplier][price].Add(product);
        }

        public bool Remove(int id)
        {
            if (!this.products.ContainsKey(id))
            {
                return false;
            }

            var productToRemove = this.products[id];
            this.products.Remove(id);
            this.productsByPrice[productToRemove.Price].Remove(productToRemove);
            this.productsByTitle[productToRemove.Title].Remove(productToRemove);
            this.productsByTitleAndPrice[productToRemove.Title][productToRemove.Price].Remove(productToRemove);
            this.productsBySupplierAndPrice[productToRemove.Supplier][productToRemove.Price].Remove(productToRemove);

            return true;
        }

        public IEnumerable<Product> FindProductsInPriceRange(decimal from, decimal to)
        {
            return this.productsByPrice.Range(from, true, to, true).SelectMany(p => p.Value);
        }

        public IEnumerable<Product> FindProductsByTitle(string title)
        {
            if (!this.productsByTitle.ContainsKey(title))
            {
                return Enumerable.Empty<Product>();
            }

            return this.productsByTitle[title];
        }

        public IEnumerable<Product> FindProductsByTitleAndPrice(string title, decimal price)
        {
            if (!this.productsByTitleAndPrice.ContainsKey(title) || !this.productsByTitleAndPrice[title].ContainsKey(price))
            {
                return Enumerable.Empty<Product>();
            }

            return this.productsByTitleAndPrice[title][price];
        }

        public IEnumerable<Product> FindProductsByTitleInPriceRange(string title, decimal from, decimal to)
        {
            if (!this.productsByTitleAndPrice.ContainsKey(title))
            {
                return Enumerable.Empty<Product>();
            }

            return this.productsByTitleAndPrice[title].Range(from, true, to, true).SelectMany(p => p.Value);
        }

        public IEnumerable<Product> FindProductsBySupplierAndPrice(string supplier, decimal price)
        {
            if (!this.productsBySupplierAndPrice.ContainsKey(supplier) || !this.productsBySupplierAndPrice[supplier].ContainsKey(price))
            {
                return Enumerable.Empty<Product>();
            }

            return this.productsBySupplierAndPrice[supplier][price];
        }

        public IEnumerable<Product> FindProductsBySupplierInPriceRange(string supplier, decimal from, decimal to)
        {
            if (!this.productsBySupplierAndPrice.ContainsKey(supplier))
            {
                return Enumerable.Empty<Product>();
            }

            return this.productsBySupplierAndPrice[supplier].Range(from, true, to, true).SelectMany(p => p.Value);
        }
    }
}
