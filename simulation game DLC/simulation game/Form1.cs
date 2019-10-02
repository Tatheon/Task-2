using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simulation_game
{
    public partial class Form1 : Form
    {
        GameEngine game = new GameEngine();
        Timer round = new Timer();
        public Form1()
        {
            InitializeComponent();

            DrawMap();
            ShowUnits();
            initTimer();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        void initTimer()
        {
            round.Interval = 1000;
            round.Tick += TimerTickHandeler;
        }

        void TimerTickHandeler(object sender, EventArgs e)
        {
            game.Run();
            game.world.UpdateWorld();
            DrawMap();
            ShowUnits();

            if (game.gameOver)
            {
                round.Stop();
                lblMap.Text += "Winning team is" + game.WinningTeam;
                btnPause.Enabled = false;
            }

        }

        public void DrawMap()
        {
            lblMap.Text = "";
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    lblMap.Text += game.world.map[x, y];
                }
                lblMap.Text += "\n";
            }
        }

        public void ShowUnits()
        {
            lblPlayerStats.Text = game.Rounds + "\n";
            foreach (Building building in game.world.buildings)
            {
                lblPlayerStats.Text += building.ToString() + "\n";
            }

            foreach (Unit unit in game.world.players)
            {
                lblPlayerStats.Text += unit.ToString() + "\n";
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
             
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            round.Start();
            btnLoad.Enabled = false;
            btnSave.Enabled = false;
        }

        private void btnPause_Click_1(object sender, EventArgs e)
        {
            round.Stop();
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            game.world.SaveWorld();
            lblMap.Text += "Game saved <<<>>>";
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            game.world.LoadWorld();
            DrawMap();
            ShowUnits();
            lblMap.Text += "Map loaded <<<>>>";
        }
    }
}
