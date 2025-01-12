using System.Collections.ObjectModel;
using System.IO;
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
using Path = System.IO.Path;

namespace Hw4Pocket_bookD
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
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == true)
            {
                BookRecords bookrecords = Resources["bookrecords"] as BookRecords; //обращаемся к ресурсам окна и получаем обьект класса ProgrammingLanguage

                var lines = File.ReadAllLines(openFileDialog1.FileName);
                bookrecords.Records.Clear();
                foreach (var line in lines)
                {
                    var parts = line.Split(';');
                    if (parts.Length == 3)
                    {
                        bookrecords.Records.Add(new Record
                        {
                            Name = parts[0],
                            Adress = parts[1],
                            Phone = parts[2]
                        });
                    }
                }

                Title = Path.GetFileName(openFileDialog1.FileName) + " - Записная книжка";
            }
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            if (saveFileDialog1.ShowDialog() == true)
            {
                BookRecords bookrecords = Resources["bookrecords"] as BookRecords; //обращаемся к ресурсам окна и получаем обьект класса ProgrammingLanguage



                var lines = new List<string>();
                foreach (Record record in bookrecords.Records)
                {
                    lines.Add(record.Name + ";" + record.Adress + ";" + record.Phone);
                }


                File.WriteAllLines(saveFileDialog1.FileName, lines);

                MessageBox.Show("Файл сохранен!");
            }


        }



        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            BookRecords bookrecords = Resources["bookrecords"] as BookRecords; //обращаемся к ресурсам окна и получаем обьект класса ProgrammingLanguage


            if (bookrecords.SelectedIndex != -1)
            {

                bookrecords.Records.RemoveAt(bookrecords.SelectedIndex);
                
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления.");
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            BookRecords bookrecords = Resources["bookrecords"] as BookRecords; //обращаемся к ресурсам окна и получаем обьект класса ProgrammingLanguage


            if (bookrecords.SelectedIndex != -1)
            {
                Record selectedRecord = bookrecords.Records[bookrecords.SelectedIndex];
                selectedRecord.Name = bookrecords.RecordName;
                selectedRecord.Adress = bookrecords.RecordAdress;
                selectedRecord.Phone = bookrecords.RecordPhone;
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования.");
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BookRecords bookrecords = Resources["bookrecords"] as BookRecords;
            Record record = new Record();
            record.Name = bookrecords.RecordName;
            record.Adress = bookrecords.RecordAdress;
            record.Phone = bookrecords.RecordPhone;
            bookrecords.Records.Add(record);
        }

        private void listBox1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                BookRecords bookrecords = Resources["bookrecords"] as BookRecords;
                if (bookrecords.SelectedIndex == -1)
                    return;
                Record record = bookrecords.Records[bookrecords.SelectedIndex];
                bookrecords.RecordName = record.Name;
                bookrecords.RecordAdress = record.Adress;
                bookrecords.RecordPhone = record.Phone;
            }
            catch { }
        }

        private void langName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            textbox.Text = string.Empty;
        }
    }

    public class Record : DependencyObject
    {
        private static readonly DependencyProperty NameProperty;
        private static readonly DependencyProperty AdressProperty;
        private static readonly DependencyProperty PhoneProperty;

        static Record()
        {
            NameProperty = DependencyProperty.Register(nameof(Name), typeof(string), typeof(Record));
            AdressProperty = DependencyProperty.Register(nameof(Adress), typeof(string), typeof(Record));
            PhoneProperty = DependencyProperty.Register(nameof(Phone), typeof(string), typeof(Record));
        }

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public string Adress
        {
            get { return (string)GetValue(AdressProperty); }
            set { SetValue(AdressProperty, value); }
        }
        public string Phone
        {
            get { return (string)GetValue(PhoneProperty); }
            set { SetValue(PhoneProperty, value); }
        }
    }

    public class BookRecords : DependencyObject
    {
        public ObservableCollection<Record> Records { get; set; } = new ObservableCollection<Record>();


        private static readonly DependencyProperty SelectedIndexProperty;
        private static readonly DependencyProperty NameProperty;
        private static readonly DependencyProperty AdressProperty;
        private static readonly DependencyProperty PhoneProperty;

        static BookRecords()
        {
            SelectedIndexProperty = DependencyProperty.Register(nameof(SelectedIndex), typeof(int), typeof(BookRecords));
            NameProperty = DependencyProperty.Register(nameof(RecordName), typeof(string), typeof(BookRecords));
            AdressProperty = DependencyProperty.Register(nameof(RecordAdress), typeof(string), typeof(BookRecords));
            PhoneProperty = DependencyProperty.Register(nameof(RecordPhone), typeof(string), typeof(BookRecords));
        }

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public string RecordName
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public string RecordAdress
        {
            get { return (string)GetValue(AdressProperty); }
            set { SetValue(AdressProperty, value); }
        }
        public string RecordPhone
        {
            get { return (string)GetValue(PhoneProperty); }
            set { SetValue(PhoneProperty, value); }
        }
    }
}