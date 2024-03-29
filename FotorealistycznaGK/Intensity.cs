﻿using System;
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

        #region Properties

        public double R
        {
            get { return r; }
            set { if (r > 1) r = 1; else if (r < 0) r = 0; else r = value; }
        }

        public double G
        {
            get { return g; }
            set { if (g > 1)g = 1; else if (g < 0) g = 0; else g = value; }
        }

        public double B
        {
            get { return b; }
            set { if (b > 1) b = 1; else if (b < 0) b = 0; else b = value; }
        }

        #endregion Properties

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

        public Intensity(Intensity I)
        {
            this.R = I.R;
            this.G = I.G;
            this.B = I.B;
        }

        #endregion Constructors

        #region Methods

        public void addValues(double r, double g, double b)
        {
            //hm...
            if (R + r <= 1)
            {
                if (R + r >= 0)
                {
                    this.R += r;
                }
                else this.R = 0;
            }
            else
                this.R = 1;
            //hm...
            if (G + g <= 1)
            {
                if (G + g >= 0)
                {
                    this.G += g;
                }
                else this.G = 0;
            }
            else
                this.G = 1;
            //hm...
            if (B + b <= 1)
            {
                if (B + b >= 0)
                {
                    this.B += b;
                }
                else this.B = 0;
            }
            else
                this.B = 1;
          //  this.B += b;
        }

        public void divide(int p)
        {
            this.R /= p;
            this.G /= p;
            this.B /= p;
        }
        public override string ToString()
        {
            return ("res"+this.R+","+this.G+","+this.B);
        }
        #endregion Methods

    }
}
