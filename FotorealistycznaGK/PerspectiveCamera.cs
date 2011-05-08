using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//WARIACJE - wzór do kombinowania

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
        float[,] depthBuffer; //NOWE
        float[,] depthBufferReflections;
        float[,] depthBufferRefractions;

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
            this.depthBuffer = new float[400, 400];
            for (int i = 0; i < 400; i++)
                for (int j = 0; j < 400; j++)
                    depthBuffer[i, j] = float.PositiveInfinity;
            this.depthBufferReflections = new float[400, 400];
            this.depthBufferRefractions = new float[400, 400];
            for (int i = 0; i < 400; i++)
                for (int j = 0; j < 400; j++)
                    depthBufferReflections[i, j] = float.PositiveInfinity;
            for (int i = 0; i < 400; i++)
                for (int j = 0; j < 400; j++)
                    depthBufferRefractions[i, j] = float.PositiveInfinity;
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

            //WARIACJE - wzór do kombinowania
            for (int i = 0; i < 400; i++)
            {
                // PHONG
                System.Console.WriteLine("" + i + @"/400");
                for (int j = 0; j < 400; j++)
                {
                    napierdalacz = new Ray(observer, poczatek + i * krok * pionPrzes + j * krok * prostopadlyPrzes);
                    //hm, teraz?
                    foreach (Primitive p in scene)
                    {

                        Vector intersection = p.findIntersection(napierdalacz);
                        if (intersection.X != float.PositiveInfinity)
                        {
                            float specular;

                            float odleglosc = intersection.countVectorDistance(observer);
                            if (depthBuffer[i, j] >= odleglosc)
                                depthBuffer[i, j] = odleglosc;
                            // float specularCoeff = 0.75f;// co to kurwa jest?
                            // Ania: tu zmianilam na 15 to a ;)
                            //   float a = 15.0f;//współczynnik rozbłysku odbicia lustrzanego
                            //Vector I = napierdalacz.direction.normalizeProduct();
                            Ray test = new Ray(light.Position, poczatek + i * krok * pionPrzes + j * krok * prostopadlyPrzes);
                            //powyższe: poprawka Łukasza S.
                            Vector I = test.direction.normalizeProduct();
                            Vector N = p.normal(intersection);
                            Vector R = I - N * (N.dot(I) * 2.0f); //brakuje tu pozycji światła
                            //  Sphere sph = (Sphere)p;
                            float ss = napierdalacz.direction.normalizeProduct().dot(R);
                            if (ss > 0)
                                specular = (float)(Math.Pow(ss, p.material.Alpha));
                            else
                                specular = 0;
                            specular *= p.material.SpecularCoefficient;
                            Vector sIntensity = new Vector((float)light.Color.R, (float)light.Color.G, (float)light.Color.B) * specular;
                            //diffuse
                            float cosinus = napierdalacz.direction.normalizeProduct().dot(
                                N);
                            double r = light.Color.R * -p.material.DiffuseCoefficient * cosinus; //-1.0 - jakieś k
                            double g = light.Color.G * -p.material.DiffuseCoefficient * cosinus;
                            double b = light.Color.B * -p.material.DiffuseCoefficient * cosinus;
                            if (intersection.X != float.PositiveInfinity)
                            {
                                p.color.R = (double)(p.Texturize(intersection).R) / 255.0;
                                p.color.G = (double)(p.Texturize(intersection).G) / 255.0;
                                p.color.B = (double)(p.Texturize(intersection).B) / 255.0;
                            }
                            Intensity diff;
                            diff = new Intensity(0, 0, 0);
                            if (depthBuffer[i, j] == odleglosc)
                            {
                                if (p.material.isMirror == false && p.material.isRefractive == false)
                                {
                                    diff = new Intensity(r * p.color.R, g * p.color.G, b * p.color.B);//33-moje
                                }
                                else if (p.material.isMirror == true)
                                {
                                    //Vector R = I - N * (N.dot(I) * 2.0f);
                                    Vector I2 = test.direction.normalizeProduct();
                                    Vector N2 = p.normal(intersection);
                                    Vector R2 = -N2 * (N2.dot(I2) * 2.0f);
                                    Ray test2 = new Ray(intersection,//przemyśleć to trochę
                                     intersection + R2);//I- N *
                                    // (N.dot(I) * 2.0f) );//wygenerować odbity, wykonać tą kupę i zwrócić kolor

                                    foreach (Primitive p2 in scene)
                                    {
                                        Vector intersection2 = p2.findIntersection(test2);
                                        if (intersection2.X != float.PositiveInfinity && p2 != p)
                                        {
                                            if (p2.getName() == "trójkąt")
                                                System.Console.WriteLine("trójkąt!");
                                            System.Console.WriteLine("trafiło odbite");

                                            double r2 = (double)(p2.Texturize(intersection2).R) / 255.0;

                                            double g2 = (double)(p2.Texturize(intersection2).G) / 255.0;
                                            //uwzględnić też specular2
                                            double b2 = (double)(p2.Texturize(intersection2).B) / 255.0;
                                            float odleglosc2 = intersection.countVectorDistance(intersection2);

                                            diff = new Intensity(0, 0, 0);
                                            if (depthBufferReflections[i, j] >= odleglosc2)
                                                depthBufferReflections[i, j] = odleglosc2;
                                            else
                                                diff = new Intensity(r2, g2, b2);//33-moje
                                            System.Console.WriteLine("" + diff);
                                        }
                                    }
                                }
                                else if (p.material.isRefractive == true)
                                {
                                    //Sparta!
                                    //kopia reflective
                                    Vector I2 = test.direction.normalizeProduct();
                                    Vector N2 = p.normal(intersection);
                                    Vector R2 = -N2 * (N2.dot(I2) * 2.0f);//2->-1
                                    Ray test2 = new Ray(intersection,//przemyśleć to trochę
                                     intersection - R2);//I- N *
                                    // (N.dot(I) * 2.0f) );//wciąż coś nie teges.

                                    foreach (Primitive p2 in scene)
                                    {
                                        Vector intersection2 = p2.findIntersection(test2);
                                        if (intersection2.X != float.PositiveInfinity && p2.GetHashCode() != p.GetHashCode())
                                        {
                                            if (p2.getName() == "trójkąt")
                                                System.Console.WriteLine("trójkąt!");

                                            System.Console.WriteLine("REFRAKCJA!!!");

                                            double r2 = (double)(p2.Texturize(intersection2).R) / 255.0;

                                            double g2 = (double)(p2.Texturize(intersection2).G) / 255.0;
                                            //uwzględnić też specular2
                                            double b2 = (double)(p2.Texturize(intersection2).B) / 255.0;
                                            float odleglosc2 = intersection.countVectorDistance(intersection2);
                                            if (depthBufferRefractions[i, j] >= odleglosc2)
                                                depthBufferRefractions[i, j] = odleglosc2;
                                            if (depthBufferRefractions[i, j] == odleglosc2)
                                                diff = new Intensity(r2, g2, b2);//33-moje
                                            System.Console.WriteLine("" + diff);
                                        }
                                    }
                                }

                                diff.addValues(sIntensity.X, sIntensity.Y, sIntensity.Z);//33-moje
                                //diff.addValues(0.33, 0.33, 0.33); //lol, ambient
                                img.setPixel(i, j, new Intensity(diff)); //w tej chwili dany promień, ale nie na rzutni
                            }
                        }
                    }
                }

            }

            img.obraz.Save(renderTarget);
        }

    }
}