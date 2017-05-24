using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Snake
{
    public partial class FrmMain : Form
    {
        private int route = 0;
        private int score = 0;
        private Timer gameTimer = new Timer();
        private Graphics graphics;
        private Snake snake;
        private Food food;
        private string record;

        public FrmMain()
        {
            InitializeComponent();
            snake = new Snake();
            food = new Food();
            gameTimer.Interval = 75;
            gameTimer.Tick += Update;
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                    if (lblMenu.Visible)
                    {
                        lblMenu.Visible = false;
                        gameTimer.Start();
                    }
                    break;
                case Keys.Space:
                    if (!lblMenu.Visible)
                        gameTimer.Enabled = (gameTimer.Enabled) ? false : true;
                    break;
                case Keys.Right:
                    if (route != 2)
                        route = 0;
                    break;
                case Keys.Down:
                    if (route != 3)
                        route = 1;
                    break;
                case Keys.Left:
                    if (route != 0)
                        route = 2;
                    break;
                case Keys.Up:
                    if (route != 1)
                        route = 3;
                    break;
            }
        }

        private void FrmMain_Paint(object sender, PaintEventArgs e)
        {
            graphics = this.CreateGraphics();
            snake.Draw(graphics);
            food.Draw(graphics);
        }

        private void Update(object sender, EventArgs e)
        {
            this.Text = string.Format($"Score: {score}");
            snake.Move(route);

            // проверка на коллизию с телом
            if (BodyCollision())
                Restart();
            // проверка на выход за пределы поля
            if (BorderCollision())
                Restart();
            // проверка на пересечение с едой
            if (IntersectFood())
            {
                score++;
                snake.Grow();
                food.Generate();
            }
            this.Invalidate();
        }

        private bool BodyCollision()
        {
            for (int i = 1; i < snake.Body.Length; i++)
                if (snake.Body[0].IntersectsWith(snake.Body[i]))
                    return true;
            return false;

        }

        private bool BorderCollision()
        {
            if ((snake.Body[0].X < 0 || snake.Body[0].X > 290) || (snake.Body[0].Y < 24 || snake.Body[0].Y > 216))
                return true;
            return false;
        }

        private bool IntersectFood()
        {
            if (snake.Body[0].IntersectsWith(food.Canary))
                return true;
            return false;
        }

        private void Restart()
        {
            gameTimer.Stop();
            graphics.Clear(SystemColors.Control);
            snake = new Snake();
            food = new Food();
            route = 0;
            score = 1;
            lblMenu.Visible = true;
        }

        private void TSMINewGame_Click(object sender, EventArgs e)
        {
            Restart();
            if (lblMenu.Visible)
            {
                lblMenu.Visible = false;
                gameTimer.Start();
            }
        }

        private void TSMIPause_Click(object sender, EventArgs e)
        {
            if (!lblMenu.Visible)
                gameTimer.Enabled = (gameTimer.Enabled) ? false : true;
        }
        
        private void TSMIAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Amazing game snake
by Anna Beletskaya");
        }
        
        private void TSMIExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
