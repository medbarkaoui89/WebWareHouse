using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebWareHouse.Models;

namespace WebWareHouse.Data
{
    public partial class WareHouseContext : DbContext
    {
        public WareHouseContext()
        {
        }

        public WareHouseContext(DbContextOptions<WareHouseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Consumer> Consumers { get; set; } = null!;
        public virtual DbSet<Good> Goods { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<InvoicesList> InvoicesLists { get; set; } = null!;
        public virtual DbSet<Shipment> Shipments { get; set; } = null!;
        public virtual DbSet<ShipmentCo> ShipmentCos { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<Warehouse> Warehouses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=MOHAMED\\SQLEXPRESS;Initial Catalog=warehouse24;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consumer>(entity =>
            {
                entity.ToTable("Consumer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Good>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.From).HasColumnName("from");

                entity.Property(e => e.IdShip).HasColumnName("id_ship");

                entity.Property(e => e.To).HasColumnName("to");

                entity.Property(e => e.TypeInvo)
                    .HasMaxLength(50)
                    .HasColumnName("type_invo");

                entity.HasOne(d => d.FromNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.From)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_Supplier");

                entity.HasOne(d => d.From1)
                    .WithMany(p => p.InvoiceFrom1s)
                    .HasForeignKey(d => d.From)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_Warehouse1");

                entity.HasOne(d => d.IdShipNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.IdShip)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_Shipment");

                entity.HasOne(d => d.ToNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.To)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_Consumer");

                entity.HasOne(d => d.To1)
                    .WithMany(p => p.InvoiceTo1s)
                    .HasForeignKey(d => d.To)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_Warehouse");
            });

            modelBuilder.Entity<InvoicesList>(entity =>
            {
                entity.ToTable("InvoicesList");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdGoods).HasColumnName("id_goods");

                entity.Property(e => e.IdInvo).HasColumnName("id_invo");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.HasOne(d => d.IdGoodsNavigation)
                    .WithMany(p => p.InvoicesLists)
                    .HasForeignKey(d => d.IdGoods)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoicesList_Goods");

                entity.HasOne(d => d.IdInvoNavigation)
                    .WithMany(p => p.InvoicesLists)
                    .HasForeignKey(d => d.IdInvo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoicesList_Invoices");
            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.ToTable("Shipment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.IdCom).HasColumnName("id_com");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Tariff).HasColumnName("tariff");

                entity.Property(e => e.Time).HasColumnName("time");
            });

            modelBuilder.Entity<ShipmentCo>(entity =>
            {
                entity.ToTable("Shipment_co");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Photo).HasColumnName("photo");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.ToTable("Warehouse");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
