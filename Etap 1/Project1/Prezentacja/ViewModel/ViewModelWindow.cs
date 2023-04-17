using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class ViewModelWindow : INotifyPropertyChanged // służy do połączenia modelu (ModelAPI) z interfejsem użytkownika (GUI)
    {  //implementuje interfejs INotifyPropertyChanged, który umożliwia powiadamianie GUI o zmianach w danych i ich aktualizacji.
        private ModelAPI modelApi;
        private readonly double scale = 5.35;
        public ObservableCollection<BallModel> Balls { get; set; }
        public ICommand StartButtonClick { get; set; }
        public ICommand StopButtonClick { get; set; }
        private string inputText;
        private Task task;

        private bool state;
        private bool notState = false;

        public bool NotState
        {
            get { 
                return notState; 
            }
            set { 
                notState = value;
                RaisePropertyChanged(nameof(NotState));
            }
        }

        public bool State //gdy pole State jest ustawione na true, to przycisk "Start" jest aktywny, gdy jest ustawione na false, to przycisk "Start" jest wyłączony, analogicznie notState do stop
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                RaisePropertyChanged(nameof(State)); 
            }
        }


        public string InputText
        {
            get
            {
                return inputText;
            }
            set
            {
                inputText = value;
                RaisePropertyChanged(nameof(InputText));
            }
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelWindow() : this(ModelAPI.CreateApi())
        {

        }

        public ViewModelWindow(ModelAPI baseModel)
        {
            State = true;
            this.modelApi = baseModel;
            StartButtonClick = new RelayCommand(StartButtonClickHandler);
            StopButtonClick = new RelayCommand(StopButtonClickHandler);
            Balls = new ObservableCollection<BallModel>();
        }

        private void StopButtonClickHandler()
        {
                modelApi.removeBallsAndStart();
                readFromTextBox();
        }

        private void StartButtonClickHandler()
        {
                modelApi.addBallsAndStart(readFromTextBox());
                task = new Task(UpdatePosition); //Wątek ten wykonuje kod z metody UpdatePosition(), która zawiera pętlę while i wewnętrznie wywołuje metodę
            task.Start();                       // Thread.Sleep(10), co oznacza, że wątek będzie uśpiony na 10 milisekund w każdej iteracji pętli.
        }

        public void UpdatePosition()
        {
            while (true)
            {
                //ObservableCollection to implementacja interfejsu INotifyCollectionChanged, która dostarcza automatyczne powiadomienia o zmianach w kolekcji
                ObservableCollection<BallModel> treadList = new ObservableCollection<BallModel>(); 
                
                foreach (BallModel ball in modelApi.balls)
                {
                    treadList.Add(ball);
                }

                Balls = treadList;
                RaisePropertyChanged(nameof(Balls)); //aktualizowani widoku, który subskrybuje zdarzenie PropertyChanged dla Balls
                Thread.Sleep(10);
            }
        }

        public int readFromTextBox()
        {
            int number;
            if (Int32.TryParse(InputText, out number) && InputText != "0")
            {
                number = Int32.Parse(InputText);
                ErrorMessage = "";
                State = !State;
                NotState = !NotState;
                if (number > 100)
                {
                    ErrorMessage = "Podaj <= 100";
                    return 0;
                }
                return number;
            }
            ErrorMessage = "Nieprawidłowa liczba";
            return 0;
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}