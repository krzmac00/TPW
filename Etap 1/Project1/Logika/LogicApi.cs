using Dane;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public abstract class LogicAPI
    {
        public abstract void addBalls(int BallsNumber); //Interfejs LogicAPI definiuje cztery metody abstrakcyjne: addBalls, start, stop, getBalls

        public abstract void start();

        public abstract void stop();

        public abstract List<Ball> getBalls();

        public static LogicAPI Create(DaneAPI data = default(DaneAPI)) 
        {
            return new BussinessLogic(                          //w klasie LogicAPI tworzony jest nowy obiekt klasy BussinessLogic z wstrzykiwanymi zale�no�ciami (w tym przypadku obiektem klasy DaneAPI)
                data == null ? DaneAPI.CreateDataAPI() : data); // a nast�pnie ten obiekt jest zwracany jako interfejs LogicAPI
        }                                                       //dzi�ki temu mamy mo�liwo�� u�ywania interfejsu LogicAPI bez konieczno�ci wiedzenia, jakie dok�adnie obiekty s� w nim wykorzystywane

        private class BussinessLogic : LogicAPI
        {
            private DaneAPI daneAPI;
            private Task updatePosition;
            private Board Board;

            public BussinessLogic(DaneAPI daneAPI)
            {
                this.daneAPI = daneAPI;
                Board = new Board(562);
            }

            public override void addBalls(int BallsNumber)
            {
                Board.addBalls(BallsNumber);
            }

            public override List<Ball> getBalls()
            {
                return Board.balls;
            }

            public override void start()   //uruchamia poruszanie si� pi�ek na planszy poprzez uruchomienie w�tku, kt�ry wywo�uje metod� MoveBallsConstantly()
            {
                if (Board.balls.Count > 0)
                {
                    updatePosition = Task.Run(Board.MoveBallsConstantly);
                }
            }

            public override void stop()
            {
                Board.balls.Clear();
            }
        }

    }
}
