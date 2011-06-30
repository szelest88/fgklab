//LOKALNE nana
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    class PointLight:Light
    {

        // strony 16-18

        public PointLight()
        {
            
        }
        public PointLight(Vector position, int energy)
        {
            this.Position = position;
            this.energy = energy;
        }
        public void generujFotony(int ile, ref int trafione, ref Photon[] tab, List<Primitive> scene)
        {
            trafione = 0;
            Random rbis; //do odbitych
            Ray R2;
            Vector dirbis;
            for (int i = 0; i < ile; i++)
            {
                Random rand = new Random();
                Vector dir = new Vector();

                dir.X = (float)(rand.NextDouble() * 2 - 1);
                dir.Y = (float)(rand.NextDouble() * 2 - 1);
                dir.Z = (float)(rand.NextDouble() * 2 - 1);
                Ray R = new Ray(this.Position, this.Position + dir);
                float odlOdTrafienia=float.PositiveInfinity;
                Vector traf=null;
                Intensity kolorObiektu=null;
                foreach (Primitive p in scene)
                {
                    if (p.findIntersection(R).X != float.PositiveInfinity && 
                        p.findIntersection(R).countVectorDistance(this.Position)<odlOdTrafienia)
                    {
                        odlOdTrafienia = p.findIntersection(R).countVectorDistance(this.Position);
                        traf = p.findIntersection(R);
                        kolorObiektu = new Intensity((((float)p.Texturize(p.findIntersection(R)).R) / 255.0f),
                            (((float)p.Texturize(p.findIntersection(R)).G) / 255.0f),
                            (((float)p.Texturize(p.findIntersection(R)).B) / 255.0f));
                        #region ruletka //?
                        rbis = new Random();
                        dirbis = new Vector();
                        dirbis.X = (float)(rbis.NextDouble() * 2 - 1);
                        dirbis.Y = (float)(rbis.NextDouble() * 2 - 1);
                        dirbis.Z = (float)(rbis.NextDouble() * 2 - 1);
                        dirbis += p.normal(p.findIntersection(R));
                        R2 = new Ray(p.findIntersection(R), dirbis);
                        foreach (Primitive p2 in scene)
                        {
                            if (p2.findIntersection(R2).X != float.PositiveInfinity
                               )
                            {
                                System.Console.WriteLine("pac");
                                kolorObiektu = new Intensity((((float)p2.Texturize(p2.findIntersection(R2)).R) / 255.0f),
                            (((float)p2.Texturize(p2.findIntersection(R2)).G) / 255.0f),
                            (((float)p2.Texturize(p2.findIntersection(R2)).B) / 255.0f));
                            }

                        }
                        #endregion ruletka
                        //dodać ruletkę: jakieś 0.2 i promień stąd w hemisferę. Jeśli trafia,
                    }
                }
                //w traf jest najbliższa intersekcja promienia z obiektem
                if (traf != null && kolorObiektu != null)
                {
                    tab[trafione] = new Photon(traf, kolorObiektu, dir);
                    trafione++;
                    System.Console.WriteLine("kolejny foton w tablicy!");
                }

            }
        }
    }
}
