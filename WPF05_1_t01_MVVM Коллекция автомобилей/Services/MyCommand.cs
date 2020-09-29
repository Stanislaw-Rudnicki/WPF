using System;
using System.Windows.Input;

namespace WPF05_1_t01.Services
{
    class MyCommand : ICommand
    {
        Action<object> methodCommand;
        Predicate<object> canExecute;

        public MyCommand(Action<object> methodCommand, Predicate<object> canExecute = null)
        {
            this.methodCommand = methodCommand;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            methodCommand?.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }
    }
}
