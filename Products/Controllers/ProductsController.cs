using Products.Database;
using Products.Models;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Data.Entity;

namespace Products.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDatabaseContext _dbContext;

        public ProductsController()
        {
            _dbContext = new ApplicationDatabaseContext();
        }

        // GET: Products
        public ActionResult Index()
        {
            var allProducts = _dbContext.Products.ToList();

            return View(allProducts);
        }

        public ActionResult Create()
        {
            return View("~/Views/Products/CreateProductView.cshtml");
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                Product newProduct = new Product()
                {
                    Name = product.Name,
                    Description = product.Description,
                    Category = product.Category,
                    Manufacturer = product.Manufacturer,
                    Supplier = product.Supplier,
                    Price = product.Price
                };

                _dbContext.Products.Add(newProduct);
                _dbContext.SaveChanges();

                ViewBag.Message = "Product is created.";

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ViewBag.Message = e.Message;

                return View("~/Views/Products/CreateProductView.cshtml");
            }                
        }
    
        public ActionResult Edit(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product == null)
                return View();
            
            return View("~/Views/Products/EditProductView.cshtml",product);
        }

        [HttpPost]
        public ActionResult Edit(Product editedProduct)
        {
            var product = _dbContext.Products.Find(editedProduct.Id);
            if (product == null)
                return View();

            _dbContext.Entry(product).State = EntityState.Modified;

            product.Id = editedProduct.Id;
            product.Name = editedProduct.Name;
            product.Description = editedProduct.Description;
            product.Category = editedProduct.Category;
            product.Manufacturer = editedProduct.Manufacturer;
            product.Supplier = editedProduct.Supplier;
            product.Price = editedProduct.Price;
             
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product == null)
                return View();

            return View("~/Views/Products/DeleteProductView.cshtml",product);
        }

       [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product == null)
                return View();

            _dbContext.Entry(product).State = EntityState.Deleted;
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            ViewBag.Message = "Product is deleted.";

            return View("~/Views/Products/DeleteProductView.cshtml",product);
        }
    }
}