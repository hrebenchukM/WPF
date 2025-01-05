using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hw1Task2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Get the current button.
            Button cmd = (Button)e.OriginalSource;//достали саму кнопку
            UserInput.Text += cmd.Content.ToString();
        }

        private void Button_Click_C(object sender, RoutedEventArgs e)
        {
           
            UserInput.Text = "";
        }
        private void Button_Click_Ok(object sender, RoutedEventArgs e)
        {
            if (UserInput.Text == "123456")
            {
                MessageBox.Show("Пароль верный!", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Неверный пароль!", "Error", MessageBoxButton.OK);
            }
        }
    }
}