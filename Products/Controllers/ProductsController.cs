using Products.Database;
using Products.Models;
using System.Web.Mvc;
using System;
using System.Linq;

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

                return View("~/Views/Products/CreateProductView.cshtml");
            }
            catch(Exception e)
            {
                ViewBag.Message = e.Message;

                return View("~/Views/Products/CreateProductView.cshtml");
            }
                 
        }
    }
}