using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using PeopleFinder.Domain;
using System.Collections.Generic;

namespace PeopleFinder.DataAccess
{
    public class PeopleFinderDbContext : DbContext
    {
        public DbSet<PeopleRegistry> Registry { get; set; }
        public PeopleFinderDbContext(DbContextOptions<PeopleFinderDbContext> options)
       : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<PeopleRegistry>(new PeopleRegistryMapping());
        }
    }

}
