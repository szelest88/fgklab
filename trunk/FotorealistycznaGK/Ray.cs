using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    class Ray
    {
        public Vector origin;
        public Vector direction;
  
        public Ray(Vector origin, Vector direction) {

            this.origin = new Vector (origin);
            this.direction = direction;
  
        }
        public override string ToString()
        {
            return "Promien ma poczatek w punkcie (" + this.origin.X.ToString() + "," + this.origin.Y.ToString() + "," + this.origin.Z.ToString() + ")";
        }

        
    }
}
