using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldsHardestGame;

namespace Evulotion_strategy
{
    public partial class Form1 : Form
    {
        GameController gc = new GameController();
        GameArea ga;



        int populationSize = 100; //Kezdő populáció
        int nbrOfSteps = 10; //játékos által megtehető lépések
        int nbrOfStepsIncrement = 10;
        int generation = 1;



        Brain winnerBrain;
        public Form1()
        {
            InitializeComponent();
            startBtn.Visible = false;
            gc.GameOver += Gc_GameOver;


            ga = gc.ActivateDisplay();
            this.Controls.Add(ga);
            for (int i = 0; i < populationSize; i++)
            {
                gc.AddPlayer(nbrOfSteps);
            }
            gc.Start();


//            gc.AddPlayer();
//            gc.Start();


        }

        private void Gc_GameOver(object sender)
        {
            generation++;
            generationLabel.Text = string.Format("{0}. generáció", generation);
            var playerList = (from p in gc.GetCurrentPlayers() 
                              orderby p.GetFitness() descending 
                              select p);
            var topPlayers = playerList.Take(populationSize/2).ToList();

            gc.ResetCurrentLevel();
            foreach (var p in topPlayers)
            {
                var b = p.Brain.Clone();
                if (generation % 3 == 0)
                    gc.AddPlayer(b.ExpandBrain(nbrOfStepsIncrement));
                else
                    gc.AddPlayer(b);

                if (generation % 3 == 0)
                    gc.AddPlayer(b.Mutate().ExpandBrain(nbrOfStepsIncrement));
                else
                    gc.AddPlayer(b.Mutate());
            }
            gc.Start();

            var winners = from p in topPlayers where p.IsWinner select p;
            if (winners.Count()>0)
            {
                winnerBrain = winners.FirstOrDefault().Brain.Clone();
                gc.GameOver -= Gc_GameOver;
                startBtn.Visible = true;
                return;
            }


        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            gc.ResetCurrentLevel();
            gc.AddPlayer(winnerBrain.Clone());
            gc.AddPlayer();
            ga.Focus();
            gc.Start(true);
        }
    }
}
