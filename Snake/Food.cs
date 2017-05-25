using System;
using System.Drawing;
namespace Snake
{
    public class Food
    {
        public Rectangle Canary;
        public int x, y, width = 10, height = 10;
        Random rand = new Random();

        public Food ()
        {
            Generate();
            Canary = new Rectangle(x, y, width, height);
        }

        public void Draw(Graphics graphics)
        {
            Canary.X = x;
            Canary.Y = y;
            graphics.FillRectangle(Brushes.Yellow, Canary);
        }

        public void Generate ()
        {
            x = rand.Next(0, 30) * 10; 
            y = rand.Next(0, 20) * 10 + 24;// Высота меню
        }
    }
}
