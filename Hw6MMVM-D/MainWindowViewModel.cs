using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using Hw6MMVM_D;
using Microsoft.Win32;



namespace Hw6MMVM_D
{
    class MainWindowViewModel : DependencyObject//должны быть публичные свойства  на которые будет привязку выполнять вьюшка ,
                                                //и команды(обьекты команд) на которые воздействуют элементы управления когда мы нажимаем на них
    {

        //регистрируем 4 свойства зависимости 
        private static readonly DependencyProperty NameProperty;
        private static readonly DependencyProperty AdressProperty;
        private static readonly DependencyProperty PhoneProperty;
        private static readonly DependencyProperty IndexSelectedListboxProperty;
        private static readonly DependencyProperty RecordsProperty;


        static MainWindowViewModel()
        {

            //обычное дотнет свойство не может оповещать подписчика поэтому регистрируем свойства зависисмостей
            NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(MainWindowViewModel));
            AdressProperty = DependencyProperty.Register("Adress", typeof(string), typeof(MainWindowViewModel));
            PhoneProperty = DependencyProperty.Register("Phone", typeof(string), typeof(MainWindowViewModel));
            IndexSelectedListboxProperty = DependencyProperty.Register("Index_selected_listbox", typeof(int), typeof(MainWindowViewModel));
            RecordsProperty = DependencyProperty.Register("Records", typeof(ObservableCollection<Record>), typeof(MainWindowViewModel));
            // зачем регистрировать PersonsProperty свойство зависисмостей если у нас уже есть ObservableCollection где уже есть паттерн наблюдатель?
            //Нужно в случае если мы хотим подменить одну коллекцию на другую ,
            //то есть мы не изменяем саму коллекцию а присваиваем ейновую коллекцию и тут уже не будет знать подписчик,
            //поэтому нам и надо свойствл зависисмостей.


        }

        public MainWindowViewModel()
        {
            Name = "Имя";
            Adress = "г.Город,ул.Улица,д.Номер";
            Phone = "+380*********";
            Records = new ObservableCollection<Record>();
        }



        //реализуем публичные свойства - обертки над свойствами зависимостей
      
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
        public int Index_selected_listbox
        {
            get { return (int)GetValue(IndexSelectedListboxProperty); }
            set
            {
                SetValue(IndexSelectedListboxProperty, value);
                if (value >= 0 && value < Records.Count)
                {
                    var selectedRecord = Records[value];
                    Name = selectedRecord.Name;
                    Adress = selectedRecord.Adress;
                    Phone = selectedRecord.Phone;
                }
            }
        }



        public ObservableCollection<Record> Records
        {
            get { return (ObservableCollection<Record>)GetValue(RecordsProperty); }
            set { SetValue(RecordsProperty, value); }
        }



      


        private DelegateCommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new DelegateCommand(Add, CanAdd);//создается обьект команды
                    //и в конструктор через делегаты передается два метода два ресивера (паттерн команда) Add(логика), CanAdd(проверка доступности)
                }
                return _AddCommand;
            }
        }
        private void Add(object o)
        {
            //никаких прямых обращений к текстовым полям быть не может , тут работает binding, мы обращаемся к свойствам где уже все есть,
            //то что мы вводим в текстовые поля сразу находится в свойствах ,ибо текстовое поле подписывается на публичные свойства + режим TwoWay для текстовых полей по умолчанию
            Record record = new Record();
            record.Name = this.Name;
            record.Adress = this.Adress;
            record.Phone = this.Phone;
            Records.Add(record);
          
        }

        private bool CanAdd(object o)
        {
            //срабатывает при смене фокуса , после любой команды ,и если у нас есть целевой элемент
            //(если не обычные кнопки а кнопки панель инструментов или пункты меню-целевой элемент указывать не нужно)
            //любое изменение в текстововм поле будет провоцировать на вызов метода проверка доступности
            if (Name == "" || Phone == "" || Adress == "")
                return false;
            return true;
        }





        private DelegateCommand _EditCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_EditCommand == null)
                {
                    _EditCommand = new DelegateCommand(Edit, CanEdit);
                }
                return _EditCommand;
            }
        }
        private void Edit(object o)
        {
            var selectedRecord = Records[Index_selected_listbox];
            selectedRecord.Name = this.Name;
            selectedRecord.Adress = this.Adress;
            selectedRecord.Phone = this.Phone;

            Records[Index_selected_listbox] = new Record(Name, Adress, Phone);
        }

        private bool CanEdit(object o)
        {
            return Index_selected_listbox >= 0 && Index_selected_listbox < Records.Count;
        }

     



        private DelegateCommand _DeleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new DelegateCommand(Delete, CanDelete);
                }
                return _DeleteCommand;
            }
        }
        private void Delete(object o)
        {
            Records.RemoveAt(Index_selected_listbox);
            Index_selected_listbox = -1;
        }

        private bool CanDelete(object o)
        {
            return Index_selected_listbox >= 0 && Index_selected_listbox < Records.Count;
        }







        private DelegateCommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new DelegateCommand(Save, CanSave);
                }
                return _SaveCommand;
            }
        }
        private void Save(object o)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            if (saveFileDialog1.ShowDialog() == true)
            {
        

                var lines = new List<string>();
                foreach (Record record in Records)
                {
                    lines.Add(record.Name + ";" + record.Adress + ";" + record.Phone);
                }

                File.WriteAllLines(saveFileDialog1.FileName, lines);

            }

        }

        private bool CanSave(object o)
        {
            return Records.Count > 0;
        }





        private DelegateCommand _OpenCommand;
        public ICommand OpenCommand
        {
            get
            {
                if (_OpenCommand == null)
                {
                    _OpenCommand = new DelegateCommand(Open, CanOpen);
                }
                return _OpenCommand;
            }
        }
        private void Open(object o)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == true)
            {
              
                var lines = File.ReadAllLines(openFileDialog1.FileName);
                Records.Clear();
                foreach (var line in lines)
                {
                    var parts = line.Split(';');
                    if (parts.Length == 3)
                    {
                        Records.Add(new Record
                        {
                            Name = parts[0],
                            Adress = parts[1],
                            Phone = parts[2]
                        });
                    }
                }
            }

        }

        private bool CanOpen(object o)
        {
            return true;
        }






    }
}
