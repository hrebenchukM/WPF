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
using Microsoft.Win32;
using System.IO;
using Path = System.IO.Path;


namespace Hw3Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   

    public partial class MainWindow : Window
    {
        public string curFilePath = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Create_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.Clear();
            curFilePath = "";
            Title = "Новый файл.txt - Блокнот";
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
        
            if (openFileDialog1.ShowDialog() == true)
            {
                TextEditor.Text = File.ReadAllText(openFileDialog1.FileName);
                Title = Path.GetFileName(openFileDialog1.FileName) + " - Блокнот";
            }
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (curFilePath == "")
            {
                SaveAs_Click(sender, e);
            }
            else
            {
                File.WriteAllText(curFilePath, TextEditor.Text);
                MessageBox.Show("Файл сохранен!");
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
          
            if (saveFileDialog1.ShowDialog() == true)
            {
                curFilePath = saveFileDialog1.FileName;

                File.WriteAllText(curFilePath, TextEditor.Text);
                MessageBox.Show("Файл сохранен!");
            }
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.Undo();
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.Cut();
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.Copy();
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.Paste();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.SelectedText = "";
        }

 
        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.SelectAll();
        }

        private void DateTime_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.Text += DateTime.Now.ToString();
        }

        private void WordWrap_Click(object sender, RoutedEventArgs e)
        {
            if (TextEditor.TextWrapping == TextWrapping.Wrap)
            {
                TextEditor.TextWrapping = TextWrapping.NoWrap;
                WordWrap.IsChecked = false;
            }
            else
            {
                TextEditor.TextWrapping = TextWrapping.Wrap;
                WordWrap.IsChecked = true;
            }
        }



        private void FontFamily_Click(object sender, RoutedEventArgs e)
        {

            MenuItem mItem = sender as MenuItem;
            string fontFamilyName = mItem.Header as string;

            if (mItem != null && fontFamilyName != null)
            {
                TextEditor.FontFamily = new FontFamily(fontFamilyName);
            }
        }

       
        private void FontSize_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mItem = sender as MenuItem;
            string fontSizeStr = mItem.Header as string;

            if (mItem != null && fontSizeStr != null && double.TryParse(fontSizeStr, out double fontSize))
            {
                TextEditor.FontSize = fontSize;
            }
        }


        private void FontStyle_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mItem = sender as MenuItem;
            string fontStyleStr = mItem.Header as string;

            if (mItem != null && fontStyleStr != null)
            {
                switch (fontStyleStr)
                {
                    case "Bold":
                        TextEditor.FontWeight = FontWeights.Bold;
                        break;
                    case "Normal":
                        TextEditor.FontWeight = FontWeights.Normal;
                        break;
                    default:
                        MessageBox.Show("No such fontstyle: " + fontStyleStr);
                        break;
                }
            }
        }

     

        private void LeftEdge_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.TextAlignment = TextAlignment.Left;
        }

        private void Center_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.TextAlignment = TextAlignment.Center;
        }

        private void RightEdge_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.TextAlignment = TextAlignment.Right;
        }

        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.FontSize += 2;
        }

        private void Decrease_Click(object sender, RoutedEventArgs e)
        {
            if (TextEditor.FontSize > 2)
                TextEditor.FontSize -= 2;
        }

        private void Default_Click(object sender, RoutedEventArgs e)
        {
            TextEditor.FontSize = 12;
        }

     


        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Приложение «Блокнот», обладающее той же \r\nфункциональностью, что и стандартный «Блокнот» \r\nоперационной системы Windows 10");
        }
    }
}