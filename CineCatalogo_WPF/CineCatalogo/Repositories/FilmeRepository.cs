using CineCatalogo.Data;
using CineCatalogo.Models;
using Microsoft.EntityFrameworkCore;
namespace CineCatalogo.Repositories;
public class FilmeRepository
{
 public async Task<List<Filme>> ListarAsync(string pesquisa = "")
 {
  await using var db = new CinemaContext();
  var query = db.Filmes.AsNoTracking().AsQueryable();
  if (!string.IsNullOrWhiteSpace(pesquisa)) query = query.Where(f => EF.Functions.Like(f.Titulo, "%" + pesquisa + "%") || EF.Functions.Like(f.Genero, "%" + pesquisa + "%"));
  return await query.OrderBy(f => f.Titulo).ToListAsync();
 }
 public async Task SalvarAsync(Filme filme)
 {
  await using var db = new CinemaContext();
  if (filme.Id == 0) db.Filmes.Add(filme);
  else
  {
   var existente = await db.Filmes.FindAsync(filme.Id) ?? throw new KeyNotFoundException("Filme não encontrado.");
   existente.Titulo = filme.Titulo; existente.Genero = filme.Genero; existente.Ano = filme.Ano; existente.Duracao = filme.Duracao; existente.Sinopse = filme.Sinopse;
  }
  await db.SaveChangesAsync();
 }
 public async Task ExcluirAsync(int id)
 {
  await using var db = new CinemaContext();
  var filme = await db.Filmes.FindAsync(id) ?? throw new KeyNotFoundException("Filme não encontrado.");
  db.Filmes.Remove(filme);
  await db.SaveChangesAsync();
 }
}
