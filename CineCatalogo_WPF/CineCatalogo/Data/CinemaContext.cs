using CineCatalogo.Models;
using Microsoft.EntityFrameworkCore;
namespace CineCatalogo.Data;
public class CinemaContext : DbContext
{
 public DbSet<Filme> Filmes => Set<Filme>();
 public DbSet<Usuario> Usuarios => Set<Usuario>();
 protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CineCatalogo", "cinema.db")}");
 protected override void OnModelCreating(ModelBuilder builder)
 {
  builder.Entity<Usuario>().HasIndex(x => x.Nome).IsUnique();
  builder.Entity<Filme>().HasIndex(x => x.Titulo);
 }
}
