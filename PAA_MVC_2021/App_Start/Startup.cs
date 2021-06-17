using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(PAA_MVC_2021.App_Start.Startup))]

namespace PAA_MVC_2021.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Para obtener más información sobre cómo configurar la aplicación, visite https://go.microsoft.com/fwlink/?LinkID=316888

            app.UseCookieAuthentication(
                new CookieAuthenticationOptions { 
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    ExpireTimeSpan  = TimeSpan.FromMinutes(30),
                    LoginPath = new PathString("/Auth/Login")
                });
        }
    }
}
