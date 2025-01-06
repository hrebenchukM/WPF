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

namespace Hw2Calculator
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
            string content = cmd.Content.ToString();

            CurrentInput.Text += content;
        }



        private void Button_Click_C(object sender, RoutedEventArgs e)
        {

            CurrentInput.Text = "";
            PreviousOperations.Text = "";
        }
        private void Button_Click_Ok(object sender, RoutedEventArgs e)
        {
            try
            {
                string expression;
                  if (PreviousOperations.Text == "" || !PreviousOperations.Text.Contains("="))
                {
                    expression = PreviousOperations.Text + CurrentInput.Text;
                }
                else
                {
                    expression = CurrentInput.Text;
                }
                expression = expression.Replace('.', ',');
                double res = Calculate(expression);

                PreviousOperations.Text = expression + " = ";

                CurrentInput.Text = res.ToString();
            }
            catch
            {
                CurrentInput.Text = "Error";
            }
        }



        private void Button_Click_CE(object sender, RoutedEventArgs e)
        {
            CurrentInput.Text = "";
          
        }

        private void Button_ClickLt(object sender, RoutedEventArgs e)
        {
            if (CurrentInput.Text.Length > 0)
            {
                CurrentInput.Text = CurrentInput.Text.Substring(0, CurrentInput.Text.Length - 1);
            }
        }
        private double Calculate(string expression)
        {
            double num1, num2;

            if (expression.Contains("+"))
            {
                string[] parts = expression.Split('+');
                if (parts.Length == 2 && double.TryParse(parts[0], out num1) && double.TryParse(parts[1], out num2))
                {
                    return num1 + num2;
                }
            }
            if (expression.Contains("-"))
            {
                string[] parts = expression.Split('-');
                if (parts.Length == 2 && double.TryParse(parts[0], out num1) && double.TryParse(parts[1], out num2))
                {
                    return num1 - num2;
                }
            }
            if (expression.Contains("*"))
            {
                string[] parts = expression.Split('*');
                if (parts.Length == 2 && double.TryParse(parts[0], out num1) && double.TryParse(parts[1], out num2))
                {
                    return num1 * num2;
                }
            }
            if (expression.Contains("/"))
            {
                string[] parts = expression.Split('/');
                if (parts.Length == 2 && double.TryParse(parts[0], out num1) && double.TryParse(parts[1], out num2))
                {
                    if (num2 != 0)
                        return num1 / num2;
                    else
                        throw new DivideByZeroException();
                }
            }

            throw new Exception("Invalid Expression");
        }


    }
}