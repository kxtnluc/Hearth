using Microsoft.EntityFrameworkCore;
using Hearth.Core.Models.Finance;
using Hearth.Core.Models;

namespace Hearth.Core.Data;

public class HearthDbContext : DbContext
{
    public HearthDbContext(DbContextOptions<HearthDbContext> options)
        : base(options)
    {
    }
    // Db Sets
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Bank> Banks => Set<Bank>();
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<TransactionCategory> TransactionCategories => Set<TransactionCategory>();
    public DbSet<TransactionCategoryRule> TransactionCategoryRules => Set<TransactionCategoryRule>();
    public DbSet<RuleCondition> RuleConditions => Set<RuleCondition>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Applies all IEntityTypeConfiguration<T> classes in this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HearthDbContext).Assembly);
    }
}