using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NovaTombola.Models
{
    public partial class LotoizvlacenjeContext : DbContext
    {
        public LotoizvlacenjeContext()
        {
        }

        public LotoizvlacenjeContext(DbContextOptions<LotoizvlacenjeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Igrac> Igraci { get; set; }
        public virtual DbSet<Kombinacija> Kombinacije { get; set; }
        public virtual DbSet<Pobednik> Pobednici { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Igrac>(entity =>
            {
                entity.HasIndex(e => e.Ime)
                    .HasName("UQ__Igrac__C497C905876CA785")
                    .IsUnique();
            });

            modelBuilder.Entity<Kombinacija>(entity =>
            {
                entity.HasOne(d => d.Igrac)
                    .WithMany(p => p.Kombinacije)
                    .HasForeignKey(d => d.IgracId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Kombinaci__Igrac__571DF1D5");
            });

            modelBuilder.Entity<Pobednik>(entity =>
            {
                entity.HasOne(d => d.PobednickaKombinacija)
                    .WithMany(p => p.Pobednici)
                    .HasForeignKey(d => d.PobednickaKombinacijaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pobednik__Pobedn__5DCAEF64");

                entity.HasOne(d => d.PobednickiIgrac)
                    .WithMany(p => p.Pobednici)
                    .HasForeignKey(d => d.PobednickiIgracId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pobednik__Pobedn__5CD6CB2B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}