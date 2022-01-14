using Programtervezesi_mintak.Abstractions;
using Programtervezesi_mintak.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programtervezesi_mintak
{
    public partial class Form1 : Form
    {
        List<Toy> _toys = new List<Toy>();
        private iToyFactory toyFactory;
        Toy _nextToy;        
        public iToyFactory factory
        {
            get { return toyFactory; }
            set { 
                toyFactory = value;
                DisplayNext();
            }
        }

        private void DisplayNext()
        {
            if (_nextToy != null)
                Controls.Remove(_nextToy);
            _nextToy = factory.CreateNew();
            _nextToy.Top = label1.Top + label1.Height + 20;
            _nextToy.Left = label1.Left;
            Controls.Add(_nextToy);
        }

        public Form1()
        {
            InitializeComponent();
            factory = new BallFactory() { BallColor = btnColor.BackColor };

        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            Toy b = toyFactory.CreateNew();
            _toys.Add(b);
            b.Left = -b.Width;
            mainPanel.Controls.Add(b);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            if (_toys.Count == 0) return;
            Toy lastToy = _toys[0];

            foreach (Toy item in _toys)
            {
                item.MoveToy();
                if (item.Left > lastToy.Left) lastToy  = item;
                if (lastToy.Left>1000)
                {
                    _toys.Remove(lastToy);
                    mainPanel.Controls.Remove(lastToy);
                }
            }
        }

        private void btnBall_Click(object sender, EventArgs e)
        {
            factory = new BallFactory() { BallColor = btnColor.BackColor };
        }

        private void btnCar_Click(object sender, EventArgs e)
        {
            factory = new CarFactory();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            Button kattintott = (Button)sender;
            ColorDialog cd = new ColorDialog();
            cd.Color = kattintott.BackColor;
            if (cd.ShowDialog() != DialogResult.OK) return;
            kattintott.BackColor = cd.Color;
        }
    }
}
