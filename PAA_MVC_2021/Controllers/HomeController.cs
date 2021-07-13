using PAA_MVC_2021.DAL;
using PAA_MVC_2021.Models.Entities;
using PAA_MVC_2021.Models.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PAA_MVC_2021.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        public async Task<ActionResult> Index(ProductIndexViewModel vm)
        {
            vm.Products = await ProductDAL.GetProducts(vm, _db);

            vm.Platforms = await _db.ProductPlatforms
                                    .OrderBy(x => x.ProductPlatformName)
                                    .ToListAsync();

            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Private()
        {
            return RedirectToAction("About");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }

    }
}