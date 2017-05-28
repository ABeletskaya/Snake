using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Snake
{
    class GameLogic
    {
        public int route = 0;
        public int score = 0;
        public Snake snake;
        public Food food;
        public string message;
        public Records record;


        public GameLogic()
        {
            snake = new Snake();
            food = new Food();
            record = new Records();
            record.ExistRecordFile(out message);
        }


        public void Draw(Graphics graphics)
        {
            snake.Draw(graphics);
            food.Draw(graphics);
        }


        public void IntersectFood()
        {
            if (snake.Body[0].IntersectsWith(food.Canary))
            {
                score++;
                snake.Grow();
                food.Generate();
            }
        }
               

        public void ChangeRoute(int direction) // 0 = Право, 1 = Низ, 2 = Лево, 3 = Вверх
        {
            switch(direction)
            {
                case 0: 
                    if (route != 2)
                        route = 0;
                    break;
                case 1:
                    if (route != 3)
                        route = 1;
                    break;
                case 2:
                    if (route != 0)
                        route = 2;
                    break;
                case 3:
                    if (route != 1)
                        route = 3;
                    break;
            }
        }


        public bool GameActionsAndRestart()
        {
            snake.Move(route);

            if (snake.BorderCollision() || snake.BodyCollision())
                return true;

            IntersectFood();
            return false;
        }


        public void Restart()
        {
            record.UpdateRecord(ref message, score);
            snake = new Snake();
            food = new Food();
            route = 0;
            score = 0;
        }
    }
}
