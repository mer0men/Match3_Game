using System;
using System.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Match3Version20
{  
       
    public class TPieces
    {
        public int X, Y, Row, Col, Kind, Posneedx, Posneedy;

        public bool Selected, Swaped, Count;

        public Image Image;

        public void PaintGems(object sender, PaintEventArgs e,  TPieces title)
        {
            e.Graphics.DrawImage(title.Image, new Point(title.X, title.Y));
            if (title.Selected == true)
            {
                e.Graphics.DrawRectangle(Pens.Black, title.X, title.Y, 64, 64);
            }
        }

        public TPieces()
        {
            X = 0;
            Y = 0;
            Row = 0;
            Col = 0;
            Kind = 0;
            Count = false;
            Image = Properties.Resources.gem1;
            Selected = false;
            Swaped = false;
            Posneedx = X;
            Posneedy = Y;

        }
    }
}
