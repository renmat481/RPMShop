using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;


namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache Cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = Cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
                productCategories = new List<ProductCategory>();
        }

        //to save the products to Cache
        public void Commit()
        {
            Cache["productCategories"] = productCategories;
        }

        //to Add new product
        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        //to update the product
        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCategorytoUpdate = productCategories.Find(p => p.ID == productCategory.ID);
            if (productCategorytoUpdate != null)
                productCategorytoUpdate = productCategory;
            else
                throw new Exception("Product Category not Found");
        }

        //To return a List of Products
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        //to Delete the product
        public void Delete(string id)
        {
            ProductCategory productCategorytoDelete = productCategories.Find(p => p.ID == id);
            if (productCategorytoDelete != null)
                productCategories.Remove(productCategorytoDelete);
            else
                throw new Exception(" Product Category not found");
        }

        public ProductCategory Find(string id)
        {
            ProductCategory productCategory = productCategories.Find(p => p.ID == id);
            if (productCategory == null)
                throw new Exception("Not found");
            else
                return productCategory;
        }

    }

    
}
