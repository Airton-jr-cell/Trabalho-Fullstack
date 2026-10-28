using System.Windows;
using System.Windows.Controls;
using CineCatalogo.Models;
using CineCatalogo.Services;
namespace CineCatalogo.Views;
public partial class FilmesView : UserControl
{
 private readonly FilmeService service = new();
 private int id;
 public FilmesView(bool somenteConsulta)
 {
  InitializeComponent(); TituloPagina.Text = somenteConsulta ? "Consulta de filmes" : "Gerenciar filmes";
  Editor.Visibility = somenteConsulta ? Visibility.Collapsed : Visibility.Visible;
  Loaded += async (_, _) => await Carregar();
 }
 private async Task Carregar()
 {
  try { Tabela.ItemsSource = await service.ListarAsync(Pesquisa.Text); }
  catch (Exception ex) { MessageBox.Show(FilmeService.Mensagem(ex)); }
 }
 private async void Pesquisar_Click(object sender, RoutedEventArgs e) => await Carregar();
 private void Tabela_SelectionChanged(object sender, SelectionChangedEventArgs e)
 {
  if (Tabela.SelectedItem is not Filme f) return;
  id = f.Id; Titulo.Text = f.Titulo; Genero.Text = f.Genero; Ano.Text = f.Ano.ToString(); Duracao.Text = f.Duracao.ToString(); Sinopse.Text = f.Sinopse;
 }
 private void Limpar() { id = 0; Tabela.SelectedItem = null; Titulo.Clear(); Genero.Clear(); Ano.Clear(); Duracao.Clear(); Sinopse.Clear(); }
 private void Novo_Click(object sender, RoutedEventArgs e) => Limpar();
 private async void Salvar_Click(object sender, RoutedEventArgs e)
 {
  if (!int.TryParse(Ano.Text, out var ano) || !int.TryParse(Duracao.Text, out var duracao)) { MessageBox.Show("Ano e duração devem ser números inteiros."); return; }
  try
  {
   await service.SalvarAsync(new Filme { Id = id, Titulo = Titulo.Text.Trim(), Genero = Genero.Text.Trim(), Ano = ano, Duracao = duracao, Sinopse = Sinopse.Text.Trim() });
   Limpar(); await Carregar(); MessageBox.Show("Filme salvo com sucesso.");
  }
  catch (Exception ex) { MessageBox.Show(FilmeService.Mensagem(ex)); }
 }
 private async void Excluir_Click(object sender, RoutedEventArgs e)
 {
  if (id == 0) { MessageBox.Show("Selecione um filme."); return; }
  if (MessageBox.Show("Deseja excluir o filme selecionado?", "Confirmar", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
  try { await service.ExcluirAsync(id); Limpar(); await Carregar(); }
  catch (Exception ex) { MessageBox.Show(FilmeService.Mensagem(ex)); }
 }
}
