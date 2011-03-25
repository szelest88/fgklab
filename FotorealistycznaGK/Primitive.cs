using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    abstract class Primitive
    {
        abstract public Vector findIntersection(Ray r);
    }
}
