using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;



namespace Hw6MVVM_I
{
    //теперь уже нету регистрации свойств зависимостей,а есть реализация интерфейса контракта INotifyPropertyChanged,который предполагает выставить подписку
    class RecordWindowViewModel : INotifyPropertyChanged
    {
        private Record _record;
        public RecordWindowViewModel(Record record)
        {

            this._record = record;

        }
       

        
        public string Name
        {
            get
            {
                return _record.Name;
            }
            set
            {
                _record.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

      
        public string Adress
        {
            get
            {
                return _record.Adress;
            }
            set
            {
                _record.Adress = value;
                RaisePropertyChanged(nameof(Adress));
            }
        }
      
        public string Phone
        {
            get
            {
                return _record.Phone;
            }
            set
            {
                _record.Phone = value;
                RaisePropertyChanged(nameof(Phone));
            }
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
