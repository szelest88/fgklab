using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    class Material
    {
        public bool isMirror = false;
        public bool isRefractive = false;
        float specularCoefficient; // wsp.swiatla odbijanego zwierciadlanie
        public Texture texture;
        public Boolean hasTexture = false;
        public float bounceProbability = 0.9f;

        #region Properties

        public float SpecularCoefficient
        {
            get { return specularCoefficient; }
            set { specularCoefficient = value; }
        }
        float ambientCoefficient;  // wsp. odbicia swiatla srodowiska

        public float AmbientCoefficient
        {
            get { return ambientCoefficient; }
            set { ambientCoefficient = value; }
        }
        float diffuseCoefficient;  // wsp. odbicia swiatla rozproszonego

        public float DiffuseCoefficient
        {
            get { return diffuseCoefficient; }
            set { diffuseCoefficient = value; }
        }
        float alpha;                      // wsp. gladkosci powierzchni (tzw. shininess)

        public float Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }
        #endregion Properties

        public Material()
        {
            this.specularCoefficient = 0.75f;
            this.ambientCoefficient = 0.0f;
            this.diffuseCoefficient = 0.75f;
            this.alpha = 15.0f; //15.0f ok
            this.hasTexture = false;
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
