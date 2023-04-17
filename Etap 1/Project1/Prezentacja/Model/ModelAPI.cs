using Logika;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class ModelAPI // definiuje interfejs modelu pi�ek w aplikacji.
    {
        public abstract List<BallModel> balls { get; }
        public abstract void addBallsAndStart(int ballsNumber);

        public abstract void removeBallsAndStart();

        public static ModelAPI CreateApi() 
        {
            return new ModelBall();
        }

        internal class ModelBall : ModelAPI
        {
            private LogicAPI logicApi;

            public override List<BallModel> balls
            {
                get
                {
                    return ChangeBallToBallInModel();
                }
            }

            public ModelBall()
            {
                logicApi = logicApi ?? LogicAPI.Create(); // je�li warto�� logicApi jest r�wna null, to zostanie utworzony nowy obiekt LogicAPI za pomoc� metody statycznej Create
            }

            public override void addBallsAndStart(int ballsNumber)
            {
                logicApi.addBalls(ballsNumber);
                logicApi.start();
            }

            public List<BallModel> ChangeBallToBallInModel()
            {
                List<BallModel> result = new List<BallModel>();

                foreach (Ball ball in logicApi.getBalls())
                {
                    result.Add(new BallModel(ball));
                }
                return result;
            }

            public override void removeBallsAndStart()
            {
                logicApi.stop();
            }
        }
    }
}