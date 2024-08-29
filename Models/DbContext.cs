using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StatusFlowAPI.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Transition> Transitions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-7QELS7G;Database=StatusFlow;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Statuses__C8EE204374A061C4").HasAnnotation("SqlServer:Identity", "1, 1"); ;

            entity.HasIndex(e => e.Name, "UQ__Statuses__737584F6C6A8808D").IsUnique();

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.IsInitial).HasDefaultValue(false);
            entity.Property(e => e.IsOrphan).HasDefaultValue(false);
            entity.Property(e => e.IsFinal).HasDefaultValue(false);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Transition>(entity =>
        {
            entity.HasKey(e => e.TransitionId).HasName("PK__Transiti__54F04847DB5546A4").HasAnnotation("SqlServer:Identity", "1, 1");

            entity.HasIndex(e => e.Name, "UQ__Transiti__737584F6ECF8E53B").IsUnique();

            entity.Property(e => e.TransitionId).HasColumnName("TransitionID");
            entity.Property(e => e.FromStatusId).HasColumnName("FromStatusID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ToStatusId).HasColumnName("ToStatusID");

            //entity.HasOne(d => d.FromStatus).WithMany(p => p.TransitionFromStatuses)
            //    .HasForeignKey(d => d.FromStatusId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__Transitio__FromS__3C69FB99");

            //entity.HasOne(d => d.ToStatus).WithMany(p => p.TransitionToStatuses)
            //    .HasForeignKey(d => d.ToStatusId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__Transitio__ToSta__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
