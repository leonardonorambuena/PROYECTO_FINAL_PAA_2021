using PAA_MVC_2021.Helpers;
using PAA_MVC_2021.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PAA_MVC_2021.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Profile
        public async Task<ActionResult> Index()
        {
            int userId = User.Identity.GetId();
            var user = await _db.Users.FindAsync(userId);
            return View(user);
        }
        // optimzar la memoria
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }

    }
}