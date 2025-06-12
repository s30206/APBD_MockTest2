using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APBD_MockTest2;

public partial class TournamentContext : DbContext
{
    public TournamentContext()
    {
    }

    public TournamentContext(DbContextOptions<TournamentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarManufacturer> CarManufacturers { get; set; }

    public virtual DbSet<Competition> Competitions { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<DriverCompetition> DriverCompetitions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Car_pk");

            entity.ToTable("Car");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CarManufacturerId).HasColumnName("CarManufacturer_ID");
            entity.Property(e => e.ModelName)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.CarManufacturer).WithMany(p => p.Cars)
                .HasForeignKey(d => d.CarManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Car_CarManufacturer");
        });

        modelBuilder.Entity<CarManufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CarManufacturer_pk");

            entity.ToTable("CarManufacturer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Competition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Competition_pk");

            entity.ToTable("Competition");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Driver_pk");

            entity.ToTable("Driver");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CarId).HasColumnName("Car_ID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Car).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Driver_Car");
        });

        modelBuilder.Entity<DriverCompetition>(entity =>
        {
            entity.HasKey(e => new { e.DriverId, e.CompetitionId }).HasName("DriverCompetition_pk");

            entity.ToTable("DriverCompetition");

            entity.Property(e => e.DriverId).HasColumnName("Driver_ID");
            entity.Property(e => e.CompetitionId).HasColumnName("Competition_ID");

            entity.HasOne(d => d.Competition).WithMany(p => p.DriverCompetitions)
                .HasForeignKey(d => d.CompetitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_6_Competition");

            entity.HasOne(d => d.Driver).WithMany(p => p.DriverCompetitions)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_6_Driver");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
