using PAA_MVC_2021.Helpers;
using PAA_MVC_2021.Models.Entities;
using PAA_MVC_2021.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PAA_MVC_2021.Controllers
{
    [Authorize(Roles = StringHelper.ROLE_ADMINISTRATOR)]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Product
        public async Task<ActionResult> Index()
        {
            var products = await _db.Products
                                .OrderBy(x => x.ProductName)
                                .ToListAsync();

            return View(products);
        }
        [HttpGet]
        public async Task<ActionResult> Show(int productId)
        {
            var product = await _db.Products.FindAsync(productId);

            if (product == null)
            {
                TempData["ErrorMessage"] = "El producto no fue encontrado";
                return RedirectToAction("Index");
            }

            return View(product);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await _db.Products.FirstOrDefaultAsync(x => x.ProductCode == model.ProductCode);
                if (product != null)
                {
                    TempData["ErrorMessage"] = "El código del producto ya se encuentra registrado";
                    return View(model);
                }
                product = new Product {
                    ProductCode = model.ProductCode,
                    ProducStock = (int)model.ProducStock,
                    ProductName = model.ProductName,
                    ProductPrice = (int)model.ProductPrice
                };

                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "El producto fue creado correctamente";
                return RedirectToAction("Index");

            }

            return View(model);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ProductId == null)
                {
                    TempData["ErrorMessage"] = "Imposible Actualizar el registro";
                    return RedirectToAction("Index");
                }

                var product = await _db.Products.FirstOrDefaultAsync(x => x.ProductCode == model.ProductCode);
                if (product == null)
                {
                    TempData["ErrorMessage"] = "El producto no fue encontrado, imposible actualizar";
                    return RedirectToAction("Index");
                }
                product.ProducStock = (int)model.ProducStock;
                product.ProductName = model.ProductName;
                product.ProductPrice = (int)model.ProductPrice;

                _db.Products.AddOrUpdate(product);
                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "El producto fue actualizado correctamente";
                return RedirectToAction("Index");

            }

            return RedirectToAction("Show", model.ProductId);
        }

        public async Task<ActionResult> Delete(int? productId)
        {
            if (productId == null)
            {
                TempData["ErrorMessage"] = "Imposible eliminar el producto";
                return RedirectToAction("Index");
            }
            var product = await _db.Products.FindAsync(productId);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Producto no encontrado, imposible eliminar";
                return RedirectToAction("Index");
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = "El producto fue eliminado correctamente";
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }


    }
}