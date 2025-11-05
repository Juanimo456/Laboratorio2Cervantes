using Microsoft.EntityFrameworkCore;
using Tiendita.Models;

namespace Tiendita.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Rol> Roles => Set<Rol>();
        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Marca> Marcas => Set<Marca>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Precision for Producto numeric fields
            modelBuilder.Entity<Producto>().Property(p => p.PrecioUnidad).HasPrecision(10, 2);
            modelBuilder.Entity<Producto>().Property(p => p.Calificacion).HasPrecision(3, 2);

            // Relaciones Producto
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.IdCategoria)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Marca)
                .WithMany(m => m.Productos)
                .HasForeignKey(p => p.IdMarca)
                .OnDelete(DeleteBehavior.Restrict);

            // Claves generadas por la DB
            modelBuilder.Entity<Categoria>()
                .Property(c => c.IdCategoria)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Marca>()
                .Property(m => m.IdMarca)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique(false);

            // Relación Usuario-Rol usando FK = IdRol (evita RolIdRol)
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}