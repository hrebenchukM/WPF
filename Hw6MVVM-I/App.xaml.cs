using System.Collections.Generic;
using System.Windows;

namespace Hw6MVVM_I
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            List<Record> records = new List<Record>()
            {
                new Record("Мария М.Г", "Березовка,Победы,32", "+380995410272"),
                new Record("Мария Г.Г", "Одесса,Бугаевская,46а","+380972860462" ),
                new Record("Мария З.Г", "Одесса,Левитана,34", "+380*********")
            };

            MainWindow view = new MainWindow();
            MainWindowViewModel viewModel = new MainWindowViewModel(records);
            view.DataContext = viewModel;
            view.Show();
        }
    }

}
