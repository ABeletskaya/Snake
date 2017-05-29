using System.Collections.Generic;
using System.Drawing;

namespace Snake
{
    class Snake
    {
        public List<Rectangle> Body;
        private int x = 0, y = 25;          
        private const int width = 10, height = 10, xMin = 0, yMin = 0, xMax = 290, yMax = 200, heightMenu = 24;
        private Rectangle Head
        {
            get
            {
                return Body[0];
            }
            set { this.Body[0] = value; }
        }


        public Snake()
        {
            Body = new List<Rectangle>();
            Body.Add(new Rectangle(x, y, width, height));
        }


        public void MoveBody()
        {
            for (int i = Body.Count - 1; i > 0; i--)
                Body[i] = Body[i - 1];
        }


        public void Draw(Graphics graphics)
        {
            graphics.FillRectangles(Brushes.Gray, Body.ToArray());
        }


        public void Move(int direction) // 0 = Право, 1 = Низ, 2 = Лево, 3 = Вверх
        {
            MoveBody();
            switch (direction)
            {
                case 0:
                    Head = new Rectangle(Body[0].X + 10, Body[0].Y, width, height);
                    break;
                case 1:
                    Head = new Rectangle(Body[0].X, Body[0].Y + 10, width, height);
                    break;
                case 2:
                    Head = new Rectangle(Body[0].X - 10, Body[0].Y, width, height);
                    break;
                case 3:
                    Head = new Rectangle(Body[0].X, Body[0].Y - 10, width, height);
                    break;
            }
        }


        public void Grow()
        {
            Body.Add(new Rectangle(Body[Body.Count - 1].X, Body[Body.Count - 1].Y, width, height));
        }


        public bool BodyCollision()
        {
            for (int i = 1; i < Body.Count; i++)
                if (Body[0].IntersectsWith(Body[i]))
                    return true;
            return false;
        }


        public bool BorderCollision()
        {
            if ((Body[0].X < xMin || Body[0].X > xMax) || (Body[0].Y < yMin + heightMenu || Body[0].Y > yMax + heightMenu))
                return true;
            return false;
        }
    }
}
