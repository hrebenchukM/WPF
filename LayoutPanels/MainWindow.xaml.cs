using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//используется рефлексия
namespace LayoutPanels
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
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // Get the current button.
            Button cmd = (Button)e.OriginalSource;//достали саму кнопку

            // Create an instance of the window named
            // by the current button.
            Type type = this.GetType();//получем метаданные об окне
            Assembly assembly = type.Assembly;//получаем ссылку на текущую сборку
            Window win = (Window)assembly.CreateInstance(
                type.Namespace + "." + cmd.Content);//создаем обьект окна используя метаданные через ссылку на  обьект сборки вызываем CreateInstance и создаем обьект окна передавая название класса с кнопки 

            // Show the window.
            win.ShowDialog();
        }
    }
}
