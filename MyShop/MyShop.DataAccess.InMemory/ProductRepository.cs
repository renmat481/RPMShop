using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache Cache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRepository()
        {
            products = Cache["products"] as List<Product>;
            if (products == null)
                products= new List<Product>();
        }

        //to save the products to Cache
        public void Commit()
        {
            Cache["products"] = products;
        }

        //to Add new product
        public void Insert(Product p)
        {
            products.Add(p);
        }

        //to update the product
        public void Update(Product product)
        {
            Product producttoUpdate = products.Find(p => p.ID == product.ID);
            if (producttoUpdate != null)
                producttoUpdate = product;
            else
                throw new Exception("Product not Found");
        }

        //To return a List of Products
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        //to Delete the product
        public void Delete(string id)
        {
            Product producttoDelete = products.Find(p => p.ID == id);
            if (producttoDelete != null)
                products.Remove(producttoDelete);
            else
                throw new Exception(" Product not found");
        }

        public Product Find(string id)
        {
            Product product = products.Find(p => p.ID == id);
            if (product == null)
                throw new Exception("Not found");
            else
                return product;
        }
    }
}
