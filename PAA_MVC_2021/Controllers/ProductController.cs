using PAA_MVC_2021.Helpers;
using PAA_MVC_2021.Models.Entities;
using PAA_MVC_2021.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PAA_MVC_2021.Controllers
{
    [Authorize(Roles = StringHelper.ROLE_ADMINISTRATOR)]
    public class ProductController : DefaultBaseController
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Product
        public async Task<ActionResult> Index(ProductIndexViewModel vm)
        {
            vm.Products = await GetProducts(vm);

            vm.Platforms = await _db.ProductPlatforms
                                    .OrderBy(x => x.ProductPlatformName)
                                    .ToListAsync();

            return View(vm);
        }

        public async Task<List<Product>> GetProducts(ProductIndexViewModel vm)
        {
            var queryProduct = _db.Products.AsQueryable();

            if (vm.ProductCode != null)
                queryProduct = queryProduct.Where(x => x.ProductCode == vm.ProductCode);
            if (vm.ProductName != null)
                queryProduct = queryProduct.Where(x => x.ProductName == vm.ProductName);
            if (vm.PlatformId != null)
                queryProduct = queryProduct.Where(x => x.PlatformId == vm.PlatformId);

            switch (vm.Sort)
            {
                case 1:
                    queryProduct = queryProduct.OrderBy(x => x.ProductCode);
                    break;
                case -1:
                    queryProduct = queryProduct.OrderByDescending(x => x.ProductCode);
                    break;
                case 2:
                    queryProduct = queryProduct.OrderBy(x => x.ProductName);
                    break;
                case -2:
                    queryProduct = queryProduct.OrderByDescending(x => x.ProductName);
                    break;
                case 3:
                    queryProduct = queryProduct.OrderBy(x => x.ProductPrice);
                    break;
                case -3:
                    queryProduct = queryProduct.OrderByDescending(x => x.ProductPrice);
                    break;
                case 4:
                    queryProduct = queryProduct.OrderBy(x => x.ProducStock);
                    break;
                case -4:
                    queryProduct = queryProduct.OrderByDescending(x => x.ProducStock);
                    break;
                default:
                    queryProduct = queryProduct.OrderByDescending(x => x.ProductName);
                    break;
            }

            return await queryProduct.ToListAsync();

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
            var vm = await GetCreateViewModel(product);

            return View(vm);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var vm = await GetCreateViewModel();  
            return View(vm);
        }

        public async Task<ProductCreateViewModel> GetCreateViewModel(Product product = null)
        {
            var vm = new ProductCreateViewModel();
            vm.ProductPlatforms = await _db.ProductPlatforms
                                    .OrderBy(x => x.ProductPlatformName)
                                    .ToListAsync();
            if (product != null)
                vm.Product = product;

            return vm;
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
                    ProductPrice = (int)model.ProductPrice,
                    PlatformId = model.PlatformId
                };

                product.ProductImage = UploadFile(model.ProductFile, "products");

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

                var product = await _db.Products.FindAsync(model.ProductId);
                if (product == null)
                {
                    TempData["ErrorMessage"] = "El producto no fue encontrado, imposible actualizar";
                    return RedirectToAction("Index");
                }
                product.ProducStock = (int)model.ProducStock;
                product.ProductName = model.ProductName;
                product.ProductPrice = (int)model.ProductPrice;
                product.ProductCode = model.ProductCode;

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