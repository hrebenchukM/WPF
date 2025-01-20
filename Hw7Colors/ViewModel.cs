using System;
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
    class ViewModel : INotifyPropertyChanged
    {//теперь уже нету регистрации свойств зависимостей,а есть реализация интерфейса контракта INotifyPropertyChanged,который предполагает выставить подписку

        public ObservableCollection<ColorViewModel> Colors { get; set; }


        public ViewModel()
        {
            
            Colors = new ObservableCollection<ColorViewModel>();
        }


    private byte _currentAlpha;

    public byte CurrentAlpha
    {
        get
        {
            return _currentAlpha;
        }
        set
        {
            _currentAlpha = value;
            RaisePropertyChanged(nameof(CurrentAlpha));
            RaisePropertyChanged(nameof(CurrentColorFromArgb));
            RaisePropertyChanged(nameof(CurrentColorCode));

        }
    }



    private byte _currentRed;

    public byte CurrentRed
    {
        get
        {
            return _currentRed;
        }
        set
        {
            _currentRed = value;
            RaisePropertyChanged(nameof(CurrentRed));
            RaisePropertyChanged(nameof(CurrentColorFromArgb));
            RaisePropertyChanged(nameof(CurrentColorCode));
        }
    }



    private byte _currentGreen;

    public byte CurrentGreen
    {
        get
        {
            return _currentGreen;
        }
        set
        {
            _currentGreen = value;
            RaisePropertyChanged(nameof(CurrentGreen));
            RaisePropertyChanged(nameof(CurrentColorFromArgb));
            RaisePropertyChanged(nameof(CurrentColorCode));
        }
    }


    private byte _currentBlue;

    public byte CurrentBlue
    {
        get
        {
            return _currentBlue;
        }
        set
        {
            _currentBlue = value;
            RaisePropertyChanged(nameof(CurrentBlue));
            RaisePropertyChanged(nameof(CurrentColorFromArgb));
            RaisePropertyChanged(nameof(CurrentColorCode));
        }
    }




    private bool _isAlphaEnabled;

    public bool IsAlphaEnabled
    {
        get
        {
            return _isAlphaEnabled;
        }
        set
        {
            _isAlphaEnabled = value;
            RaisePropertyChanged(nameof(IsAlphaEnabled));
        }
    }



    private bool _isRedEnabled;

    public bool IsRedEnabled
    {
        get
        {
            return _isRedEnabled;
        }
        set
        {
            _isRedEnabled = value;
            RaisePropertyChanged(nameof(IsRedEnabled));
        }
    }



    private bool _isGreenEnabled;

    public bool IsGreenEnabled
    {
        get
        {
            return _isGreenEnabled;
        }
        set
        {
            _isGreenEnabled = value;
            RaisePropertyChanged(nameof(IsGreenEnabled));
        }
    }


    private bool _isBlueEnabled;

    public bool IsBlueEnabled
    {
        get
        {
            return _isBlueEnabled;
        }
        set
        {
            _isBlueEnabled = value;
            RaisePropertyChanged(nameof(IsBlueEnabled));
        }
    }




    private Color _currentColorFromArgb;

    public Color CurrentColorFromArgb
        {
        get
        {
                return Color.FromArgb(CurrentAlpha, CurrentRed, CurrentGreen, CurrentBlue);
        }
        set
        {
                _currentColorFromArgb = value;
                RaisePropertyChanged(nameof(CurrentColorFromArgb));
        }

    }



    private string _currentColorCode;

    public string CurrentColorCode
    {
        get
        {
               return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", CurrentAlpha, CurrentRed, CurrentGreen, CurrentBlue);

        }
        set
        {
             _currentColorCode = value;
             RaisePropertyChanged(nameof(CurrentColorCode));
        }

    }




    private int _index_selected_listbox;

    public int Index_selected_listbox
    {
        get
        {
            return _index_selected_listbox;
        }
        set
        {
            _index_selected_listbox = value;
            RaisePropertyChanged(nameof(Index_selected_listbox));
                if (_index_selected_listbox >= 0 && _index_selected_listbox <   Colors.Count)
                {
                    ColorViewModel selectedColor = Colors[_index_selected_listbox];
                    CurrentAlpha = selectedColor.Alpha;
                    CurrentRed = selectedColor.Red;
                    CurrentBlue= selectedColor.Blue;
                    CurrentColorCode = selectedColor.ColorC;
                    CurrentColorFromArgb = selectedColor.ColorF;
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
            ColorModel newColorModel = new ColorModel();
            newColorModel.Alpha = CurrentAlpha;
            newColorModel.Red = CurrentRed;
            newColorModel.Green = CurrentGreen;
            newColorModel.Blue = CurrentBlue;
            newColorModel.ColorC = CurrentColorCode;
            newColorModel.ColorF = CurrentColorFromArgb;
            Colors.Add(new ColorViewModel(newColorModel));

        }


        private bool CanAdd(object o)
        {
           return !Colors.Any(c =>
                c.Alpha == CurrentAlpha &&
                c.Red == CurrentRed &&
                c.Green == CurrentGreen &&
                c.Blue == CurrentBlue &&
                c.ColorC == CurrentColorCode &&
                c.ColorF == CurrentColorFromArgb);
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
            Colors.RemoveAt(Index_selected_listbox);
            Index_selected_listbox = -1;
        }

        private bool CanDelete(object o)
        {
            return Index_selected_listbox >= 0 && Index_selected_listbox < Colors.Count;
        }








        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;



    }
}
