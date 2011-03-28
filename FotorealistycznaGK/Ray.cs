using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    class Ray
    {
        //tu pozamieniałem
        public Vector origin; //origin+direction=destination
        public Vector direction;//direction = destination - origin
        public Vector destination;//destination = origin+direction
  
        public Ray(Vector origin, Vector destination) {

            this.origin = new Vector (origin);
            this.destination = new Vector(destination);
            this.direction = destination - origin;
  
        }
        public override string ToString()
        {
            return "Jestem promieniem o początku w punkcie (" + this.origin.X.ToString() + "," + this.origin.Y.ToString() + "," + this.origin.Z.ToString() + ") i kierunku"+this.direction.ToString();
        }

        
    }
}
