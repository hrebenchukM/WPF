using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using Color = System.Windows.Media.Color;



namespace Hw7Colors
{
    //теперь уже нету регистрации свойств зависимостей,а есть реализация интерфейса контракта INotifyPropertyChanged,который предполагает выставить подписку
    class ColorViewModel : INotifyPropertyChanged
    {
        private ColorModel _color;
        public ColorViewModel(ColorModel color)
        {

            _color = color;

        }


        public byte Alpha
        {
            get
            {
                return _color.Alpha;
            }
            set
            {
                _color.Alpha = value;
                RaisePropertyChanged(nameof(Alpha));
              
            }
        }
        public byte Red
        {
            get
            {
                return _color.Red;
            }
            set
            {
                _color.Red = value;
                RaisePropertyChanged(nameof(Red));
             

            }
        }


        public byte Green
        {
            get
            {
                return _color.Green;
            }
            set
            {
                _color.Green = value;
                RaisePropertyChanged(nameof(Green));
            

            }
        }


        public byte Blue
        {
            get
            {
                return _color.Blue;
            }
            set
            {
                _color.Blue = value;
                RaisePropertyChanged(nameof(Blue));
             

            }
        }


        public Color ColorF
        {
            get
            {
                return Color.FromArgb(Alpha, Red, Green, Blue);
            }
            set
            {
                _color.ColorF = value;
                RaisePropertyChanged(nameof(ColorF));
            }
        }

        public string ColorC
        {
            get
            {
                return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", Alpha, Red, Green, Blue);
            }
            set
            {
                _color.ColorC = value;
                RaisePropertyChanged(nameof(ColorC));
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
