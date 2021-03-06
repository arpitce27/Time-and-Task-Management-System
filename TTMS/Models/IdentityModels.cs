﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using TTMS.Models;
using System.Collections.Generic;
using System.Collections;

namespace TTMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public User()
        {
           this.AssignedWorks = new HashSet<Work>();
        }
        public string Office { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public virtual ICollection<Work> AssignedWorks { get; set; }
        public virtual IdentityRole UserRoles { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        //public ApplicationUser() { }
    }

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<OfficeInfo> OfficeInfo { get; set; }
        public virtual DbSet<WorkType> WorkType { get; set; }

        public System.Data.Entity.DbSet<TTMS.Models.Work> Works { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<WorkLog> WorkLog { get; set; }
        //public IEnumerable ApplicationUsers { get; internal set; }
    }
}