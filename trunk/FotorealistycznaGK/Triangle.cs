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

        public Triangle(Vector a, Vector b, Vector c, Intensity inten, Material material)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            ((Primitive)this).color = inten;
            this.material = material;
        }

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
           // float gMfM = gM * fM;
            float bMlM_kMcM = bM * lM - kM*cM;
            float eMIm_hMfM = eM * iM - hM*fM;
            float gMfM_dMiM = gM * fM - dM * iM;
            float aMkM_jMbM = aM * kM - jM * bM;
            float jMcM_aMlM = jM * cM - aM * lM;
            float dMhM_eMgM = dM * hM - eM * gM;

            float M = aM * eMIm_hMfM + bM * gMfM_dMiM + cM * dMhM_eMgM;

            if (M == 0) return new Vector(
               float.PositiveInfinity,
               float.PositiveInfinity,
               float.PositiveInfinity); //obie kolizje z tyłu. Kurwa mać jebana.
            //przepraszam, ale przez błąd w tym miejscu
            //zarwałem dwie noce

            float beta = jM * eMIm_hMfM + kM * gMfM_dMiM + lM * dMhM_eMgM;
            beta /= M;
            float gamma = iM * aMkM_jMbM + hM * jMcM_aMlM + gM * bMlM_kMcM;
            gamma /= M;
            float t = fM * aMkM_jMbM + eM * jMcM_aMlM + dM * bMlM_kMcM;
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
