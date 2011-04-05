using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    class Material
    {
        double specularCoefficient; // wsp.swiatla odbijanego zwierciadlanie
        double ambientCoefficient;  // wsp. odbicia swiatla srodowiska
        double diffuseCoefficient;  // wsp. odbicia swiatla rozproszonego
        int n;                      // wsp. gladkosci powierzchni (tzw. shininess)

        public Material()
        { 
            this.specularCoefficient = 0.5;
            this.ambientCoefficient = 0.0;
            this.diffuseCoefficient = 0.5;
            this.n= 20;
        }


        /*
         * Jesli wolisz na tabelach to moze byc na tabelach ;)
         * 
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
        */

        // trza dodac pole material do prymitywu
    }
}
