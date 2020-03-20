using AngularWithAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithAPI.Data
{
    public class EfDbContext : DbContext
    {
        public DbSet<Persone> Persones  { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public EfDbContext(DbContextOptions<EfDbContext> options):base(options)
        {}

    }
}
