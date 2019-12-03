using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCDemo;
using MVCDemo.Models;

namespace MVCDemo.Models
{
    public class DELETEMEContext : DbContext
    {
        public DELETEMEContext (DbContextOptions<DELETEMEContext> options)
            : base(options)
        {
        }

        public DbSet<MVCDemo.Citizen> Citizen { get; set; }

        public DbSet<MVCDemo.SuperHero> SuperHero { get; set; }

        public DbSet<MVCDemo.SuperPet> SuperPet { get; set; }

        public DbSet<MVCDemo.City> City { get; set; }

        public DbSet<MVCDemo.Costume> Costume { get; set; }

        public DbSet<MVCDemo.PetType> PetType { get; set; }

        public DbSet<MVCDemo.Villian> Villian { get; set; }

        public DbSet<MVCDemo.Models.Role> Role { get; set; }
    }
}
