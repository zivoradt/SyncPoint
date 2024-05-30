using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SyncPointBack.Model.Excel;
using SyncPointBack.Persistance.Interface;
using System.Data.Common;

namespace SyncPointBack.Persistance
{
    public class SyncPointDb : DbContext
    {
        private readonly IConfiguration _configuration;

        private readonly int tryToConnectToDb = 1;

        private int countConnectionAttempts = 0;

        private readonly TimeSpan secondsAfterTryToConnectToDb = TimeSpan.FromSeconds(5);

        private ILogger<SyncPointDb> _logger;

        public SyncPointDb(DbContextOptions<SyncPointDb> options, IConfiguration configuration, ILogger<SyncPointDb> logger) : base(options)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public DbSet<ExcelRecord> ExcelRecords { get; set; }

        public DbSet<StaticPageCreation> StaticPageCreation { get; set; }

        public DbSet<StaticPageModification> StaticPageModification { get; set; }

        public DbSet<PDRegistration> PDRegistration { get; set; }

        public DbSet<PDModification> PDModification { get; set; }

        public DbSet<PIM> PIM { get; set; }

        public DbSet<GNB> GNB { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StaticPageCreation>()
               .HasOne(s => s.ExcelRecord)
               .WithOne(e => e.StaticPageCreation)
               .HasForeignKey<StaticPageCreation>(s => s.ExcelRecordId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StaticPageModification>()
                    .HasOne(s => s.ExcelRecord)
                    .WithOne(e => e.StaticPageModification)
                    .HasForeignKey<StaticPageModification>(s => s.ExcelRecordId)
                    .OnDelete(DeleteBehavior.Cascade);

            // Configure cascading delete for PDRegistration
            modelBuilder.Entity<PDRegistration>()
                .HasOne(p => p.ExcelRecord)
                .WithOne(e => e.PDRegistration)
                .HasForeignKey<PDRegistration>(p => p.ExcelRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure cascading delete for PDModification
            modelBuilder.Entity<PDModification>()
                .HasOne(p => p.ExcelRecord)
                .WithOne(e => e.PDModification)
                .HasForeignKey<PDModification>(p => p.ExcelRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure cascading delete for PIM
            modelBuilder.Entity<PIM>()
                .HasOne(p => p.ExcelRecord)
                .WithOne(e => e.PIM)
                .HasForeignKey<PIM>(p => p.ExcelRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure cascading delete for GNB
            modelBuilder.Entity<GNB>()
                .HasOne(g => g.ExcelRecord)
                .WithOne(e => e.GNB)
                .HasForeignKey<GNB>(g => g.ExcelRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public async Task<bool> IsDatabaseConnectedAsync()
        {
            try
            {
                // Execute a simple SQL query to check database connectivity
                var _ = await Database.ExecuteSqlRawAsync("SELECT 1");

                if (_ != null)
                {
                    _logger.LogInformation("Database is pinned.");
                }
                _logger.LogInformation("Database is sucessffuly connected.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error connecting to the database.");
                _logger.LogInformation($"Try to reconnect after {secondsAfterTryToConnectToDb} seconds.");

                return await TryToReconnectAsync();
            }
        }

        private async Task<bool> TryToReconnectAsync()
        {
            if (countConnectionAttempts < tryToConnectToDb)
            {
                await Task.Delay(secondsAfterTryToConnectToDb);

                countConnectionAttempts++;

                return await IsDatabaseConnectedAsync();
            }
            else
            {
                return false;
            }
        }
    }
}