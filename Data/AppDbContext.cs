
namespace BudgetService.Data;

using BudgetService.Properties.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Budget> Budgets   { get; set; }

        private readonly IConfiguration _configuration;
        private readonly ILogger<AppDbContext> _logger;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IConfiguration configuration,
            ILogger<AppDbContext> logger)
            : base(options)
        {
            _configuration = configuration;
            _logger = logger;

            _logger.LogInformation("AppDbContext initialized.");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _logger.LogInformation("Configuring AppDbContext for Azure SQL...");

            if (!optionsBuilder.IsConfigured)
            {
                // Assumes you have a "DefaultConnection" in appsettings.json pointing to your Azure SQL
                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlServer(connectionString, sqlOpts =>
                {
                    // Enable retry on transient failures (e.g. brief network blips)
                    sqlOpts.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);
                });

                _logger.LogInformation("SQL Server (Azure) configured with DefaultConnection.");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Optional: Fluent API configuration if you want to enforce the unique constraint
            modelBuilder.Entity<Budget>()
                .HasIndex(b => new { b.AccountId, b.PeriodStart, b.PeriodEnd })
                .IsUnique();

            // Set up the relationship
            modelBuilder.Entity<Budget>()
                .HasOne(b => b.Account)
                .WithMany(a => a.Budgets)
                .HasForeignKey(b => b.AccountId);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            _logger.LogInformation("Saving changes to the database...");
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Saving changes to the database (async)...");
            return base.SaveChangesAsync(cancellationToken);
        }
    }

