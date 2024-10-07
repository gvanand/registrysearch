using Microsoft.EntityFrameworkCore;
using RegistrySearch.Domain;

namespace RegistrySearch.DataAccess
{
    public class RegistryDbContext : DbContext
    {
        public DbSet<PeopleRegistry> Registry { get; set; }
        public DbSet<BulkSearchResult> BulkSearchResult { get; set; }
        public DbSet<IndividualSearchResult> IndividualSearchResult { get; set; }
        public RegistryDbContext(DbContextOptions<RegistryDbContext> options)
       : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<PeopleRegistry>(new PeopleRegistryMapping());
            modelBuilder.ApplyConfiguration<BulkSearchResult>(new BulkSearchResultMapping());
            modelBuilder.ApplyConfiguration<IndividualSearchResult>(new IndividualSearchResultMapping());
        }
    }

}
