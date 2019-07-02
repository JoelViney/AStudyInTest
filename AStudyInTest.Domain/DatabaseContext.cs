using AStudyInTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace AStudyInTest.Domain
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Product> Products { get; set; }

        #region Constructors...

        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                var builder = new ConfigurationBuilder().AddJsonFile("config.json");
                var configuration = builder.Build();
                var connectionString = configuration["ConnectionStrings:DefaultConnection"];

                optionsBuilder.UseSqlServer(connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RemoveCascadeDeletes(modelBuilder);

            // Set Precision
            modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnType("decimal(18, 2)");

            // Create Indexes
            modelBuilder.Entity<Product>().HasIndex(x => new { x.Name }).IsUnique();

            base.OnModelCreating(modelBuilder);
        }


        /// <summary>Removes Cascade Deletes</summary>
        public static void RemoveCascadeDeletes(ModelBuilder modelBuilder)
        {
            // Note: This is not used.
            foreach (IMutableForeignKey relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Allow some Cascade Deletes....

            // Order            
            // modelBuilder.Entity<WidgetLine>().HasOne(e => e.Widget).WithMany(x => x.Lines).Metadata.DeleteBehavior = DeleteBehavior.Cascade;
        }
    }
}
