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
        public int x, y, row, col, kind, posneedx, posneedy;

        public bool Selected, Swaped, Count;

        public Image image;

        public void Paint_Gems(object sender, PaintEventArgs e,  TPieces title)
        {
            e.Graphics.DrawImage(title.image, new Point(title.x, title.y));
            if (title.Selected == true)
            {
                e.Graphics.DrawRectangle(Pens.Black, title.x, title.y, 64, 64);
            }
        }

        public TPieces()
        {
            x = 0;
            y = 0;
            row = 0;
            col = 0;
            kind = 0;
            Count = false;
            image = Properties.Resources.gem1;
            Selected = false;
            Swaped = false;
            posneedx = x;
            posneedy = y;
        }
    }
}
