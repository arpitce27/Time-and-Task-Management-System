﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using TTMS.Models;
using Owin;
using System.Security.Claims;

[assembly: OwinStartupAttribute(typeof(TTMS.Startup))]
namespace TTMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
			//createRolesandUsers();
		}


		// In this method we will create default User roles and Admin user for login
		private void createRolesandUsers()
		{
			ApplicationDbContext context = new ApplicationDbContext();

			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
			var UserManager = new UserManager<User>(new UserStore<User>(context));


			// In Startup iam creating first Admin Role and creating a default Admin User 
			if (!roleManager.RoleExists("Admin"))
			{

				// first we create Admin rool
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Admin";
				roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website	
                var user = new User();
                user.UserName = "arpit";
                user.Email = "arpit.ce27@gmail.com";

                string userPWD = "Ap_123456";

                //var user = new ApplicationUser();
                //user.UserName = "shanu";
                //user.Email = "syedshanumcain@gmail.com";

                //string userPWD = "A@Z200711";

                var chkUser = UserManager.Create(user, userPWD);

				//Add default User to Role Admin
				if (chkUser.Succeeded)
				{
					var result1 = UserManager.AddToRole(user.Id, "Admin");

				}
			}

			// creating Creating Manager role 
			if (!roleManager.RoleExists("Supervisor"))
			{
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Supervisor";
				roleManager.Create(role);

			}

			// creating Creating Employee role 
			if (!roleManager.RoleExists("Student"))
			{
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Student";
				roleManager.Create(role);

			}
		}
	}
}
