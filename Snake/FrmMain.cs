using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class FrmMain : Form
    {
        private Timer gameTimer = new Timer();
        private Graphics graphics;
        private GameLogic gameLogic;


        public FrmMain()
        {
            InitializeComponent();
            gameLogic = new GameLogic();
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
                        gameTimer.Enabled = !gameTimer.Enabled;
                    break;
                case Keys.Right:
                    gameLogic.ChangeRoute(0);
                    break;
                case Keys.Down:
                    gameLogic.ChangeRoute(1);
                    break;
                case Keys.Left:
                    gameLogic.ChangeRoute(2);
                    break;
                case Keys.Up:
                    gameLogic.ChangeRoute(3);
                    break;
            }
        }


        private void FrmMain_Paint(object sender, PaintEventArgs e)
        {
            graphics = this.CreateGraphics();
            gameLogic.Draw(graphics);
        }


        private void Update(object sender, EventArgs e)
        {
            this.Text = string.Format($"Score: {gameLogic.score}");

            if (gameLogic.GameActionsAndRestart())
                Restart();

            this.Invalidate();
        }


        private void Restart()
        {
            gameTimer.Stop();
            graphics.Clear(SystemColors.Control);
            lblMenu.Visible = true;
            gameLogic.Restart();
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
                gameTimer.Enabled = !gameTimer.Enabled;
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
            MessageBox.Show(gameLogic.message);
        }
    }
}
