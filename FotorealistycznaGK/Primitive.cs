using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FotorealistycznaGK
{
    abstract class Primitive
    {
        public Intensity color;
        public Material material;
        abstract public Vector findIntersection(Ray r);
    }
}
