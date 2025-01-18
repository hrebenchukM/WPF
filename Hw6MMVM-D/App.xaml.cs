using System.Configuration;
using System.Data;
using System.Windows;


namespace Hw6MMVM_D
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {

            MainWindow view = new MainWindow();
            MainWindowViewModel viewModel = new MainWindowViewModel();
            view.DataContext = viewModel;
            view.Show();
        }
    }

}
