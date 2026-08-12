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
    public DbSet<User> Users => Set<User>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Bank> Banks => Set<Bank>();
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<TransactionCategory> TransactionCategories => Set<TransactionCategory>();
    public DbSet<TransactionCategoryRule> TransactionCategoryRules => Set<TransactionCategoryRule>();
    public DbSet<BankCategory> BankCategories => Set<BankCategory>();
    public DbSet<BankCategoryRule> BankCategoryRules => Set<BankCategoryRule>();
    public DbSet<Rule> Rules => Set<Rule>();
    public DbSet<RuleCondition> RuleConditions => Set<RuleCondition>();
    public DbSet<Role> Roles => Set<Role>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Rule>()
            .HasDiscriminator<string>("RuleType")
            .HasValue<TransactionCategoryRule>("TransactionCategoryRule")
            .HasValue<BankCategoryRule>("BankCategoryRule");

        // Applies all IEntityTypeConfiguration<T> classes in this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HearthDbContext).Assembly);
    }
}