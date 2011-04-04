using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    class PerspectiveCamera: Camera
    {
        //FUCK!

          /**
         <summary>
         szerokość i wysokość viewportu (nearShit) w jednostkach sceny
         </summary>
         **/
        float w, h;
        /**
         <summary>
         szerokość i wysokość viewportu (nearShit) w pikselach
         </summary>
         **/
        int wPix, hPix;
        /**
         <summary>
         szerokość i wysokość piksela w jednostkach sceny
         </summary>
         **/
        float widthOfPix, heightOfPix;
        /**
         <summary>
         kąt widzenia, w pionie i w poziomie (fov)
         </summary>
         **/ 
        float alpha;
        /**
        <summary>
        odległość takiej niby rzutni ;) to jest bliższa kamerze) płaszczyzna obcinania chyba
         * W każdym razie posługuję się nią przy liczeniu kierunku promieni
        </summary>
        **/ 
        float near;
        List<Primitive> scene;
        string renderTarget; //dodałem, string na ścieżkę do pliku wynikowego
        public PerspectiveCamera(float w, float h, int pixelsPerUnit, Vector position, Vector target, Vector up, List<Primitive> scene, Uri renderTarget)
        {
            this.w = w;
            this.h = h;
            this.wPix = (int)w * pixelsPerUnit; ///POPRAWIIICC!!!!!!!!!!!!!!!!!
            this.hPix = (int)h * pixelsPerUnit;
            widthOfPix = 1.0f / pixelsPerUnit;
            heightOfPix = 1.0f / pixelsPerUnit;

            this.Position = position;
            this.Target = target;
            this.Up = up;
            this.scene = scene;
            this.renderTarget = renderTarget.AbsolutePath;
        }

        public override void renderScene()
        {
            Image img = new Image(400, 400);
            //kurwaaaa
            //trzeba znaleźć współrzędne prostokąta
            Ray napierdalacz;
            Vector prostopadlyPrzes =
                ((this.Target - this.Position).cross(this.Position -this.Up)).normalizeProduct();//cross(this.Positon-this.Up)
            Vector pionPrzes = (this.Up).normalizeProduct();//; *1.5f;//(this.Up-this.Position).nor
            
            Vector observer = this.Position;
            Vector srodek = this.Position + (this.Target - this.Position).normalizeProduct() * near;
            float s = near*(float)Math.Tan((double)alpha/2.0);
            Vector poczatek = srodek - prostopadlyPrzes * s - pionPrzes * s;
            //powyższe to róg (lewy dolny) rzutni ("tylnej płaszczyzny obcinania").
            //teraz trzeba się od niego odsuwać w płaszczyźnie pionPrzes x prostopadlyPrzes
            //co ileśtam (jeszcz nie wiem, ile ;)), i w ten sposób określi się
            //kolejne punkty, które razem z this.Position będą wyznaczały nasze raye.
            //zaraz dokończę
            
            //poniżej to bullshit


            /*
            System.Console.WriteLine(prostopadlyPrzes);
          //  Vector srodek = this.Position;
           
            poczatek -= pionPrzes * (h * 0.5f - heightOfPix * 0.5f);
            System.Console.WriteLine("początek" + poczatek);
            prostopadlyPrzes /= 400.0f;
            pionPrzes /= 400.0f;
            Vector iterator = new Vector(poczatek);
            for (int i = 0; i < 400; i++)
                for (int j = 0; j < 400; j++)
                    img.setPixel(i, j, new Intensity(0, 0, 0));
            for (int i = 0; i < 400; i++)
                for (int j = 0; j < 400; j++)
                {
                    napierdalacz = new Ray(srodek, poczatek + (Target - Position));
                    foreach (Primitive p in scene)
                    {
                        if (p.findIntersection(napierdalacz).X != float.PositiveInfinity)
                            img.setPixel(i, j, p.color); //w tej chwili dany promień, ale nie na rzutni
                    }
                    poczatek = iterator + new Vector(pionPrzes * i + prostopadlyPrzes * j);
                    // poczatek.add(prostopadlyPrzes + pionPrzes);
                    System.Console.WriteLine("pocz:" + poczatek);
                }
            img.obraz.Save(renderTarget);
            System.Console.WriteLine(poczatek);
            */
        }

    }
}
