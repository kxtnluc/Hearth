using Microsoft.EntityFrameworkCore;
using Hearth.Models;
namespace Hearth.Data;

public class HearthDbContext : DbContext
{
    private readonly string _dbPath;
    //private readonly IConfiguration _configuration;

    public HearthDbContext(string dbPath)
    {
        _dbPath = dbPath;
        //_configuration = config;
    }


    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<TransactionSyncRecord> TransactionSyncRecords { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryOrganizationRule> CategoryOrganizationRules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={_dbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Account)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.AccountId);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CategoryId);

        modelBuilder.Entity<CategoryOrganizationRule>()
            .HasOne(cor => cor.Category)
            .WithMany(c => c.OrganizationRules)
            .HasForeignKey(t => t.CategoryId);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Transactions)
            .WithOne(t => t.Category);

        //var entityType = self.FindEntityType(typeof(Transaction));
        //var prop = entityType.FindProperty(nameof(Transaction.Authorized_Datetime));
        //Console.WriteLine($"IsNullable = {prop.IsNullable}");
    }
}
