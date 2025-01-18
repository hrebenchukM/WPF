using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;



namespace Hw6MVVM_I
{
    //теперь уже нету регистрации свойств зависимостей,а есть реализация интерфейса контракта INotifyPropertyChanged,который предполагает выставить подписку
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            CurrentName = "Имя";
            CurrentAdress = "г.Город,ул.Улица,д.Номер";
            CurrentPhone = "+380*********";
            
        }
        private ObservableCollection<Record> _records = new ObservableCollection<Record>();

        public ObservableCollection<Record> Records
        {
            get
            {
                return _records;
            }
            set
            {
                _records = value;
                RaisePropertyChanged(nameof(Records));
            }
        }

        private string _currentName;

        public string CurrentName
        {
            get
            {
                return _currentName;
            }
            set
            {
                _currentName = value;
                RaisePropertyChanged(nameof(CurrentName));
            }
        }

        private string _currentAdress;
        public string CurrentAdress
        {
            get
            {
                return _currentAdress;
            }
            set
            {
                _currentAdress = value;
                RaisePropertyChanged(nameof(CurrentAdress));
            }
        }
        private string _currentPhone;

        public string CurrentPhone
        {
            get
            {
                return _currentPhone;
            }
            set
            {
                _currentPhone = value;
                RaisePropertyChanged(nameof(CurrentPhone));
            }
        }

      

        private int index_selected_listbox = -1;

        public int Index_selected_listbox
        {
            get { return index_selected_listbox; }
            set
            {
                index_selected_listbox = value;

                RaisePropertyChanged(nameof(Index_selected_listbox));
                if (index_selected_listbox >= 0 && index_selected_listbox < Records.Count)
                {
                    var selectedRecord = Records[index_selected_listbox];
                    CurrentName = selectedRecord.Name;
                    CurrentAdress = selectedRecord.Adress;
                    CurrentPhone = selectedRecord.Phone;
                }
            }
        }
       



      


        private DelegateCommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new DelegateCommand(Add, CanAdd);
                }
                return _AddCommand;
            }
        }
        private void Add(object o)
        {
            Record record = new Record();
            record.Name = CurrentName;
            record.Adress = CurrentAdress;
            record.Phone = CurrentPhone;
            Records.Add(record);
        }

        private bool CanAdd(object o)
        {
            if (CurrentName == "" || CurrentPhone == "" || CurrentAdress == "")
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
                selectedRecord.Name = CurrentName;
                selectedRecord.Adress = CurrentAdress;
                selectedRecord.Phone = CurrentPhone;
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




        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));//поскольку нет свойств зависимостей то требуется оповестить подписчика элементов управления при изменениях.
                                                                                      //Оповещаем впф инфраструктуру о том что ей нужно отреагировать и повызывать canExecute для всех команд,
                                                                                      //вернее оповестить подписчиков которые выполнили привязку
        }

        public event PropertyChangedEventHandler PropertyChanged;//выставляем подписку






    }
}
