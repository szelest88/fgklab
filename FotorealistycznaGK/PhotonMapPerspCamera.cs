//LOKALNE
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
        int ilosc = 0;
        int photonCount; // ilość tegesów
        float radius; //promień wyszukiwania
        //każdy foton ma od 0 do 1 i średnia?
        Photon[] map;
        int effectivePhotonCount = 0;
        float[,] depthBuffer; //NOWE
        public void createMap(int photonCount, Light light, List<Primitive> scene, int bounces) 
                                                //po utworzeniu obiektu, wywoła się
                                                //to - funkcja zapełni tablicę
                                                //fotonami
        {
            //tutaj rzucamy w scenę fotonami ze śródła
            // i zapamiętujemy miejsca trafień, odbijając fotony max bounces razy
            // gdzie wywołać tą funkcję? chyba niżej
            Random random = new Random();
           // map = new Photon[photonCount];
            int indexer = -1;
            ilosc = 0;
            for (int i = 0; i < photonCount; i++) // coś tu powaliłem
            {
                System.Console.WriteLine("foton" + i + "/" + photonCount);
                Ray photonDir = new Ray(light.Position, new Vector((float)(random.NextDouble() * 2 - 1), (float)(random.NextDouble() * 2 - 1), (float)(random.NextDouble() * 2 - 1)));
                //tworzymy promień związany z fotonem
                foreach (Primitive pr in scene)
                {
                    if (pr.findIntersection(photonDir).X != float.PositiveInfinity)
                        ilosc++;
                 
                }
                
            }
            map = new Photon[ilosc];
            for (int i = 0; i < ilosc; i++) // coś tu powaliłem
            {

                Ray photonDir = new Ray(light.Position, new Vector((float)(random.NextDouble() * 2 - 1), (float)(random.NextDouble() * 2 - 1), (float)(random.NextDouble() * 2 - 1)));
                indexer++;
                foreach (Primitive pr in scene) //i sprawdzamy, w co trafia. Punkt trafienia zapisujemy w mapie (chwilowo olewamy rekursję)
                {
                    if (pr.findIntersection(photonDir).X != float.PositiveInfinity)
                    {
                        map[indexer] = new Photon(pr.findIntersection(photonDir), pr.color, photonDir.direction);
                        
                        effectivePhotonCount++;
                        System.Console.WriteLine("Trafił foton"+pr.findIntersection(photonDir));
                    //    break;
                    }else
                    {
               //         map[indexer] = new Photon(pr.findIntersection(photonDir), new Intensity(1,1,1), photonDir.direction);
                      //  indexer++; break;
                    }

                }
            }
        }
        public PhotonMapPerspCamera(float radius,
             
            //i parametry dla konstruktora klasy bazowej:
            float w, float h, int pixelsPerUnit, Vector position, Vector target, Vector up, float alpha, List<Primitive> scene, Light light,Photon[] map, Uri renderTarget
            ):base(w,h,pixelsPerUnit,position,target,up,alpha,scene,light,renderTarget) //wywołanie konstruktora klasy bazowej
        {
            this.w = w;
            this.h = h;
            this.wPix = (int)w * pixelsPerUnit;
            this.hPix = (int)h * pixelsPerUnit;
            widthOfPix = 1.0f / pixelsPerUnit;
            this.alpha = alpha;
            heightOfPix = 1.0f / pixelsPerUnit;
         //   this.photonCount = photonCount;
            this.radius = radius;
            this.map = map;
            this.depthBuffer = new float[400, 400];
            for (int i = 0; i < 400; i++)
                for (int j = 0; j < 400; j++)
                    depthBuffer[i, j] = float.PositiveInfinity;
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
            float krok = s * 4.0f / 400.0f;//2->4
            Vector poczatek = srodek - prostopadlyPrzes * s*2 - pionPrzes * s*2;//nic->*2
            Intensity[,] res = new Intensity[400, 400];
            for (int i = 0; i < 400; i++)
                for (int j = 0; j < 400; j++)
                {
                    res[i, j] = new Intensity();
                    img.setPixel(i, j, new Intensity());
                }

            for (int i = 0; i < 400; i++)
            {
       //         System.Console.WriteLine("dla nex fotonu");
                System.Console.WriteLine("Rendering:" + i + "/400");
                for (int j = 0; j < 400; j++)
                {

                    Ray napierdalacz = new Ray(this.Position,
                        poczatek +
                        i * krok * pionPrzes +
                        j * krok * prostopadlyPrzes);
                    bool traf = false;
                    //int ile=0;
                    int ileFotonow = 0;
                    foreach (Primitive p in scene)
                    {
                     //   System.Console.WriteLine("nono");
                        //dodać kontrolę depthbuffera
                        //intersection powinno uwzględniać głębokość
                        Vector intersection = p.findIntersection(napierdalacz);

         
                        if (intersection.X != float.PositiveInfinity)
                        {
                            float odleglosc = intersection.countVectorDistance(this.Position);
                            if (depthBuffer[i, j] >= odleglosc)
                                depthBuffer[i, j] = odleglosc;
                            if (depthBuffer[i, j] == odleglosc)
                            {
                                //teraz trzeba rozejrzeć się po okolicy (sfera), zliczyć
                                //fotony w określonym promieniu i obliczyć ich średnią czy coś takiego
                                //po czym ją zwrócić. W sumie nie wygląda na jakiś mega hardkor,
                                //chociaż mój Atom się na mnie obrazi - O(N^4) będzie boleć.
                                int count = 0;
                                double sumR = 0, sumG = 0, sumB = 0;
                                //     foreach (Photon ph in map)
                                //     {
                                //  for (int ą = 0; ą < map.; ą++)
                                //  {
                                int ju = 0;
                                // tu coś nie tak chyba
                                // System.Console.WriteLine("Trafiło w obiekt");
                                foreach (Photon pp in map)
                                {
                                    if (pp != null)
                                        if (pp.Position.countVectorDistance(intersection) < radius && p.material.isMirror==false && p.material.isRefractive==false)
                                        {
                                            sumR += (double)((double)p.Texturize(intersection).R / 255.0);
                                            sumG += (double)((double)p.Texturize(intersection).G / 255.0);
                                            sumB += (double)((double)p.Texturize(intersection).B / 255.0);
                                            traf = true;
                                            ileFotonow++;
                                        }
                                    ju++;
                                }
                                res[i, j] = new Intensity(sumR / (double)ileFotonow, sumG / (double)ileFotonow, sumB / (double)ileFotonow);
                                //wrzucić do tablicy z rezultatem sumę podzieloną przez pi R kwadrat
                                if (traf == true)
                                    img.setPixel(i, j, res[i, j]);
                            }//koniec ifa depthBuffer[...]==odleglosc
                           // img.setPixel(i, j, new Intensity(1,0, 0)); //dla każdego prymitywu, wziąć pod uwagę depthBuffer!
                         //   else
                         //       img.setPixel(i, j, new Intensity(0, 0, 0));
                        }

                    }

                }
            }
            //jebnąć res do pliku. Gott mit uns!
            img.obraz.Save(renderTarget);
        }
    }
}
