using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PAA_MVC_2021.Helpers;
using PAA_MVC_2021.Models.Entities;
using PAA_MVC_2021.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace PAA_MVC_2021.Controllers
{

    public class AuthController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Verify(string token)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.UserToken == token);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Imposible activar cuenta token no encontrado";
                return RedirectToAction("Index", "Home");
            }

            user.VerifiedAt = DateTime.Now;
            _db.Entry(user).Property(x => x.VerifiedAt).IsModified = true;
            await _db.SaveChangesAsync();
            TempData["SuccessMessage"] = "El usuario fue activado correctamente";
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // debo generar el registro de usuario
                using (var transaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        User user = await _db.Users.FirstOrDefaultAsync(x => x.EmailAddress == model.EmailAddress);
                        if (user != null)
                        {
                            TempData["ErrorMessage"] = "El usuario ya se encuentra registrado";
                            return View(model);
                        }

                        var role = await _db.Roles.FirstOrDefaultAsync(x => x.RoleName == StringHelper.ROLE_USER);

                        PasswordHelper.CreatePassword(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

                        user = new User
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            EmailAddress = model.EmailAddress,
                            RoleId = role.RoleId,
                            PasswordHash = passwordHash,
                            PasswordSalt = passwordSalt,
                            CreatedAt = DateTime.Now,
                            UserToken = Guid.NewGuid().ToString().Replace("-", string.Empty)
                        };

                        _db.Users.Add(user);
                        await _db.SaveChangesAsync();
                        // todo: Enviar correo electrónico para verificar email ingresado
                        string urlPath = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/Auth/Verify?token="+user.UserToken;
                        string body = $"Para activar su cuenta acceda a la siguiente url <a href='{urlPath}'>Pinche Aquí para activar su cuenta</a>";
                        if (EmailHelper.Send(model.EmailAddress, "Verifique su Email", body, out string message))
                        {
                            transaction.Commit();
                            TempData["SuccessMessage"] = "El usuario fue creado correctamente, verifique su Email";
                            return RedirectToAction("Index", "Home");
                        }
                        transaction.Rollback();
                        TempData["ErrorMessage"] = "Existieron problemas al enviar el correo de validación";
                        return View(model);
                    } catch (Exception e)
                    {
                        transaction.Rollback();
                        TempData["ErrorMessage"] = e.Message; // Crearse un log o enviar un correo al area de desarrollo
                        return View(model);
                    }
                    
                }
                    
               
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            // todo: el inicio de sesión
            // return View();

            if (ModelState.IsValid)
            {
                var user = await _db.Users.FirstOrDefaultAsync(x => x.EmailAddress == model.Email && x.VerifiedAt != null);

                if (user == null)
                {
                    TempData["ErrorMessage"] = "Usuario no encontrado";
                    return RedirectToAction("Login");
                }

                if (!PasswordHelper.CheckPassword(model.Password, user.PasswordHash, user.PasswordSalt))
                {
                    TempData["ErrorMessage"] = "La contraseña no es correcta";
                    return RedirectToAction("Login");
                }
                await InitOwin(user);
                TempData["SuccessMessage"] = "Conectado correctamente";
            }
            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            CloseOwin();
            TempData["SuccessMessage"] = "Sesión cerrada correctamente";
            return RedirectToAction("Index", "Home");
        }


        private async Task InitOwin(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.EmailAddress),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim("FullName", user.FullName)
            };

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            var role = await _db.Roles.FindAsync(user.RoleId);
            if (role != null)
                identity.AddClaim(new Claim(ClaimTypes.Role, role.RoleName));

            GetAuthentication().SignIn(identity);
        }

        private void CloseOwin()
        {
            GetAuthentication().SignOut();
        }

        private IAuthenticationManager GetAuthentication()
        {
            var context = Request.GetOwinContext();
            return context.Authentication;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}