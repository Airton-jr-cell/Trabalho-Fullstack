using CineCatalogo.Models;
using CineCatalogo.Repositories;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace CineCatalogo.Services;
public class FilmeService
{
 private readonly FilmeRepository repository = new();
 public Task<List<Filme>> ListarAsync(string pesquisa = "") => repository.ListarAsync(pesquisa.Trim());
 public async Task SalvarAsync(Filme filme)
 {
  Validator.ValidateObject(filme, new ValidationContext(filme), true);
  if (string.IsNullOrWhiteSpace(filme.Titulo) || string.IsNullOrWhiteSpace(filme.Genero)) throw new ValidationException("Preencha título e gênero.");
  await repository.SalvarAsync(filme);
 }
 public Task ExcluirAsync(int id) => repository.ExcluirAsync(id);
 public static string Mensagem(Exception ex) => ex switch
 {
  ValidationException => ex.Message,
  DbUpdateException => "Não foi possível salvar os dados no banco.",
  KeyNotFoundException => ex.Message,
  _ => "Não foi possível concluir a operação: " + ex.Message
 };
}
