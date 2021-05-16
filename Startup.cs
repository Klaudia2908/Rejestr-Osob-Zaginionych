using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using RejOsZag.Models;

[assembly: OwinStartupAttribute(typeof(RejOsZag.Startup))]
namespace RejOsZag
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createApplicationRoles();
        }

        private void createApplicationRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
      
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));   
            if (!roleManager.RoleExists("User"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
 
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
        }
    }
}
