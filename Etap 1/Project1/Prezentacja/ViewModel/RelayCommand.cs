using System;
using System.Windows.Input;

namespace ViewModel
{
    public class RelayCommand : ICommand //implementacja interfejsu ICommand
    {
        //odpowiada za reprezentowanie polecenia (komendy) do wykonania w interfejsie u�ytkownika oraz za logik� jej wykonywania i dost�pno�ci.

        public RelayCommand(Action execute) : this(execute, null) { } // tworzy now� instancj� RelayCommand z akcj�, kt�ra b�dzie wykonywana po wywo�aniu metody Execute

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            this.m_Execute = execute ?? throw new ArgumentNullException(nameof(execute));  // je�li lewa strona jest nullem, to zamiast niej zostanie rzucony wyj�tek
            this.m_CanExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public virtual void Execute(object parameter) //"virtual" w C# umo�liwia klasom dziedzicz�cym nadpisanie metody, kt�r� deklarujemy jako wirtualn�, i przedefiniowanie jej dzia�ania
        {
            this.m_Execute();
        }


        public event EventHandler CanExecuteChanged;
        private readonly Action m_Execute;
        private readonly Func<bool> m_CanExecute;
    }
}