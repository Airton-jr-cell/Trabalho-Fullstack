using System.Windows;
using CineCatalogo.Views;
namespace CineCatalogo;
public partial class MainWindow : Window
{
 public MainWindow() { InitializeComponent(); }
 private void Abrir(object view) { Dashboard.Visibility = Visibility.Collapsed; Conteudo.Visibility = Visibility.Visible; Area.Content = view; }
 private void Filmes_Click(object sender, RoutedEventArgs e) => Abrir(new FilmesView(false));
 private void Catalogo_Click(object sender, RoutedEventArgs e) => Abrir(new FilmesView(true));
 private void Estatisticas_Click(object sender, RoutedEventArgs e) => Abrir(new EstatisticasView());
 private void Sobre_Click(object sender, RoutedEventArgs e) => Abrir(new SobreView());
 private void Voltar_Click(object sender, RoutedEventArgs e) { Area.Content = null; Conteudo.Visibility = Visibility.Collapsed; Dashboard.Visibility = Visibility.Visible; }
 private void Sair_Click(object sender, RoutedEventArgs e) { var login = new LoginWindow(); Application.Current.MainWindow = login; login.Show(); Close(); }
}
