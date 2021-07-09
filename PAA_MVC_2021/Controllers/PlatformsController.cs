using PAA_MVC_2021.Models.Entities;
using PAA_MVC_2021.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PAA_MVC_2021.Controllers
{
    public class PlatformsController : DefaultBaseController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Platforms
        public async Task<ActionResult> Index()
        {
            var platforms = await _db.ProductPlatforms.Include("Products").ToListAsync();
            return View(platforms);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new PlatformCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PlatformCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                ProductPlatform model = new ProductPlatform();
                model.ProductPlatformName = vm.ProductPlatformName;
                model.ProductPlatformLogo = UploadFile(vm.PlatformFile, "plataformas");
                _db.ProductPlatforms.Add(model);
                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Plataforma creada correctamente";
                return RedirectToAction("Index");
            }
            return View(vm);
        }

    }
}