using System.ComponentModel.DataAnnotations;
namespace CineCatalogo.Models;
public class Filme
{
 public int Id { get; set; }
 [Required, MaxLength(150)] public string Titulo { get; set; } = "";
 [Required, MaxLength(80)] public string Genero { get; set; } = "";
 [Range(1888, 2100)] public int Ano { get; set; }
 [Range(1, 600)] public int Duracao { get; set; }
 public string Sinopse { get; set; } = "";
}
