using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.IO;
using System.Windows;

#nullable disable

namespace ProjektKsiazkaTelefoniczna
{
    public partial class DataTelContext : DbContext
    {
        public DataTelContext()
        {
        }

        public virtual DbSet<Osoby> Osoby { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Directory.GetCurrentDirectory()}\DataTel.mdf;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Osoby>(entity =>
            {
                entity.ToTable("Osoby");

                entity.Property(e => e.OsobyId).HasColumnName("OsobyID");

                entity.Property(e => e.Adres)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("adres");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Imie)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("imie");

                entity.Property(e => e.Nazwisko)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nazwisko");

                entity.Property(e => e.NrTel)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nrTel");
            });
        }
    }
}