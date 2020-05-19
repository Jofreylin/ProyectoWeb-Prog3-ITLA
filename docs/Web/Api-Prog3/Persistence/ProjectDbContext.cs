using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public partial class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Ciudad> Ciudad { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<TipoTrabajo> TipoTrabajo { get; set; }
        public virtual DbSet<UserAdmin> UserAdmin { get; set; }
        public virtual DbSet<UserPoster> UserPoster { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasIndex(e => e.Nombre)
                    .HasName("UQ__CATEGORI__75E3EFCF7C426275")
                    .IsUnique();

                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.HasOne(d => d.NombrePaisNavigation)
                    .WithMany(p => p.Ciudad)
                    .HasForeignKey(d => d.NombrePais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CIUDAD_PAIS");
            });

            modelBuilder.Entity<Pais>(entity =>
            {
                entity.HasIndex(e => e.Nombre)
                    .HasName("UQ__PAIS__75E3EFCFD08B0375")
                    .IsUnique();

                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.DireccionUrl).IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NombreCalle).IsUnicode(false);

                entity.Property(e => e.NombrePosicion).IsUnicode(false);

                entity.HasOne(d => d.NombreCategoriaNavigation)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.NombreCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POST_CATEGORIA");

                entity.HasOne(d => d.NombreCiudadNavigation)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.NombreCiudad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POST_CIUDAD");

                entity.HasOne(d => d.NombrePaisNavigation)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.NombrePais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POST_PAIS");

                entity.HasOne(d => d.NombreTipoTrabajoNavigation)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.NombreTipoTrabajo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POST_TRABAJO");
            });

            modelBuilder.Entity<TipoTrabajo>(entity =>
            {
                entity.HasIndex(e => e.Nombre)
                    .HasName("UQ__TIPO_TRA__75E3EFCFA3F31137")
                    .IsUnique();

                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<UserAdmin>(entity =>
            {
                entity.HasIndex(e => e.Usuario)
                    .HasName("UQ__USER_ADM__E3237CF72DA8FDC4")
                    .IsUnique();

                entity.Property(e => e.Contraseña).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.Property(e => e.Usuario).IsUnicode(false);
            });

            modelBuilder.Entity<UserPoster>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__USER_POS__A9D10534E7793F11")
                    .IsUnique();

                entity.Property(e => e.Contra).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NombreCalle).IsUnicode(false);

                entity.Property(e => e.NombreEmpresa).IsUnicode(false);

                entity.HasOne(d => d.NombreCiudadNavigation)
                    .WithMany(p => p.UserPoster)
                    .HasForeignKey(d => d.NombreCiudad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POSTER_CIUDAD");

                entity.HasOne(d => d.NombrePaisNavigation)
                    .WithMany(p => p.UserPoster)
                    .HasForeignKey(d => d.NombrePais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POSTER_PAIS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
