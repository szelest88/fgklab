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
        //
        float near;
        Light light;
        List<Primitive> scene;
        string renderTarget; // string na ścieżkę do pliku wynikowego
        public PerspectiveCamera(float w, float h, int pixelsPerUnit, Vector position, Vector target, Vector up, float alpha, List<Primitive> scene, Light light, Uri renderTarget)
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
            this.light = light;
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
             poczatek - lewy dolny róg rzutni (tylnej płaszczyzny obcinania). Trza się od niego odsuwać
             w płaszczyźnie pionPrzes x prostopadlyPrzes co (szerokosc/rozdzielczosc), i w ten sposób 
             określi się kolejne punkty, które razem z this.Position będą wyznaczały nasze raye.
             **/
            float krok = s * 2.0f / 400.0f;
            /* 
            Powyższe:
             400 to piksele, krok to odległość w pionie lub w poziomie - chwilowo view kwadratowy - 
             o jaką będzie się przesuwać "cel" promienia po płaszczyźnie rzutni
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
                        //if (p.findIntersection(napierdalacz).X != float.PositiveInfinity)
                        //{
                        Vector intersection = p.findIntersection(napierdalacz);
                        if (intersection.X != float.PositiveInfinity)
                        {
                            float specular;
                            float specularCoeff = 1.0f;// co to kurwa jest?
                            float a = 1.0f;//współczynnik rozbłysku odbicia lustrzanego
                            //Vector I = napierdalacz.direction.normalizeProduct();
                            Ray test = new Ray(light.Position, poczatek + i * krok * pionPrzes + j * krok * prostopadlyPrzes);
                            //powyższe: poprawka Łukasza S.
                            Vector I = test.direction.normalizeProduct();
                            Vector N = ((Sphere)p).normal(intersection);
                            Vector R = I - N * (N.dot(I) * 2.0f); //brakuje tu pozycji światła
                            float ss = napierdalacz.direction.normalizeProduct().dot(R);
                            if (ss > 0)
                                specular = (float)(Math.Pow(ss, a));
                            else
                                specular = 0;
                            specular *= specularCoeff;
                            Vector sIntensity = new Vector((float)light.Color.R, (float)light.Color.G, (float)light.Color.B) * -specular;
                            //diffuse
                            float cosinus = napierdalacz.direction.normalizeProduct().dot(
                                N);
                            double r = light.Color.R * -0.5 * cosinus; //-1.0 - jakieś k
                            double g = light.Color.G * -0.5 * cosinus;
                            double b = light.Color.B * -0.5 * cosinus;

                            Intensity diff = new Intensity(r * p.color.R, g * p.color.G, b * p.color.B);//33-moje
                            //diff.addValues(p.color.R*0.33, p.color.G*0.33, p.color.B*0.33);//moje, ale ma sens?
                            //ten kolor należałoby raczej mnożyć- teraz to jest ambient zależny od koloru




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
                            diff.addValues(sIntensity.X * 0.33, sIntensity.Y * 0.33, sIntensity.Z * 0.33);//33-moje
                            //  diff.addValues(0.33, 0.33, 0.33); //lol, ambient
                            img.setPixel(i, j, new Intensity(diff)); //w tej chwili dany promień, ale nie na rzutni
                        }
                    }
                }
                img.obraz.Save(renderTarget);

            }

        }
    }
}