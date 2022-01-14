using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Programtervezesi_mintak.Entities
{
    public class Ball : Abstractions.Toy
    {
        public SolidBrush ballBrush{ get; private set; }

        public  Ball(Color kivalasztottszin)
        {
            ballBrush = new SolidBrush(kivalasztottszin);
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillEllipse(ballBrush, 0, 0, Width, Height);
        }


    }

}
