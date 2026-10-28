using System.Windows;
using CineCatalogo.Services;
namespace CineCatalogo;
public partial class LoginWindow : Window
{
 private readonly AuthService auth = new();
 public LoginWindow() { InitializeComponent(); Loaded += Iniciar; }
 private async void Iniciar(object sender, RoutedEventArgs e)
 {
  try { await auth.InicializarAsync(); }
  catch (Exception ex) { Status.Text = "Falha ao iniciar: " + ex.Message; }
 }
 private async void Entrar_Click(object sender, RoutedEventArgs e)
 {
  try
  {
   if (!await auth.EntrarAsync(UsuarioBox.Text.Trim(), SenhaBox.Password)) { Status.Text = "Usuário ou senha incorretos."; return; }
   var janela = new MainWindow(); Application.Current.MainWindow = janela; janela.Show(); Close();
  }
  catch (Exception ex) { Status.Text = "Falha no acesso: " + ex.Message; }
 }
}
