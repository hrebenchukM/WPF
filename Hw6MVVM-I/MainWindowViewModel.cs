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
        public ObservableCollection<RecordWindowViewModel> RecordsList { get; set; }

        public MainWindowViewModel(List<Record> records)
        {
            RecordsList = new ObservableCollection<RecordWindowViewModel>(records.Select(r => new RecordWindowViewModel(r)));
        }



        private string currentName;
        public string CurrentName
        {
            get { return currentName; }
            set
            {
                currentName = value;
                RaisePropertyChanged(nameof(CurrentName));
            }
        }

        private string currentAdress;
        public string CurrentAdress
        {
            get { return currentAdress; }
            set
            {
                currentAdress = value;
                RaisePropertyChanged(nameof(CurrentAdress));
            }
        }

        private string currentPhone;
        public string CurrentPhone
        {
            get { return currentPhone; }
            set
            {
                currentPhone = value;
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
                if (index_selected_listbox >= 0 && index_selected_listbox < RecordsList.Count)
                {
                    RecordWindowViewModel selectedRecord = RecordsList[index_selected_listbox];
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
            Record newRecord = new Record();
            newRecord.Name = CurrentName;
            newRecord.Adress = CurrentAdress;
            newRecord.Phone = CurrentPhone;
            RecordsList.Add(new RecordWindowViewModel(newRecord));
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
            RecordWindowViewModel selectedRecord = RecordsList[Index_selected_listbox];
            selectedRecord.Name = CurrentName;
            selectedRecord.Adress = CurrentAdress;
            selectedRecord.Phone = CurrentPhone;
        }


        private bool CanEdit(object o)
        {
            return Index_selected_listbox >= 0 && Index_selected_listbox < RecordsList.Count;
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

            RecordsList.RemoveAt(Index_selected_listbox);
            Index_selected_listbox = -1;

        }

        private bool CanDelete(object o)
        {
            return Index_selected_listbox >= 0 && Index_selected_listbox < RecordsList.Count;
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
                foreach (RecordWindowViewModel recordViewModel in RecordsList)
                {
                   
                    lines.Add(recordViewModel.Name + ";" + recordViewModel.Adress + ";" + recordViewModel.Phone);
                }

                File.WriteAllLines(saveFileDialog1.FileName, lines);

            }

        }

        private bool CanSave(object o)
        {
            return RecordsList.Count > 0;
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
                RecordsList.Clear();
                foreach (var line in lines)
                {
                    var parts = line.Split(';');
                    if (parts.Length == 3)
                    {
                        RecordsList.Add(new RecordWindowViewModel(new Record
                        {
                            Name = parts[0],
                            Adress = parts[1],
                            Phone = parts[2]
                        }));
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));//поскольку нет свойств зависимостей то требуется оповестить подписчика элементов управления при 
                                                                                      //Оповещаем впф инфраструктуру о том что ей нужно отреагировать и повызывать canExecute для всех команд, изменениях.  
                                                                                      //вернее оповестить подписчиков которые выполнили привязку
        }

        public event PropertyChangedEventHandler PropertyChanged;//выставляем подписку






    }
}
