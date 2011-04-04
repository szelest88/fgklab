using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    class Material
    {
        double[] ambientTable = new double[3];
        double[] diffuseTable = new double[3];
        double[] specularTable= new double[3];
        double alpha;

        public Material()
        {
            for (int i = 0; i < 3; i++)
            {
                this.ambientTable[i] = 0.3f;
                this.diffuseTable[i] = 0.5f;
                this.specularTable[i] = 0.8f;
            }

            this.alpha = 100.0;
        }

        // trza dodac pole material do prymitywu
    }
}
