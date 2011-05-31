using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    // stworzyłem nową klasę, bo w ten rendering gdzieś się musi odbyć, a w sumie
    // tak będzie chyba czytelniej to będzie. Rozumiem, że 
    // sama mapa fotonów = tablica fotonów
    class PhotonMapPerspCamera: PerspectiveCamera
    {
        int photonCount; // ilość tegesów
        float radius; //promień wyszukiwania
        //każdy foton ma od 0 do 1 i średnia?
        Photon[] map;
        public void createMap(int photonCount, Light light, List<Primitive> scene, int bounces) 
                                                //po utworzeniu obiektu, wywoła się
                                                //to - funkcja zapełni tablicę
                                                //fotonami
        {
            //tutaj rzucamy w scenę fotonami ze śródła
            // i zapamiętujemy miejsca trafień, odbijając fotony max bounces razy
            // gdzie wywołać tą funkcję? chyba niżej
            Random random = new Random();
            map = new Photon[photonCount];
            int indexer = 0;
            for (int i = 0; i < photonCount; i++) // coś tu powaliłem
            {
                System.Console.WriteLine("foton" + i + "/" + photonCount);
                Ray photonDir = new Ray(light.Position,new Vector((float)random.NextDouble(),(float)random.NextDouble(),(float)random.NextDouble()));
                //tworzymy promień związany z fotonem
                foreach (Primitive pr in scene) //i sprawdzamy, w co trafia. Punkt trafienia zapisujemy w mapie (chwilowo olewamy rekursję)
                {
                    if (pr.findIntersection(photonDir).X != float.PositiveInfinity)
                    {
                        map[indexer] = new Photon(pr.findIntersection(photonDir), light.Color, photonDir.direction);
                        indexer++; break;
                    }else
                    {
                        map[indexer] = new Photon(pr.findIntersection(photonDir), new Intensity(0,0,0), photonDir.direction);
                        indexer++; break;
                    }

                }
            }
        }
        public PhotonMapPerspCamera(int photonCount, float radius, int bounces,
             
            //i parametry dla konstruktora klasy bazowej:
            float w, float h, int pixelsPerUnit, Vector position, Vector target, Vector up, float alpha, List<Primitive> scene, Light light, Uri renderTarget
            ):base(w,h,pixelsPerUnit,position,target,up,alpha,scene,light,renderTarget) //wywołanie konstruktora klasy bazowej
        {
            
            this.photonCount = photonCount;
            this.radius = radius;
            map = new Photon[photonCount];
            //tu wywołamy funkcję powyższą na rzecz parametru map
            createMap(photonCount, light, scene, bounces); //tzn. od razu w konstruktorze generujemy mapę

        }
        public override void renderScene()
        {
            //po utworzeniu kamery (w konstruktorze renderujemy mapę),
            //napierdalamy klasycznie promieniami - dla każdego przeszukujemy mapę
            //w promieniu radius, liczymy średnią i zwracamy shita
            
            //do ustalania kierunku promieni, skopiowane z PerspectiveCamera
            //przepisuję właśnie kod z Persp. Camera, nie jest to jakieś ekonomiczne,
            //(bo kod będzie się powtarzał), ale detale będą inne, a nie chce mi się
            //dzielić tamtego renderScene'a na funkcje
            //zaraz zrobię z tego jakiś region // !!!!!!!!!!!!!!!!!
            Image img = new Image(400, 400);
            
            Vector observer = this.Position;
            Vector srodek = this.Position + (this.Target - this.Position).normalizeProduct() * near;
            float s = near * (float)Math.Tan((alpha / 180.0 * Math.PI) / 2.0);
            Vector prostopadlyPrzes =
               ((this.Target - this.Position).cross(this.Position - this.Up)).normalizeProduct();//cross(this.Positon-this.Up)
            Vector pionPrzes = (this.Up).normalizeProduct();//; *1.5f;//(this.Up-this.Position).nor
             float krok = s * 2.0f / 400.0f;
            Vector poczatek = srodek - prostopadlyPrzes * s - pionPrzes * s;
            Intensity[,] res = new Intensity[400, 400];
            for (int i = 0; i < 400; i++)
                for (int j = 0; j < 400; j++)
                    res[i, j] = new Intensity();

            for (int i = 0; i < 400; i++)
            {
                System.Console.WriteLine("Rendering:" + i + "/400");
                for (int j = 0; j < 400; j++)
                {
                    Ray napierdalacz = new Ray(this.Position,
                        poczatek +
                        i * krok * pionPrzes +
                        j * krok * prostopadlyPrzes);
                    foreach (Primitive p in scene)
                    {
                        //dodać kontrolę depthbuffera
                        Vector intersection = p.findIntersection(napierdalacz);
                        if (intersection.X != float.PositiveInfinity)
                        {
                            //teraz trzeba rozejrzeć się po okolicy (sfera), zliczyć
                            //fotony w określonym promieniu i obliczyć ich średnią czy coś takiego
                            //po czym ją zwrócić. W sumie nie wygląda na jakiś mega hardkor,
                            //chociaż mój Atom się na mnie obrazi - O(N^4) będzie boleć.
                            int count = 0;
                            double sumR = 0, sumG = 0, sumB = 0;
                            foreach (Photon ph in map)
                            {
                                if (ph.Position.countVectorDistance(intersection) < radius)
                                {
                                    count++;
                                    sumR += ph.Intensity.R;
                                    sumG += ph.Intensity.G;
                                    sumB += ph.Intensity.B;

                                }

                            }
                            sumR /= (float)count;// ((float)(Math.PI * radius * radius));
                            sumG /= (float)count; //((float)(Math.PI * radius * radius));
                            sumB /= (float)count; //((float)(Math.PI * radius * radius));
                            res[i, j] = new Intensity(sumR, sumG, sumB);
                            //wrzucić do tablicy z rezultatem sumę podzieloną przez pi R kwadrat
                            img.setPixel(i, j, new Intensity(sumR, sumG, sumB)); //dla każdego prymitywu, wziąć pod uwagę depthBuffer!
                        }

                    }

                }
            }
            //jebnąć res do pliku. Gott mit uns!
            img.obraz.Save(renderTarget);
        }
    }
}
