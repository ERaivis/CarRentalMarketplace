using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.SqlModels;

public partial class CarMarketplaceContext : DbContext
{
    public CarMarketplaceContext()
    {
    }

    public CarMarketplaceContext(DbContextOptions<CarMarketplaceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CarRentalOffer> CarRentalOffers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarRentalOffer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Vehicles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasColumnName("currency");
            entity.Property(e => e.ImageLink)
                .HasMaxLength(300)
                .HasColumnName("imageLink");
            entity.Property(e => e.LogoLink)
                .HasMaxLength(300)
                .HasColumnName("logoLink");
            entity.Property(e => e.Make)
                .HasMaxLength(20)
                .HasColumnName("make");
            entity.Property(e => e.Model)
                .HasMaxLength(20)
                .HasColumnName("model");
            entity.Property(e => e.RentalCost)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("rentalCost");
            entity.Property(e => e.Sipp)
                .HasMaxLength(10)
                .HasColumnName("sipp");
            entity.Property(e => e.SupplierId)
                .HasMaxLength(20)
                .HasColumnName("supplierId");
            entity.Property(e => e.VehicleId)
                .HasMaxLength(20)
                .HasColumnName("vehicleId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
