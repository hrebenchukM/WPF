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
using static Hw5Pocket_BookICommands.MainWindow;
using Path = System.IO.Path;

namespace Hw5Pocket_BookICommands
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

        //стандартный код любой команды
        public class RelayCommand : ICommand//реализуем интерфейс ICommand чтоб создать свою команду с нуля,этот контракт требует чтоб мы выставили подписку на событие для инфраструктуры WPF
        {//делегат хранит ссылку на метод
            Action<object> _execute;//стандартный делегат войдовский в котором находится обработчик события execute выполнения команды
            Predicate<object> _canExecute;//стандартный делегат булевский в котором находится обработчик события canExecute доступности команды


            //В примере нет архитектуры и паттерна MVVM,ибо в нем категорически команда не может знать не про какие листбоксы
            protected internal object sender;//просто храним ссылку на тот элемент в отношении которого команда

            public RelayCommand(Action<object> execute, Predicate<object> canExecute)
            {
                _execute = execute ?? throw new ArgumentNullException("execute");//обязано быть 
                _canExecute = canExecute;//не обязано быть 
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute == null ? true : _canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                sender = parameter;
                _execute(parameter);
            }

            public event EventHandler CanExecuteChanged//выставляем подписку на событие в расширенном варианте чтоб элемент интерфейса вел себя правильно
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }


        private void SaveCommand(object sender, ExecutedRoutedEventArgs e)
        {
           
          //  MessageBox.Show(e.OriginalSource.ToString());


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

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //срабатывает
            //1-если изменилось текстовое поле фокуса если бы был флажок Change,
            //2-если переходит фокус ввода по табуляции с элемента на элемент,
            //3-если после любого действия
            //из этого вывод никакого большщого тяжелого кода в обработчике не делать 

            //любое текстовое поле в фокусе является целевым элементом 
            e.CanExecute = true;//включаем команду
        }



        private void OpenCommand(object sender, ExecutedRoutedEventArgs e)
        {

           // MessageBox.Show(e.OriginalSource.ToString());


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

        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;//включаем команду
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




        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public BookRecords()
        {
            AddCommand = new RelayCommand(param => AddRecord(), param => CanAddRecord());
            EditCommand = new RelayCommand(param => EditRecord(), param => CanEditRecord());
            DeleteCommand = new RelayCommand(param => DeleteRecord(), param => CanDeleteRecord());
        }
        private void AddRecord()
        {

            Record record = new Record();
            record.Name = RecordName;
            record.Adress = RecordAdress;
            record.Phone =RecordPhone;
            Records.Add(record);
          
        }

        private void EditRecord()
        {
                var selectedRecord =Records[Index_selected_listbox];
                selectedRecord.Name = RecordName;
                selectedRecord.Adress = RecordAdress;
                selectedRecord.Phone =RecordPhone;          
        }

        private void DeleteRecord()
        {
               Records.RemoveAt(Index_selected_listbox);
               Index_selected_listbox = -1;
        }

        private bool CanAddRecord()
        {
            return RecordName != "" && RecordAdress != "" && RecordPhone != "";
        }

        private bool CanEditRecord()
        {
            return Index_selected_listbox >= 0 && Index_selected_listbox < Records.Count;
        }

        private bool CanDeleteRecord()
        {
            return Index_selected_listbox >= 0 && Index_selected_listbox < Records.Count;
        }

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