using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace OrderDetailsMaintenance.Models.DataLayer;

public partial class NorthwindContext : DbContext
{
    public NorthwindContext()
    {
    }

    public NorthwindContext(DbContextOptions<NorthwindContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }
    // Nadia Cowins
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString
            );
        }
    }


    // Nadia Cowins
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.CustomerId).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
