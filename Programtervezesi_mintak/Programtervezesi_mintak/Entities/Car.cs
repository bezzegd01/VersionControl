using Programtervezesi_mintak.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programtervezesi_mintak.Entities
{
    public class Car : Toy
    {
        protected override void DrawImage(Graphics g)
        {
            //Image i = Image.FromFile("Images/car.png");
            g.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width + 5, Height);

        }

    }
}
