using System;
using System.Windows.Input;

namespace Umsetzung_III
{
    // Abstrakte Blueprintklasse fuer das einfachere Erstellen von CommandKlassen 
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public abstract void Execute(object? parameter);
    }
}
