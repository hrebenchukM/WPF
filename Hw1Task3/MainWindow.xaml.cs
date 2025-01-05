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

namespace Hw1Task3
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
        
            try
            {
                
                double a00 = double.Parse(UserInput00.Text);
                double a01 = double.Parse(UserInput01.Text);
                double a02 = double.Parse(UserInput02.Text);
                double a10 = double.Parse(UserInput10.Text);
                double a11 = double.Parse(UserInput11.Text);
                double a12 = double.Parse(UserInput12.Text);
                double a20 = double.Parse(UserInput20.Text);
                double a21 = double.Parse(UserInput21.Text);
                double a22 = double.Parse(UserInput22.Text);

                double res = a00 * (a11 * a22 - a12 * a21) -
                               a01 * (a10 * a22 - a12 * a20) +
                               a02 * (a10 * a21 - a11 * a20);

                UserAnswer.Text = res.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
    }
}