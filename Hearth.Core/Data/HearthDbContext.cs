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
    #region Base
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Rule> Rules => Set<Rule>();
    public DbSet<RuleCondition> RuleConditions => Set<RuleCondition>();
    #endregion
    #region Finance
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Bank> Banks => Set<Bank>();
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<TransactionCategory> TransactionCategories => Set<TransactionCategory>();
    public DbSet<TransactionCategoryRule> TransactionCategoryRules => Set<TransactionCategoryRule>();
    public DbSet<TransactionSyncRecord> TransactionSyncRecords => Set<TransactionSyncRecord>();
    public DbSet<BankCategory> BankCategories => Set<BankCategory>();
    public DbSet<BankCategoryRule> BankCategoryRules => Set<BankCategoryRule>();
    #endregion

    // Custom EFC Building stuff.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Rule>()
            .HasDiscriminator<string>("RuleType")
            .HasValue<TransactionCategoryRule>("TransactionCategoryRule")
            .HasValue<BankCategoryRule>("BankCategoryRule");

        modelBuilder.Entity<Account>(builder =>
        {
            builder.OwnsOne(a => a.Balances, bal =>
            {
                bal.Property(b => b.Available).HasColumnName("Balances_Available");
                bal.Property(b => b.Current).HasColumnName("Balances_Current");
                bal.Property(b => b.Limit).HasColumnName("Balances_Limit");
                bal.Property(b => b.Iso_Currency_Code).HasColumnName("Balances_Iso_Currency_Code");
                bal.Property(b => b.Unofficial_Currency_Code).HasColumnName("Balances_Unofficial_Currency_Code");
            });
        });

        modelBuilder.Entity<Transaction>(builder =>
        {
            builder.OwnsOne(t => t.Location, loc =>
            {
                loc.Property(p => p.Address).HasColumnName("Location_Address");
                loc.Property(p => p.City).HasColumnName("Location_City");
                loc.Property(p => p.Country).HasColumnName("Location_Country");
                loc.Property(p => p.Lat).HasColumnName("Location_Lat");
                loc.Property(p => p.Lon).HasColumnName("Location_Lon");
                loc.Property(p => p.Postal_Code).HasColumnName("Location_Postal_Code");
                loc.Property(p => p.Region).HasColumnName("Location_Region");
                loc.Property(p => p.Store_Number).HasColumnName("Location_Store_Number");
            });


            builder.OwnsOne(t => t.Payment_Meta, pm =>
            {
                pm.Property(p => p.By_Order_Of).HasColumnName("PaymentMeta_By_Order_Of");
                pm.Property(p => p.Payee).HasColumnName("PaymentMeta_Payee");
                pm.Property(p => p.Payer).HasColumnName("PaymentMeta_Payer");
                pm.Property(p => p.Payment_Method).HasColumnName("PaymentMeta_Payment_Method");
                pm.Property(p => p.Payment_Processor).HasColumnName("PaymentMeta_Payment_Processor");
                pm.Property(p => p.Ppd_Id).HasColumnName("PaymentMeta_Ppd_Id");
                pm.Property(p => p.Reason).HasColumnName("PaymentMeta_Reason");
                pm.Property(p => p.Reference_Number).HasColumnName("PaymentMeta_Reference_Number");
            });

            builder.OwnsMany(t => t.Counterparties, cp =>
            {
                cp.WithOwner().HasForeignKey("TransactionId");
                cp.Property<int>("Id");
                cp.HasKey("Id");

                cp.Property(p => p.Name).HasColumnName("Name");
                cp.Property(p => p.Type).HasColumnName("Type");
                cp.Property(p => p.Logo_Url).HasColumnName("Logo_Url");
                cp.Property(p => p.Website).HasColumnName("Website");
                cp.Property(p => p.Entity_Id).HasColumnName("Entity_Id");
                cp.Property(p => p.Confidence_Level).HasColumnName("Confidence_Level");

                cp.ToTable("TransactionCounterparties");
            });

            builder.OwnsOne(t => t.Personal_Finance_Category, pfc =>
            {
                pfc.Property(p => p.Primary).HasColumnName("PersonalFinanceCategory_Primary");
                pfc.Property(p => p.Primary).HasColumnName("PersonalFinanceCategory_Detailed");
                pfc.Property(p => p.Primary).HasColumnName("PersonalFinanceCategory_Confidence_Level");
            });
        });

        // Applies all IEntityTypeConfiguration<T> classes in this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HearthDbContext).Assembly);
    }
}