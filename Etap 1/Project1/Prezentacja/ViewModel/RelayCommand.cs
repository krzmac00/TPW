using System;
using System.Windows.Input;

namespace ViewModel
{
    public class RelayCommand : ICommand //implementacja interfejsu ICommand
    {
        //odpowiada za reprezentowanie polecenia (komendy) do wykonania w interfejsie u¿ytkownika oraz za logikê jej wykonywania i dostêpnoœci.

        public RelayCommand(Action execute) : this(execute, null) { } // tworzy now¹ instancjê RelayCommand z akcj¹, która bêdzie wykonywana po wywo³aniu metody Execute

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            this.m_Execute = execute ?? throw new ArgumentNullException(nameof(execute));  // jeœli lewa strona jest nullem, to zamiast niej zostanie rzucony wyj¹tek
            this.m_CanExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public virtual void Execute(object parameter) //"virtual" w C# umo¿liwia klasom dziedzicz¹cym nadpisanie metody, któr¹ deklarujemy jako wirtualn¹, i przedefiniowanie jej dzia³ania
        {
            this.m_Execute();
        }


        public event EventHandler CanExecuteChanged;
        private readonly Action m_Execute;
        private readonly Func<bool> m_CanExecute;
    }
}