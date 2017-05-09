using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TODO.Models;
using TODO.Controllers;
using System.Security.Claims;
using System.Data.Entity.Validation;
using System.Diagnostics;

[assembly: OwinStartupAttribute(typeof(TODO.Startup))]
namespace TODO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
