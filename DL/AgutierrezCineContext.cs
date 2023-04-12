using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class AgutierrezCineContext : DbContext
{
    public AgutierrezCineContext()
    {
    }

    public AgutierrezCineContext(DbContextOptions<AgutierrezCineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cine> Cines { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Zona> Zonas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= AGutierrezCine; User ID=sa; TrustServerCertificate=True; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cine>(entity =>
        {
            entity.HasKey(e => e.IdCine).HasName("PK__Cine__394B724BADCA0DDA");

            entity.ToTable("Cine");

            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Latitud).HasColumnType("decimal(8, 6)");
            entity.Property(e => e.Longitud).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Venta).HasColumnType("decimal(10, 3)");

            entity.HasOne(d => d.ZonaNavigation).WithMany(p => p.Cines)
                .HasForeignKey(d => d.Zona)
                .HasConstraintName("FK__Cine__Zona__1A14E395");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF973FD6B4BB");

            entity.ToTable("Usuario");

            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Zona>(entity =>
        {
            entity.HasKey(e => e.IdZona).HasName("PK__Zona__F631C12D298015FE");

            entity.ToTable("Zona");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
