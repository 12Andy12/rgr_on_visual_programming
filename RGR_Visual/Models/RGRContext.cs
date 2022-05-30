using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RGR_Visual.Models
{
    public partial class RGRContext : DbContext
    {
        public RGRContext()
        {
        }

        public RGRContext(DbContextOptions<RGRContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Horse> Horses { get; set; } = null!;
        public virtual DbSet<Jockey> Jockeys { get; set; } = null!;
        public virtual DbSet<Owner> Owners { get; set; } = null!;
        public virtual DbSet<Race> Races { get; set; } = null!;
        public virtual DbSet<Racecourse> Racecourses { get; set; } = null!;
        public virtual DbSet<Result> Results { get; set; } = null!;
        public virtual DbSet<Trainer> Trainers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Data Source=..\\..\\..\\..\\..\\RGR.db");
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Horse>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("Horse");

                entity.Property(e => e.Name).HasColumnType("STRING");

                entity.Property(e => e.Gender).HasColumnType("STRING");

                entity.Property(e => e.Owner).HasColumnType("STRING");

                entity.Property(e => e.Trainer).HasColumnType("STRING");

                entity.Property(e => e.Weight).HasColumnType("STRING");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.Horses)
                    .HasForeignKey(d => d.Owner)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.TrainerNavigation)
                    .WithMany(p => p.Horses)
                    .HasForeignKey(d => d.Trainer)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Jockey>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("Jockey");

                entity.Property(e => e.Name).HasColumnType("STRING");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("Owner");

                entity.Property(e => e.Name).HasColumnType("STRING");
            });

            modelBuilder.Entity<Race>(entity =>
            {
                entity.HasKey(e => new { e.Date, e.Number, e.Racecourse });

                entity.ToTable("Race");

                entity.Property(e => e.Date).HasColumnType("STRING");

                entity.Property(e => e.Racecourse).HasColumnType("STRING");

                entity.Property(e => e.Distance).HasColumnType("STRING");

                entity.Property(e => e.Type).HasColumnType("STRING");

                entity.HasOne(d => d.RacecourseNavigation)
                    .WithMany(p => p.Races)
                    .HasForeignKey(d => d.Racecourse)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Racecourse>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("Racecourse");

                entity.Property(e => e.Name).HasColumnType("STRING");
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.HasKey(e => new { e.Horse, e.Date, e.RaceNumber, e.Racecourse });

                entity.Property(e => e.Date).HasColumnType("STRING");

                entity.Property(e => e.DistanceBetween).HasColumnType("STRING");

                entity.Property(e => e.FinishPosition).HasColumnName("FinishPosition");

                entity.Property(e => e.Horse).HasColumnType("STRING");

                entity.Property(e => e.Jockey).HasColumnType("STRING");

                entity.Property(e => e.RaceNumber).HasColumnName("RaceNumber");

                entity.Property(e => e.Racecourse).HasColumnType("STRING");

                entity.HasOne(d => d.HorseNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Horse)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.JockeyNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Jockey)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Race)
                    .WithMany()
                    .HasForeignKey(d => new { d.Date, d.RaceNumber, d.Racecourse })
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("Trainer");

                entity.Property(e => e.Name).HasColumnType("STRING");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
