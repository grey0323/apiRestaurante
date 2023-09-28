using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiRestaurante.Models;

public partial class RestaurantContext : DbContext
{
    public RestaurantContext()
    {
    }

    public RestaurantContext(DbContextOptions<RestaurantContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ingrediente> Ingredientes { get; set; }

    public virtual DbSet<IngredientePlato> IngredientePlatos { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<OrdenPlato> OrdenPlatos { get; set; }

    public virtual DbSet<Ordene> Ordenes { get; set; }

    public virtual DbSet<Plato> Platos { get; set; }

    public virtual DbSet<TblEstado> TblEstados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=True;Initial Catalog=Restaurant;TrustServerCertificate=True;Data Source=DESKTOP-NN49812");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingrediente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ingredie__3213E83F9EBC76DF");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<IngredientePlato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ingredie__3213E83FDA6FF82E");

            entity.ToTable("IngredientePlato");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idingrediente).HasColumnName("idingrediente");
            entity.Property(e => e.Idplato).HasColumnName("idplato");

            entity.HasOne(d => d.IdingredienteNavigation).WithMany(p => p.IngredientePlatos)
                .HasForeignKey(d => d.Idingrediente)
                .HasConstraintName("FK__Ingredien__iding__6A30C649");

            entity.HasOne(d => d.IdplatoNavigation).WithMany(p => p.IngredientePlatos)
                .HasForeignKey(d => d.Idplato)
                .HasConstraintName("FK__Ingredien__idpla__693CA210");
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mesa__3213E83FD3B324D2");

            entity.ToTable("Mesa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CantidadPerson).HasColumnName("cantidadPerson");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdEstado).HasColumnName("idEstado");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Mesas)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK__Mesa__idEstado__02FC7413");
        });

        modelBuilder.Entity<OrdenPlato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrdenPla__3213E83F805EFEE0");

            entity.ToTable("OrdenPlato");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idorden).HasColumnName("idorden");
            entity.Property(e => e.Idplato).HasColumnName("idplato");

            entity.HasOne(d => d.IdordenNavigation).WithMany(p => p.OrdenPlatos)
                .HasForeignKey(d => d.Idorden)
                .HasConstraintName("FK__OrdenPlat__idord__72C60C4A");

            entity.HasOne(d => d.IdplatoNavigation).WithMany(p => p.OrdenPlatos)
                .HasForeignKey(d => d.Idplato)
                .HasConstraintName("FK__OrdenPlat__idpla__71D1E811");
        });

        modelBuilder.Entity<Ordene>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ordenes__3213E83FF0BB5016");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.MesaPertenece).HasColumnName("mesaPertenece");
            entity.Property(e => e.Subtotal).HasColumnName("subtotal");

            entity.HasOne(d => d.MesaPerteneceNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.MesaPertenece)
                .HasConstraintName("FK__Ordenes__mesaPer__6EF57B66");
        });

        modelBuilder.Entity<Plato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platos__3213E83FB48A93A3");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("categoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumeroPersonas).HasColumnName("numeroPersonas");
            entity.Property(e => e.Precio).HasColumnName("precio");
        });

        modelBuilder.Entity<TblEstado>(entity =>
        {
            entity.ToTable("tblEstado");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("estado");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
