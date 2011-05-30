using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System;

namespace FotorealistycznaGK
{
    class Photon //zmieniłem na class, kompilator mówi, że
        //"Struct cannot contain explicit parameterless constructors"
    {
        Vector position; //pozycja fotonu
        Intensity intensity; // energia fotonow
        public float[] direction; // kierunek nadejscia fotonu; mozna ew da. Vector direction
        
        #region Properties

        public Vector Position
        {
            get { return position; }
            set { position = value; }
        }

        public Intensity Intensity
        {
            get { return intensity; }
            set { intensity = value; }
        }

        #endregion Properties

        #region Constructors

        public Photon() {

            this.position = new Vector(0, 0, 0);
            this.intensity = new Intensity(0,0,0);
            this.direction = new float[3];
        }

        public Photon(Vector position, Intensity intensity, Vector direction) {

            this.position = position;
            this.intensity = intensity;
            this.direction = new float[3];
            this.direction[0] = direction.X;
            this.direction[1] = direction.Y;
            this.direction[2] = direction.Z;
        }

        #endregion Constructors

        // funkcja losujaca kierunek wyslania fotona
        public Vector findRandomDirection() {

            Random rand = new Random();
            Vector v = new Vector();

            v.X = (float)(rand.NextDouble() * 2 - 1);
            v.Y = (float)(rand.NextDouble() * 2 - 1);
            v.Z = (float)(rand.NextDouble() * 2 - 1);

            return v;
        }

        // funkcja sledzaca foton
        void tracePhoton(Vector direction, Photon[] map, Light light)
        {

            Vector start = light.Position;
            Random rand = new Random(); // przyda sie do prawdopodobienstwa

            // tu musi być jakiś odpowiednik findIntersection()
            // i teraz w zaleznosci od materialu dajemy
            // zapisujemy do mapy map[0] = new Photon(pkt przeciecia, intensity i to nasze direcion (SKAD))
            // paczamy ktory ksztalt i jakie ma wlasciwosci i teraz w zaleznosci od materialu i wsp. prawdopodobienstwa
            // losujemy wsp. prawdopodobienstwa --> tylko do dyfuzyjnego
        }

        // funkcja emitujaca fotony ze zrodla punktowego
        void sendPhoton(int numberOfPhotons, Light light, Photon[] map) {

            // numberOfPhotons mozna tez pobrac wielkosc mapy fotonowej :> Ale poki co niech tak zostanie jak jest.
            int ne = 0; //liczba wyemitowanych fotonow
            Vector d = new Vector(); // wektor kierunkowy wmisji fotonua

            while (numberOfPhotons != ne) {

                do { d = findRandomDirection(); }
                while (Math.Pow(d.X, 2) + Math.Pow(d.Y, 2) + Math.Pow(d.Z, 2) > 1);
            }

            tracePhoton(d, map, light); // nie wiem dokładnie co tu ze swiatlem czy nie powinno byc w tej funkcji ale sie zobaczy :>
            ne = ne + 1;
        }

        /*
         * przeskaluj energię wszystkich zapisanych fotonów przez 1/ne
         * 
         * jeszcze nie mam pomyslu -_-
         */
    }

}
