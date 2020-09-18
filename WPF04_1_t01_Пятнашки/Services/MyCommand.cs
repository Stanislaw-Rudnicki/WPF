using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF04_1_t01.Services
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
    }
}
