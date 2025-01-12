using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Hw4Pocket_book
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


            if (bookrecords.Index_selected_listbox >= 0 && bookrecords.Index_selected_listbox < bookrecords.Records.Count)
            {
               
                    bookrecords.Records.RemoveAt(bookrecords.Index_selected_listbox);
                    bookrecords.Index_selected_listbox = -1;
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления.");
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            BookRecords bookrecords = Resources["bookrecords"] as BookRecords; //обращаемся к ресурсам окна и получаем обьект класса ProgrammingLanguage


            if (bookrecords.Index_selected_listbox >= 0 && bookrecords.Index_selected_listbox < bookrecords.Records.Count)
            {
                var selectedRecord = bookrecords.Records[bookrecords.Index_selected_listbox];
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
            BookRecords bookrecords = Resources["bookrecords"] as BookRecords; //обращаемся к ресурсам окна и получаем обьект класса ProgrammingLanguage
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
                BookRecords bookrecords = Resources["bookrecords"] as BookRecords; //обращаемся к ресурсам окна и получаем обьект класса ProgrammingLanguage
                if (bookrecords.Index_selected_listbox == -1)
                    return;
                Record record = bookrecords.Records[bookrecords.Index_selected_listbox];
                bookrecords.RecordName = record.Name;
                bookrecords.RecordAdress = record.Adress;
                bookrecords.RecordPhone = record.Phone;
            }
            catch { }
        }

        private void recordName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            textbox.Text = string.Empty;
        }
    }






    public class Record : INotifyPropertyChanged
    {
        private string name;
        private string adress;
        private string phone;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        public string Adress
        {
            get
            {
                return adress;
            }
            set
            {
                adress = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Adress)));
            }
        }
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Phone)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }

    public class BookRecords : INotifyPropertyChanged
    {
        //коллекция хранит не строки а обьекты Language
        public ObservableCollection<Record> Records { get; set; } = new ObservableCollection<Record>();//как Лист ,но реализует еще паттерн наблюдатель,то есть при изменениях коллекции,то она уведомляет сообщением наблюдателя о том что с ней происходило . На эту коллекцию подписан ListBox ItemsSource="{Binding Languages}"  

        private int index_selected_listbox = -1;

        public int Index_selected_listbox
        {
            get { return index_selected_listbox; }
            set
            {
                index_selected_listbox = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Index_selected_listbox)));//в метод передали данные о том какое сейчас свойство устанавливается ,чтоб понимать какого подписчика дергать нам        
            }
        }

        private string name;
        private string adress;
        private string phone;

        public string RecordName
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(RecordName)));//в метод передали данные о том какое сейчас свойство устанавливается ,чтоб понимать какого подписчика дергать нам        
            }
        }
        public string RecordAdress
        {
            get
            {
                return adress;
            }
            set
            {
                adress = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(RecordAdress)));//в метод передали данные о том какое сейчас свойство устанавливается ,чтоб понимать какого подписчика дергать нам        
            }
        }
        public string RecordPhone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(RecordPhone)));//в метод передали данные о том какое сейчас свойство устанавливается ,чтоб понимать какого подписчика дергать нам        
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);//оповещение конкретного подписчика чтоб не дергать всех подряд .                    
            //подписчик-это тот элемент который привязывался  Text="{Binding Name}
        }
    }

}