using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FotorealistycznaGK
{
    class Image
    {
        int xsize, ysize;
        Bitmap obraz;

        public int XSize
        {

            get { return xsize; }
            set { xsize = value; }
        }

        public int YSize
        {

            get { return ysize; }
            set { ysize = value; }
        }

        public Image(int x, int y) {

            this.XSize = x;
            this.YSize = y;
            this.obraz = new Bitmap(this.XSize, this.YSize, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }

        public void setPixel(int x, int y, Intensity pixel)
	    {
	        Color color = Color.FromArgb((int)(pixel.R*255), (int)(pixel.G*255), (int)(pixel.B*255));
	        obraz.SetPixel(x,y,color);
	    }
    }       
}

