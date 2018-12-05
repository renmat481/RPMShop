using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        ProductCategoryRepository ProductCategories;

        public ProductManagerController()
        {
            context = new ProductRepository();
            ProductCategories = new ProductCategoryRepository();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        //Creating product

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.product = new Product();
            viewModel.ProductCategories = ProductCategories.Collection();
            return View(viewModel);
            
            //Product product = new Product();
            //return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        //Editing Product

        public ActionResult Edit(string id)
        {
            Product product = context.Find(id);
            if (product == null)
                return HttpNotFound();
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.product = product;
                viewModel.ProductCategories = ProductCategories.Collection();
                return View(viewModel);
            }
                //return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product,string id)
        {
            Product producttoEdit = context.Find(id);
            if (producttoEdit == null)
                return HttpNotFound();
            else
            {
                if (!ModelState.IsValid)
                    return View(product);

                producttoEdit.Category = product.Category;
                producttoEdit.Description = product.Description;
                producttoEdit.Name = product.Name;
                producttoEdit.Price = product.Price;
                producttoEdit.Image = product.Image;

                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string id)
        {
            Product producttoDelete = context.Find(id);
            if (producttoDelete == null)
                return HttpNotFound();
            else
                return View(producttoDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(Product product,string id)
        {
            Product producttoDelete = context.Find(id);
            if (producttoDelete == null)
                return HttpNotFound();
            else
            {
                context.Delete(id);
                context.Commit();
                return RedirectToAction("Index");
            }        
                
        }
    }
}