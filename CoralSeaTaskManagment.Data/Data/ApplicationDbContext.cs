using CoralSeaTaskManagment.Model.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoralSeaTaskManagment.Data.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
 : base(options)
        {
        }
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) 
        //{ 

        //}
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Assign> Assigns { get; set; }
        public DbSet<Otype> Otypes { get; set; }
        public DbSet<Ecategory> Ecategories { get; set; }
        public DbSet<Eclass> Eclasses { get; set; }
        public DbSet<Efamily> Efamilies { get; set; }
        public DbSet<Estatus> Estatuses { get; set; }
        public DbSet<Periority> Periorities { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Gitem> Gitems { get; set; }
        public DbSet<Glocation> Glocations { get; set; }
        public DbSet<Grooms> Grooms { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Gorder> Gorders { get; set; }
        public DbSet<Assignation> Assignations { get; set; }
        public DbSet<Outgoing> Outgoings { get; set; }
        public DbSet<Spart> Sparts { get; set; }
        public DbSet<Scheduale> Scheduales { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
