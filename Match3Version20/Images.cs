using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Match3Version20
{
    class Images
    {
        public List<Image> Imglist = new List<Image>();

        public Images()
        {
            Imglist.Add(Properties.Resources.gem1);
            Imglist.Add(Properties.Resources.gem2);
            Imglist.Add(Properties.Resources.gem3);
            Imglist.Add(Properties.Resources.gem4);
            Imglist.Add(Properties.Resources.gem5);
            Imglist.Add(Properties.Resources.x);
        }
       

    }
}
