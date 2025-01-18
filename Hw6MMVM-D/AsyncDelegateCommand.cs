using System;
using System.Windows.Input;

namespace Hw6MMVM_D
{
    //стандартный код любой команды
    public class DelegateCommand : ICommand//реализуем интерфейс ICommand чтоб создать свою команду с нуля,этот контракт требует чтоб мы выставили подписку на событие для инфраструктуры WPF
    {
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;//обязано быть 
            _canExecute = canExecute;//не обязано быть 
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute(parameter);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged//выставляем подписку на событие в расширенном варианте чтоб элемент интерфейса вел себя правильно
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //делегат хранит ссылку на метод
        Action<object> _execute;//стандартный делегат войдовский в котором находится обработчик события execute выполнения команды
        Predicate<object> _canExecute;//стандартный делегат булевский в котором находится обработчик события canExecute доступности команды

    }

}
