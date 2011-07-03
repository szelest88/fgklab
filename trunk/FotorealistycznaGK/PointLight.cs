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
            bool ok = false; //?????????
            for (int i = 0; i < ile; i++)
            {
                ok = false;//????????
                Random rand = new Random();
                Vector dir = new Vector();

                dir.X = (float)(rand.NextDouble() * 2 - 1);
                dir.Y = (float)(rand.NextDouble() * 2 - 1);
                dir.Z = (float)(rand.NextDouble() * 2 - 1);
                Ray R = new Ray(this.Position, this.Position + dir);
                float odlOdTrafienia=float.PositiveInfinity;
                Vector traf=null;
                Vector traf2=null;
                Intensity kolorObiektu=null;
                Intensity kolorObiektu2 = null;
                foreach (Primitive p in scene)
                {
                    if (p.material.isMirror==false && p.material.isRefractive==false && p.findIntersection(R).X != float.PositiveInfinity &&  //dodałem te 2 false'y
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
                        dirbis += p.normal(p.findIntersection(R));//?????
                        R2 = new Ray(p.findIntersection(R), dirbis);
                        foreach (Primitive p2 in scene)
                        {
                            if (p2.findIntersection(R2).X != float.PositiveInfinity
                               )
                            {
                                traf2 = p2.findIntersection(R2);
                                ok = true;
                                System.Console.WriteLine("odbity");
                                kolorObiektu = new Intensity((((float)p2.Texturize(traf2).R) / 255.0f),
                            (((float)p2.Texturize(traf2).G) / 255.0f),
                            (((float)p2.Texturize(traf2).B) / 255.0f));//sensowniej, tj. dodać też tamto?
                            }

                        }
                        #endregion ruletka
                        //dodać ruletkę: jakieś 0.2 i promień stąd w hemisferę. Jeśli trafia,
                    }
                    else
                        if (p.material.isRefractive == true && p.findIntersection(R).X != float.PositiveInfinity)//próba ugięcia
                        {
                            //wyślij ugięty promień i trafienie zapisz w traf
                            Vector I2a = R.direction.normalizeProduct();
                            Vector N2a = p.normal(p.findIntersection(R));
                            Vector R2a = -N2a * (N2a.dot(I2a) * 1.0f);//1->-2
                            Ray test2a = new Ray(p.findIntersection(R),
                             p.findIntersection(R)- 2* R2a);//I- N *
                            foreach (Primitive p2 in scene)
                            {
                                if (p2.findIntersection(test2a).X != float.PositiveInfinity
                                   )
                                {
                                    traf2 = p2.findIntersection(test2a);
                                    ok = true;
                                    System.Console.WriteLine("odbity");
                                    kolorObiektu2 = new Intensity((((float)p2.Texturize(traf2).R) / 255.0f),
                                (((float)p2.Texturize(traf2).G) / 255.0f),
                                (((float)p2.Texturize(traf2).B) / 255.0f));//sensowniej, tj. dodać też tamto?
                                }

                            }
                        }
                }
                //w traf jest najbliższa intersekcja promienia z obiektem
                if (traf != null && kolorObiektu != null)
                {
                    tab[trafione] = new Photon(traf, kolorObiektu, dir);
                    trafione++;
                    if (ok == true && traf2!=null)
                    {
                        tab[trafione] = new Photon(traf2, kolorObiektu2, dir);
                        trafione++;
                    }
                    System.Console.WriteLine("kolejny foton w tablicy!");
                }

            }
        }
    }
}
