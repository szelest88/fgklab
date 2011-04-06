using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    class PerspectiveCamera : Camera
    {
        /*
         * by AM - tutaj będą te komentarze, za co przepraszam, ale ta klasa jest niefajna i sam 
         * się w niej gubię. Większość z nich można zredukować (minusik po lewej), 
         * ale niektórych się nie da, bo są za głęboko.
         * 
         * Na samym końcu jest zasadniczy rendering ( for > for > foreach ) i to jest miejsce, 
         * w którym będzie trzeba pewnie pobawić się ze światłami - chwilowo jest tam zakomentowana
         * wersja, chyba niedokończona.
         * 
         * Ten komentarz of course możesz wykasować, jak przeczytasz ;)
         */

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
         kąt widzenia, w pionie i w poziomie (fov) (jest jeden, bo chwilowo będziemy rzutować - dla
         uproszczenia - na kwadrat
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
        string renderTarget; // string na ścieżkę do pliku wynikowego
        public PerspectiveCamera(float w, float h, int pixelsPerUnit, Vector position, Vector target, Vector up, float alpha, List<Primitive> scene, Uri renderTarget)
        {
            this.w = w;
            this.h = h;
            this.wPix = (int)w * pixelsPerUnit;
            this.hPix = (int)h * pixelsPerUnit;
            widthOfPix = 1.0f / pixelsPerUnit;
            this.alpha = alpha;
            heightOfPix = 1.0f / pixelsPerUnit;

            this.Position = position;
            this.Target = target;
            this.Up = up;
            this.scene = scene;
            this.renderTarget = renderTarget.AbsolutePath;
            this.near = 0.2f;
        }

        public override void renderScene()
        {
            Image img = new Image(400, 400);
            Ray napierdalacz;
            Vector prostopadlyPrzes =
                ((this.Target - this.Position).cross(this.Position - this.Up)).normalizeProduct();//cross(this.Positon-this.Up)
            Vector pionPrzes = (this.Up).normalizeProduct();//; *1.5f;//(this.Up-this.Position).nor

            Vector observer = this.Position;
            Vector srodek = this.Position + (this.Target - this.Position).normalizeProduct() * near;
            float s = near * (float)Math.Tan((double)(alpha / 180.0 * Math.PI) / 2.0);
            Vector poczatek = srodek - prostopadlyPrzes * s - pionPrzes * s;
            System.Console.WriteLine("" + poczatek);
            /**
             * powyższe to róg (lewy dolny) rzutni ("tylnej płaszczyzny obcinania").
             * teraz trzeba się od niego odsuwać w płaszczyźnie pionPrzes x prostopadlyPrzes
             * co ileśtam (jeszcz nie wiem, ile ;)), i w ten sposób określi się
             * kolejne punkty, które razem z this.Position będą wyznaczały nasze raye.
             **/
            float krok = s * 2.0f / 400.0f;
            /* 
            Powyższe:
             400 to piksele, krok to odległość w pionie lub w poziomie
            - chwilowo view kwadratowy - o jaką będzie się przesuwać "cel" promienia po płaszczyźnie
            rzutni
             */
            for (int i = 0; i < 400; i++)
                for (int j = 0; j < 400; j++)
                    img.setPixel(i, j, new Intensity(0, 0, 0));
            for (int i = 0; i < 400; i++)
            {

                System.Console.WriteLine("" + i + @"/400");
                for (int j = 0; j < 400; j++)
                {
                    napierdalacz = new Ray(observer, poczatek + i * krok * pionPrzes + j * krok * prostopadlyPrzes);

                    foreach (Primitive p in scene)
                    {
                        if (p.findIntersection(napierdalacz).X != float.PositiveInfinity)
                        {
                            // PHONG
                            /*
                            double r, g, b, cos;
                            Vector I = napierdalacz.direction.normalizeProduct();
                            Vector N = ((Sphere)p).normal(p.findIntersection(napierdalacz));
                            Vector R = I - N * (N.dot(I) * 2.0f);
                            float ss = napierdalacz.direction.normalizeProduct().dot(R);

                            if (-ss > 0)
                            {

                            }
                            */
                            img.setPixel(i, j, p.color); //w tej chwili dany promień, ale nie na rzutni
                        }
                    }
                }
                    img.obraz.Save(renderTarget);

                }

        }
    }
}