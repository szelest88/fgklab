//SVN
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
        Vector direction; // kierunek nadejscia fotonu; mozna ew da. Vector direction
        
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
            this.intensity = new Intensity(1,0,0);
            this.direction = new Vector();
        }

        public Photon(Vector position, Intensity intensity, Vector direction) {

            this.position = position;
            this.intensity = intensity;
            this.direction = direction;
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
        void tracePhoton(Vector direction, Photon[] map, Vector start,ref int index, List<Primitive> scene, int depth)
        {
            
            if (depth != 0)
            {
       //         System.Console.WriteLine("tracephoton wywołuje się dla depth!=0");
                //Vector start = light.Position;
                Random rand = new Random(); // przyda sie do prawdopodobienstwa

                this.direction = direction;
                this.position = start;

                Ray r = new Ray(start, direction);

                foreach (Primitive pr in scene)
                {
                    Vector where = pr.findIntersection(r);
                //    System.Console.WriteLine("NIE JEBŁO");
                    if (where.X != float.PositiveInfinity)
                    {
                        System.Console.WriteLine("JEBŁO");
                        if (!(index >= map.Length))
                        {
                            map[index] = new Photon(this.position, pr.color, this.direction);
                            index++;
                        }
                        // tutaj trza zobaczyc material
                        if (!pr.material.isMirror && !pr.material.isRefractive)
                        {
                            if (rand.NextDouble() < (double)pr.material.bounceProbability)
                            {
                                System.Console.WriteLine("odbicie dla diffuse");

                                this.position = where;
                                this.direction = findRandomDirection();

                                depth = depth-1;
                                tracePhoton(this.direction, map, this.position, ref index, scene, depth);

                            }
                        }
                        else depth = 0;  break;
                    }
                }

            }


            // tu musi być jakiś odpowiednik findIntersection()
            // i teraz w zaleznosci od materialu dajemy
            // zapisujemy do mapy map[x] = new Photon(pkt przeciecia, intensity i to nasze direcion (SKAD))
            // paczamy ktory ksztalt i jakie ma wlasciwosci i teraz w zaleznosci od materialu i wsp. prawdopodobienstwa
            // losujemy wsp. prawdopodobienstwa --> tylko do dyfuzyjnego
        }

        // funkcja emitujaca fotony ze zrodla punktowego
        // czyli dla każdego 
       public  void sendPhoton(int numberOfPhotons, Vector start, ref Photon[] map, ref int index, List<Primitive> scene, ref int depth, int ne)
        {

            // numberOfPhotons mozna tez pobrac wielkosc mapy fotonowej :> Ale poki co niech tak zostanie jak jest.
            // int ne = 0; //liczba wyemitowanych fotonow
            Vector d = new Vector(); // wektor kierunkowy emisji fotona

                do { d = findRandomDirection(); }
                while (Math.Pow(d.X, 2) + Math.Pow(d.Y, 2) + Math.Pow(d.Z, 2) > 1);

            tracePhoton(d, map, start,ref index, scene, depth); // nie wiem dokładnie co tu ze swiatlem czy nie powinno byc w tej funkcji ale sie zobaczy :>
            ne = ne + 1; // czy to sa wyemitowane?
        }

        /*
         * przeskaluj energię wszystkich zapisanych fotonów przez 1/ne
         * 
         * jeszcze nie mam pomyslu -_-
         */
    }

}
