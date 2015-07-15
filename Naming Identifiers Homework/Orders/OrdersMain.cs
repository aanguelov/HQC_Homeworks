namespace Orders
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    public class OrdersMain
    {
        private static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var dataMapper = new DataMapper();
            var categories = dataMapper.GetAllCategories().ToArray();
            var products = dataMapper.GetAllProducts().ToArray();
            var orders = dataMapper.GetAllOrders().ToArray();

            // Names of the 5 most expensive products
            var top5MostExpensiveProducts = products
                .OrderByDescending(p => p.UnitPrice)
                .Take(5)
                .Select(p => p.Name);
            Console.WriteLine(string.Join(Environment.NewLine, top5MostExpensiveProducts));

            Console.WriteLine(new string('-', 10));

            // Number of products in each category
            var categoryProducts = products
                .GroupBy(p => p.CategoryId)
                .Select(group => new
                                     {
                                         Category = categories.First(c => c.Id == group.Key).Name, 
                                         Count = group.Count()
                                     })
                .ToList();
            foreach (var item in categoryProducts)
            {
                Console.WriteLine("{0}: {1}", item.Category, item.Count);
            }

            Console.WriteLine(new string('-', 10));

            // The 5 top products (by order quantity)
            var top5ProductsByOrderQuantity = orders
                .GroupBy(o => o.ProductId)
                .Select(group => new
                                   {
                                       Product = products.First(p => p.Id == group.Key).Name, 
                                       Quantities = group.Sum(o => o.Quantity)                                   
                                   })
                .OrderByDescending(group => group.Quantities)
                .Take(5)
                .ToArray();
            foreach (var item in top5ProductsByOrderQuantity)
            {
                Console.WriteLine("{0}: {1}", item.Product, item.Quantities);
            }

            Console.WriteLine(new string('-', 10));

            // The most profitable category
            var category = orders
                .GroupBy(o => o.ProductId)
                .Select(group => new
                                 {
                                     ProductCategoryId = products.First(p => p.Id == group.Key).CategoryId, 
                                     Price = products.First(p => p.Id == group.Key).UnitPrice, 
                                     Quantity = group.Sum(p => p.Quantity)                                 
                                 })
                .GroupBy(product => product.ProductCategoryId)
                .Select(productGroup => new
                                   {
                                       CategoryName = categories.First(c => c.Id == productGroup.Key).Name, 
                                       TotalQuantity = productGroup.Sum(g => g.Quantity * g.Price)                                   
                                   })
                .OrderByDescending(productGroup => productGroup.TotalQuantity)
                .First();
            Console.WriteLine("{0}: {1}", category.CategoryName, category.TotalQuantity);
        }
    }
}
