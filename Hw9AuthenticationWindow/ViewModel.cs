using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using Hw9AuthenticationWindow;
using Microsoft.Win32;



namespace Hw9AuthenticationWindow
{
    class ViewModel : DependencyObject//должны быть публичные свойства  на которые будет привязку выполнять вьюшка ,
                                                //и команды(обьекты команд) на которые воздействуют элементы управления когда мы нажимаем на них
    {

        //регистрируем 4 свойства зависимости 
        private static readonly DependencyProperty UsernameProperty;
        private static readonly DependencyProperty PasswordProperty;
        private static readonly DependencyProperty RememberMeProperty;
        private static readonly DependencyProperty RecordsProperty;

        static ViewModel()
        {

            //обычное дотнет свойство не может оповещать подписчика поэтому регистрируем свойства зависисмостей
            UsernameProperty = DependencyProperty.Register("Username", typeof(string), typeof(ViewModel));
            PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(ViewModel));
            RememberMeProperty = DependencyProperty.Register("RememberMe", typeof(bool), typeof(ViewModel));
            RecordsProperty = DependencyProperty.Register("Records", typeof(ObservableCollection<Account>), typeof(ViewModel));


        }

        public ViewModel()
        {
            Username = "MaryZaiats";
            Password = "12345678";
            RememberMe = true;
            Records = new ObservableCollection<Account>();
        }



        //реализуем публичные свойства - обертки над свойствами зависимостей

        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }
      
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }


        public bool RememberMe
        {
            get { return (bool)GetValue(RememberMeProperty); }
            set { SetValue(RememberMeProperty, value); }
        }

        public ObservableCollection<Account> Records
        {
            get { return (ObservableCollection<Account>)GetValue(RecordsProperty); }
            set { SetValue(RecordsProperty, value); }
        }







        private DelegateCommand _LoginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (_LoginCommand == null)
                {
                    _LoginCommand = new DelegateCommand(Add, CanAdd);//создается обьект команды
                    //и в конструктор через делегаты передается два метода два ресивера (паттерн команда) Add(логика), CanAdd(проверка доступности)
                }
                return _LoginCommand;
            }
        }
        private void Add(object o)
        {
            //никаких прямых обращений к текстовым полям быть не может , тут работает binding, мы обращаемся к свойствам где уже все есть,
            //то что мы вводим в текстовые поля сразу находится в свойствах ,ибо текстовое поле подписывается на публичные свойства + режим TwoWay для текстовых полей по умолчанию
            Account acc = new Account();
            acc.Login = Username;
            acc.Password = Password;
            Records.Add(acc);
            MessageBox.Show($"Logging in as {Username}", "Authentication", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanAdd(object o)
        {
            //срабатывает при смене фокуса , после любой команды ,и если у нас есть целевой элемент
            //(если не обычные кнопки а кнопки панель инструментов или пункты меню-целевой элемент указывать не нужно)
            //любое изменение в текстововм поле будет провоцировать на вызов метода проверка доступности
            if (Username == "" || Password == "")
                return false;
            return true;
        }





        private DelegateCommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new DelegateCommand(Cancel, CanCancel);
                }
                return _CancelCommand;
            }
        }
        private void Cancel(object o)
        {
            Application.Current.Shutdown();
        }

        private bool CanCancel(object o)
        {
            return true;
        }




    }
}
