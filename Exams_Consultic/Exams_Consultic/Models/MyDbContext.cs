using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Exams_Consultic.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }

    public virtual DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-G1NBAML;Database=Exams_Consultic;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CurrencyCode).HasName("PK__Currency__408426BE465511F5");

            entity.ToTable("Currency");

            entity.Property(e => e.CurrencyCode).HasMaxLength(3);
            entity.Property(e => e.CurrencyName).HasMaxLength(15);
        });

        modelBuilder.Entity<PurchaseOrderHeader>(entity =>
        {
            entity.HasKey(e => e.PurchId).HasName("PK__Purchase__25A0C58E6215F7FD");

            entity.ToTable("PurchaseOrderHeader");

            entity.Property(e => e.PurchId).HasMaxLength(20);
            entity.Property(e => e.CurrencyCode).HasMaxLength(3);
            entity.Property(e => e.Vendor).HasMaxLength(20);

            entity.HasOne(d => d.CurrencyCodeNavigation).WithMany(p => p.PurchaseOrderHeaders)
                .HasForeignKey(d => d.CurrencyCode)
                .HasConstraintName("FK__PurchaseO__Curre__4F7CD00D");

            entity.HasOne(d => d.VendorNavigation).WithMany(p => p.PurchaseOrderHeaders)
                .HasForeignKey(d => d.Vendor)
                .HasConstraintName("FK__PurchaseO__Vendo__4E88ABD4");
        });

        modelBuilder.Entity<PurchaseOrderLine>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Amount)
                .HasComputedColumnSql("([Qty]*[UnitPrice])", true)
                .HasColumnType("decimal(37, 4)");
            entity.Property(e => e.ItemId).HasMaxLength(30);
            entity.Property(e => e.PurchId).HasMaxLength(20);
            entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Purch).WithMany()
                .HasForeignKey(d => d.PurchId)
                .HasConstraintName("FK__PurchaseO__Purch__5165187F");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__Vendor__FC8618F3B712E01E");

            entity.ToTable("Vendor");

            entity.Property(e => e.VendorId).HasMaxLength(20);
            entity.Property(e => e.VendorName).HasMaxLength(160);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
