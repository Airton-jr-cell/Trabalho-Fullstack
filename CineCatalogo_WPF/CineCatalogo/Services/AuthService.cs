using System.Security.Cryptography;
using CineCatalogo.Data;
using CineCatalogo.Models;
using Microsoft.EntityFrameworkCore;
namespace CineCatalogo.Services;
public class AuthService
{
 private static string Hash(string senha, byte[] salt) => Convert.ToBase64String(Rfc2898DeriveBytes.Pbkdf2(senha, salt, 120000, HashAlgorithmName.SHA256, 32));
 public async Task InicializarAsync()
 {
  var pasta = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CineCatalogo");
  System.IO.Directory.CreateDirectory(pasta);
  await using var db = new CinemaContext();
  await db.Database.EnsureCreatedAsync();
  if (!await db.Usuarios.AnyAsync())
  {
   var salt = RandomNumberGenerator.GetBytes(16);
   db.Usuarios.Add(new Usuario { Nome = "admin", Salt = Convert.ToBase64String(salt), SenhaHash = Hash("admin123", salt) });
   await db.SaveChangesAsync();
  }
 }
 public async Task<bool> EntrarAsync(string nome, string senha)
 {
  await using var db = new CinemaContext();
  var usuario = await db.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Nome == nome);
  if (usuario == null) return false;
  var esperado = Convert.FromBase64String(usuario.SenhaHash);
  var recebido = Convert.FromBase64String(Hash(senha, Convert.FromBase64String(usuario.Salt)));
  return CryptographicOperations.FixedTimeEquals(esperado, recebido);
 }
}
