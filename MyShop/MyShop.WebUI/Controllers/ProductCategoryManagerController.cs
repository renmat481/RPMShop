using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> context;

        public ProductCategoryManagerController(IRepository<ProductCategory> Context)
        {
            this.context = Context;
        }

        // GET: ProductCategory
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
       
             
        }

        //Creating product

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
                return View(productCategory);
            else
            {
                context.Insert(productCategory);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        //Editing Product

        public ActionResult Edit(string id)
        {
            ProductCategory productCategory = context.Find(id);
            if (productCategory == null)
                return HttpNotFound();
            else
                return View(productCategory);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string id)
        {
            ProductCategory productCategorytoEdit = context.Find(id);
            if (productCategorytoEdit == null)
                return HttpNotFound();
            else
            {
                if (!ModelState.IsValid)
                    return View(productCategory);

                productCategorytoEdit.Category = productCategory.Category;                

                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string id)
        {
            ProductCategory productCategorytoDelete = context.Find(id);
            if (productCategorytoDelete == null)
                return HttpNotFound();
            else
                return View(productCategorytoDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(ProductCategory productCategory, string id)
        {
            ProductCategory productCategorytoDelete = context.Find(id);
            if (productCategorytoDelete == null)
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