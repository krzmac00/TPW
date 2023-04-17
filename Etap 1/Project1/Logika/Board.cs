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
        private Task updatePosition; //w�tek, kt�ry odpowiada za poruszanie pi�ek
        private int speed = 30; //czas pomi�dzy krokami poruszania si� pi�ek

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
       
        public void MoveBalls() //przesuwa ka�d� pi�k� na planszy o jej warto�� przesuni�cia
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
                Thread.Sleep(speed); //zawiesza w�tek na okre�lony czas przy pomocy metody Thread.Sleep(speed)
            }
        }

        public void start()
        {
            updatePosition = new Task(MoveBallsConstantly); //uruchamia w osobnym w�tku ci�g�e przesuwanie pi�ek na planszy poprzez wywo�anie metody MoveBallsConstantly()
            updatePosition.Start();
        }
    }
}