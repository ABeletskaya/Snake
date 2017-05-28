using System;
using System.Drawing;

namespace Snake
{
    public class Food
    {
        public Rectangle Canary;
        public int x, y;
        private const int xMin = 0, yMin = 0, xMax = 290, yMax = 200, width = 10, height = 10, heightMenu = 24;
        private const int xRandMax = xMax / width;
        private const int yRandMax = yMax / height;
        Random rand = new Random();


        public Food()
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


        public void Generate()
        {           
            x = rand.Next(20, xRandMax) * width; 
            y = rand.Next(16, yRandMax) * height + heightMenu;
        }
    }
}
