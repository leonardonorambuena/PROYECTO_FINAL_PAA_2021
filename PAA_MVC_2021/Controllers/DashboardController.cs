using PAA_MVC_2021.Helpers;
using PAA_MVC_2021.Models.Entities;
using PAA_MVC_2021.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAA_MVC_2021.Controllers
{
    [Authorize(Roles = StringHelper.ROLE_ADMINISTRATOR)] // validamos que el usuario conectado sea administrador
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Dashboard
        public ActionResult Index()
        {
            var vm = new DashboardViewModel 
            {
            CountUsers = _db.Users.Count(), // select count(FirstName) from users;
            CountGames = _db.Products.Count(),
            CountNintendo = _db.Products.Count(x => x.PlatformId == 1),
            CountPlay = _db.Products.Count(x => x.PlatformId == 2)
        };
            
            return View(vm);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}