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
        private int oldRecord;


        public FrmMain()
        {
            InitializeComponent();
            snake = new Snake();
            food = new Food();
            gameTimer.Interval = 75;
            gameTimer.Tick += Update;

            if (!File.Exists(@"../../ Record.txt"))
            {
                record = "0 - Play the game to note your record";
                File.WriteAllText(@"../../ Record.txt", $"{record}");
            }
            else
            {
                record = File.ReadAllText(@"../../ Record.txt");
            }
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

            if (BodyCollision())
                Restart();

            if (BorderCollision())
                Restart();

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
            UpdateRecord();
            snake = new Snake();
            food = new Food();
            lblMenu.Visible = true;
            route = 0;
            score = 0;
        }


        private void UpdateRecord()
        {
            oldRecord = int.Parse(record.Split(' ')[0]);
            if (oldRecord < score)
            {
                record = $"{score} - Max record, {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";
                File.WriteAllText(@"../../ Record.txt", $"{record}");
            }
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
            MessageBox.Show(@"Gorgeous game snake
by Anna Beletskaya");
        }


        private void TSMIExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void TSMIRecord_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{record}");
        }
    }
}
