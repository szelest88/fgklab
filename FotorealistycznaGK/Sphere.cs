using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    class Sphere:Primitive
    {
        Vector sphereCenter;
        float sphereRadius;

        public float SphereRadius
        {
            get { return sphereRadius; }
            set { sphereRadius = value; }
        }

        public Vector SphereCenter
        {

            get { return sphereCenter; }
            set { sphereCenter = value; }
        }
    
        public Sphere()
        {

            this.sphereCenter = new Vector(0, 0, 0);
            this.sphereRadius = 1;
        }

        public Sphere(Vector v, float radius)
        {

            this.sphereCenter = v;
            this.sphereRadius = radius;

        }

        public override string ToString()
        {
            return ("Jestem sferą o środku w (x = " + this.SphereCenter.X.ToString() + ", y = " + this.SphereCenter.Y.ToString() + ", z = " + this.SphereCenter.Z.ToString()+") i promieniu "+this.sphereRadius+"\n");
        }

        // Funkcja znajdujaca punkt przeciecia promienia ze sfera

        /*
         * Funkcja docelowo powinna zwracać Vector, ale poki eksperymentujemy to niech zostanie void
        */

        public override void findIntersection(Ray r)
        {

            Vector d = new Vector(r.direction);
            Vector o = new Vector(r.origin);
            Vector c = this.SphereCenter;
            float delta, A, B;
            double sqrtDelta;
                       
            A = d.dot(d);
            B = 2 * (d.dot(o - c));
            delta = B * B - 4 * (A * ((o - c).dot(o - c) - this.SphereRadius * this.SphereRadius));
            sqrtDelta = Math.Sqrt(delta);
             

            if (delta > 0)
            {

                float t1 = ((-B - (float)sqrtDelta) / (2 * A));
                float t2 = ((-B + (float)sqrtDelta) / (2 * A));
                
                /*
                 * Tak mysle, czy zostawic te dwa punkty, czy po prostu machnac jeden jakis p (poza petla) i pozniej go w petli obliczac i zwracac returnem
                */
                Vector p1 = new Vector((r.origin.X + t1 * r.direction.X), (r.origin.Y + t1 * r.direction.Y), (r.origin.Z + t1 * r.direction.Z));
                Vector p2 = new Vector((r.origin.X + t2 * r.direction.X), (r.origin.Y + t2 * r.direction.Y), (r.origin.Z + t2 * r.direction.Z));
                
                //System.Console.WriteLine("Promien przecina sfere w dwoch punktach: (" + p1.X + ", " + p1.Y + ", " + p1.Z + ") i (" + p2.X + ", " + p2.Y + ", " + p2.Z + ").");
                //znajdowanie bliższego punktu
                /*
                 * Tu musi być jakis dodatkowy warunek, kurcze no trza sprawdzic czy aby nie mamy pkt za plecami - najlepiej jest to sprawdzac na podstawie promienia w pocz gdzies w kuli np. (0,0,8) czy cos
                */
                //dziwnie prosto to wyszło, aczkolwiek sprawdzam, czy przecięcie nie jest "za plecami" obserwatora
                //chyba wystarczy do tego parametr t
                //chyba brakuje opcji t1>0,t2<0 -> t2<t1, zaraz jakoś to zasymuluję.
                /* mały eksperyment z eliminacją błędnych punktów
                if ((t1 < t2)) //czy p1 bliższe
                {
                    
                    //  Tu chyba bedziemy an zyca obliczac wczesniej zadeklarowany p i go returniemy.
                    
                    if (t1 > 0)//czy nie za plecami
                        System.Console.WriteLine(p1.ToString());
                }
                else if (t2 > 0) //jeśli drugie bliższe, to czy nie za plecami
                {

                    System.Console.WriteLine(p2.ToString());
                }
                else //oba są za plecami
                    System.Console.WriteLine("plecy");
                */
                //AM nie podoba mi się ten kod, taka ilość ifów to nie jest to, co
                //tygryski lubią najbardziej, w kontekście programu, który wymaga
                //bądź co bądź dużej wydajności. Ale tak jest skutecznie.
                //Jeśli masz jakieś pomysły na optymalizację, byłoby fajnie ;)
                //Jeśli nie - w tej chwili po prostu działa ;)
                if (t1 > 0)
                {
                    if (t2 > 0)
                    {
                        if (t1 < t2)
                            System.Console.WriteLine(p1.ToString());
                        else
                            System.Console.WriteLine(p2.ToString());
                    }
                    else
                        System.Console.WriteLine(p1.ToString());
                }
                else if (t2 > 0)
                {
                    System.Console.WriteLine(p2.ToString());
                }
                else
                    System.Console.WriteLine("nie wykryto");
            }
            

            else if (delta == 0)
            {

                float t3 = (-B / (2 * A));
                Vector p3 = new Vector((r.origin.X + t3 * r.direction.X), (r.origin.Y + t3 * r.direction.Y), (r.origin.Z + t3 * r.direction.Z));
                if (t3 > 0)
                {
                    System.Console.WriteLine("Promien jest styczny do sfery w punkcie: (" + p3.X + ", " + p3.Y + ", " + p3.Z + ")");
                }
                else
                    System.Console.WriteLine("plecy");
            }

            else
            {

                System.Console.WriteLine("Promien nie ma punktow wspolnych ze sfera.");
            }

        }
    }

    
}
