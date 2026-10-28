using System.Windows;
using System.Windows.Controls;
using CineCatalogo.Services;
namespace CineCatalogo.Views;
public partial class EstatisticasView : UserControl
{
 public EstatisticasView() { InitializeComponent(); Loaded += Carregar; }
 private async void Carregar(object sender, RoutedEventArgs e)
 {
  try
  {
   var filmes = await new FilmeService().ListarAsync();
   Total.Text = $"Filmes cadastrados: {filmes.Count}";
   Generos.Text = $"Gêneros diferentes: {filmes.Select(f => f.Genero.ToUpperInvariant()).Distinct().Count()}";
   Duracao.Text = $"Duração média: {(filmes.Count == 0 ? 0 : filmes.Average(f => f.Duracao)):F0} minutos";
  }
  catch (Exception ex) { MessageBox.Show(FilmeService.Mensagem(ex)); }
 }
}
