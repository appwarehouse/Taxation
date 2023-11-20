using System.Collections.Generic;
using System.Reflection.Emit;
using Taxation.Models;
using Microsoft.EntityFrameworkCore;

namespace Taxation.Data
{
    public class TaxationDBContext : DbContext

    {
        public DbSet<TaxCalculationRecord> TaxCalculations { get; set; }

        public TaxationDBContext(DbContextOptions<TaxationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxCalculationRecord>(entity =>
            {
                entity.ToTable("TaxCalculations"); // Set the table name
                entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1000, 1);
                entity.Property(e => e.PostalCode).HasMaxLength(255).IsRequired();
                entity.Property(e => e.AnnualIncome).HasColumnType("decimal(18, 2)").IsRequired();
                entity.Property(e => e.CalculatedTax).HasColumnType("decimal(18, 2)").IsRequired();
                entity.Property(e => e.CalculationDate).IsRequired();
            });

            // Additional configurations if needed...

            base.OnModelCreating(modelBuilder);
        }
    }
}