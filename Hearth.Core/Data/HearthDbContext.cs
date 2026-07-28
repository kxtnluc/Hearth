using Microsoft.EntityFrameworkCore;
using Hearth.Core.Models.Finance;

namespace Hearth.Core.Data;

public class HearthDbContext : DbContext
{
    public HearthDbContext(DbContextOptions<HearthDbContext> options)
        : base(options)
    {
    }

    // DbSets go here once you have entities, e.g.:
     public DbSet<Transaction> Transactions => Set<Transaction>();
     public DbSet<Account> Accounts => Set<Account>();
     public DbSet<Bank> Banks => Set<Bank>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Applies all IEntityTypeConfiguration<T> classes in this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HearthDbContext).Assembly);
    }
}