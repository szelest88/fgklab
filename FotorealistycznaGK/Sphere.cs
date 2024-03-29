﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FotorealistycznaGK
{
    class Sphere : Primitive
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

        public Sphere(Vector v, float radius, Intensity inten, Material material)
        {

            this.sphereCenter = v;
            this.sphereRadius = radius;
            ((Primitive)this).color = inten;
            this.material = material;
        }

        public override string ToString()
        {
            return ("Jestem sferą o środku w (x = " + this.SphereCenter.X.ToString() + ", y = " + this.SphereCenter.Y.ToString() + ", z = " + this.SphereCenter.Z.ToString() + ") i promieniu " + this.sphereRadius + "\n");
        }

        public override Vector findIntersection(Ray r)
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
                //znajdowanie bliższego punktu:

                if (t1 > 0) //jeśli kolizja nr. 1 jest z przodu...
                {
                    if (t2 > 0) // ...i kolizja nr. 2 też...
                    {
                        if (t1 < t2) //...to sprawdzamy, która jest bliżej...
                            return p1;//i ją...
                        else
                            return p2;//...zwracamy.
                    }
                    else//jeśli zaś tylko kolizja nr. 1 jest z przodu...
                        return p1;//...to ją zwracamy.
                }
                else if (t2 > 0) //natomiast jeśli tylko kolizja 2 jest z przodu, zwracamy
                {
                    return p2; //ją.
                }
                else
                    return new Vector(
                        float.PositiveInfinity,
                        float.PositiveInfinity,
                        float.PositiveInfinity); //obie kolizje z tyłu.
            }


            else if (delta == 0)
            {

                float t3 = (-B / (2 * A));
                Vector p3 = new Vector((r.origin.X + t3 * r.direction.X), (r.origin.Y + t3 * r.direction.Y), (r.origin.Z + t3 * r.direction.Z));
                if (t3 > 0) //wykryta kolizja jest z przodu obserwatora
                {
                    return p3; //jedyna kolizja z przodu
                }
                else
                    return new Vector(
                        float.PositiveInfinity,
                        float.PositiveInfinity,
                        float.PositiveInfinity); //jedyna kolizja z tyłu
            }
            else //ponieważ używamy returnów, ten else jest zbędny, chwilowo zostawię dla czytelności.
            {
                //    System.Console.WriteLine("Promien nie ma punktow wspolnych ze sfera.");
                return new Vector(
                        float.PositiveInfinity,
                        float.PositiveInfinity,
                        float.PositiveInfinity); //jak wyżej
            }

        }

        public override Vector normal(Vector inter)
        {
            return ((inter - this.sphereCenter).normalizeProduct());
        }

        //funkcja teksturujaca sfere,v- jest to wynik z findIntersection
        // i tu lece z instrukcji str. 9
        // wszystko, co jest tu napisane nalezy traktowac jako wielkie CHYBA

        public override Color Texturize(Vector vec) //poprawiłem na override,y...Color?
        {
            if (material.hasTexture)
            {
                vec = vec - this.SphereCenter;
                Color res;
                double theta = Math.Acos(vec.Y);
                double phi = Math.Atan2(vec.X, vec.Z);
                double pi = Math.PI;
                if (phi < 0.0)
                    phi += 2 * pi;

                double u = phi / (2 * pi); //2*pi miast pi?
                double v = 1.0d - theta / pi;
                Image tex = this.material.texture.texture;
                //co to xres, yres (rezultat?) i po co to i jaki to ma zwiazek z colorMap z Texture? :>
                //int column = (int)((xres - 1) * u);     // kolumna jest poziomo 
                //int row = (int)((yres - 1) * v);      // wiersz jest pionowo 
                int column = (int)((tex.XSize - 1) * u);
                int row = (int)((tex.YSize - 1) * v);
                res = tex.obraz.GetPixel(column, row);
                return res;
            }
            return Color.FromArgb(0, (int)(color.R * 255.0), (int)(color.G * 255.0), (int)(color.B * 255.0));
            // else
            //     return Color.Red;
        }
        public override string getName()
        {
            return "sphere";
        }

    }



}
