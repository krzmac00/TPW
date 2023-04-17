using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logika
{
    public class Board
    {
        public int size { get; set; }
        public List<Ball> balls { get; set; }
        private Task updatePosition; //w¹tek, który odpowiada za poruszanie pi³ek
        private int speed = 30; //czas pomiêdzy krokami poruszania siê pi³ek

        public Board(int size)
        {
            this.size = size;
        }

        public void addBalls(int BallsNumber)
        {
            balls = new List<Ball>();
            for (int i = 0; i < BallsNumber; i++)
            {
                balls.Add(new Ball());
            }
        }

        public void removeBalls(int BallsNumber)
        {
            balls = new List<Ball>();
            for (int i = 0; i < BallsNumber; i++)
            {
                balls.RemoveAt(i);
            }
        }
       
        public void MoveBalls() //przesuwa ka¿d¹ pi³kê na planszy o jej wartoœæ przesuniêcia
        {
                foreach (Ball ball in balls)
                {
                ball.changeBallPosition(size);
                }
        }

        public void MoveBallsConstantly()
        {
            while (true)
            {
                MoveBalls();
                Thread.Sleep(speed); //zawiesza w¹tek na okreœlony czas przy pomocy metody Thread.Sleep(speed)
            }
        }

        public void start()
        {
            updatePosition = new Task(MoveBallsConstantly); //uruchamia w osobnym w¹tku ci¹g³e przesuwanie pi³ek na planszy poprzez wywo³anie metody MoveBallsConstantly()
            updatePosition.Start();
        }
    }
}