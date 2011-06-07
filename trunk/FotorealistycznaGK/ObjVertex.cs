using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    public class ObjVertex
    {
        public double x, y, z;
        public ObjVertex(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public override string ToString()
        {
            return "Wierzchołek: x = " + x + ", y = " + y + ",z = " + z;
        }
    }
}
