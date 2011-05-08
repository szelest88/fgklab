using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FotorealistycznaGK
{
    class Triangle : Primitive
    {
        public Vector a;
        public Vector b;
        public Vector c;
        //  Material material;
        public Triangle(Vector a, Vector b, Vector c, Intensity inten, Material material)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            ((Primitive)this).color = inten;
            this.material = material;
        }
        float alfa;
        float beta;
        float gamma;
        public float Au, Av, Bu, Bv, Cu, Cv;
        public void setUvCoords(float Au, float Av, float Bu, float Bv, float Cu, float Cv)
        {
            this.Au = Au;
            this.Av = Av;
            this.Bu = Bu;
            this.Bv = Bv;
            this.Cu = Cu;
            this.Cv = Cv;
        }
        public override Vector findIntersection(Ray r)
        {
            Vector o = new Vector(r.origin);
            Vector d = new Vector(r.direction);
            float aM = a.X - b.X;
            float bM = a.Y - b.Y;
            float cM = a.Z - b.Z;
            float dM = a.X - c.X;
            float eM = a.Y - c.Y;
            float fM = a.Z - c.Z;
            float gM = d.X;
            float hM = d.Y;
            float iM = d.Z;
            float jM = a.X - o.X;
            float kM = a.Y - o.Y;
            float lM = a.Z - o.Z;

            float M = aM * (eM * iM - hM * fM) + bM * (gM * fM - dM * iM) + cM * (dM * hM - eM * gM);

            if (M == 0) return new Vector(
               float.PositiveInfinity,
               float.PositiveInfinity,
               float.PositiveInfinity); //obie kolizje z tyłu. Kurwa mać jebana.
            //przepraszam, ale przez błąd w tym miejscu
            //zarwałem dwie noce

            float beta = jM * (eM * iM - hM * fM) + kM * (gM * fM - dM * iM) + lM * (dM * hM - eM * gM);
            beta /= M;
            float gamma = iM * (aM * kM - jM * bM) + hM * (jM * cM - aM * lM) + gM * (bM * lM - kM * cM);
            gamma /= M;
            float t = fM * (aM * kM - jM * bM) + eM * (jM * cM - aM * lM) + dM * (bM * lM - kM * cM);
            t /= M;
            if (beta + gamma <= 1 && beta >= 0 && gamma >= 0)
                return new Vector( //WPISAC
                    r.origin + t * d);
            return new Vector(
               float.PositiveInfinity,
               float.PositiveInfinity,
               float.PositiveInfinity); //obie kolizje z tyłu.

        }
        public override Vector normal(Vector inter)
        {
            return ((a - b).cross(a - c)).normalizeProduct();

        }

        public override Color Texturize(Vector vec) //dodałem AM
        {
            if (vec.X != float.PositiveInfinity)
            {
                Color temp = Color.FromArgb(0, (int)(this.color.R * 255.0),
                    (int)(this.color.G * 255.0),
                    (int)(this.color.B * 255.0));
                //this.material.texture = new Texture(@"C:\lenatex.jpg");

                //nadszedł CZAS TEKSTUROWANIA.

                return temp;
            }
            return Color.Black;
        }
        public override string getName()
        {
            return "trójkąt";
        }
    }
}
