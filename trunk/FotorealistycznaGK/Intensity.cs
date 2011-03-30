using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    class Intensity
    {
        double r;
        double g;
        double b;

        public double R {
            get { return r; }
            set { r = value; }
        }

        public double G
        {
            get { return g; }
            set { g = value; }
        }

        public double B
        {
            get { return b; }
            set { b = value; }
        }

        #region Constructors

        public Intensity() 
        {
            this.R = 0;
            this.G = 0;
            this.B = 0;
        }

        public Intensity(double r, double g, double b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        #endregion Constructors

    }
}
