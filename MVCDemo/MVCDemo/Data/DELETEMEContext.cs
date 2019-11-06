using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCDemo;

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
    }
}
