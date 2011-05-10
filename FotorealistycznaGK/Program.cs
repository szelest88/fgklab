using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace FotorealistycznaGK
{
    class Program
    {
        //tototo! ściany się tworzą!
        static void Main(string[] args)
        {
            Vector origin = new Vector(0, 0, 0);
            Vector des1 = new Vector(0, 0, 1);
            Vector des2 = new Vector(0, 1, 0);
            Material material = new Material();
            material.hasTexture = true;
            material.texture = new Texture(@"C:\lenatex.jpg");
            Material zolty = new Material();
            zolty.hasTexture = true;
            zolty.texture = new Texture(@"C:\zoltetex.jpg");
            zolty.Alpha = 15.0f;
            zolty.SpecularCoefficient = 0.75f;
            Material mirr = new Material();
            mirr.isMirror = true;
            Material glass = new Material();
            glass.isRefractive = true;

            /*
             * Przesuwanki
             (x,y,z): x- blizej/dalej, y - gora/dol, z - prawo/lewo
             */

            //my: 1,0,0 -> S jest przed kuleną2
            Sphere S = new Sphere(new Vector(-0.01f, 0.1f, -0.25f), 0.125f, new Intensity(0, 0, 0), mirr);
            Sphere S2 = new Sphere(new Vector(0.175f, -0.275f, 0.15f), 0.20f, new Intensity(0, 0, 0), glass);
            //3: 0->1? chemy do tyłu
            S2.material.SpecularCoefficient = 0.35f;
            S2.material.Alpha = 0.1f;
            Sphere kulena1 = new Sphere(new Vector(0.0f, 0.25f, 0.0f), 0.125f, new Intensity(0, 0, 0), material);//druga kulka, tak, żeby sprawdzić czy działa.
            Sphere zolta = new Sphere(new Vector(0.0f, 0.0f, -0.0f), 0.125f, new Intensity(0, 0, 0), zolty);//druga kulka, tak, żeby sprawdzić czy działa.
            //EX: COMMENT!
            //  Triangle trn = new Triangle(new Vector(-1, 1.0f, -1f), new Vector(-1, 1.5f, 0), new Vector(-1, 1.0f, 0), new Intensity(0.1f, 0.1f, 1), material);
            //  Triangle trn2 = new Triangle(new Vector(-1, -1.1f, -0.1f), new Vector(-1, -1.0f, 0f), new Vector(-1, -1.0f, 0f), new Intensity(1, 0.1f, 1), material);
            //END OF EX
            //EX:
            #region jebaneŚciany
            //niebieska:
            Triangle trn = new Triangle(new Vector(0.0f, -0.5f, -0.5f), new Vector(0.0f, 0.5f, -0.5f), new Vector(0.0f, -0.5f, 0.5f), new Intensity(0, 0, 1), material);
            Triangle trn2 = new Triangle(new Vector(0.0f, 0.5f, 0.5f), new Vector(0.0f, -0.5f, 0.5f), new Vector(0.0f, .5f, -.5f), new Intensity(0, 0, 1), material);
            //zielona:
            Triangle trn3 = new Triangle(new Vector(0.0f, -0.5f, -0.5f), new Vector(0.5f, -0.5f, -0.5f), new Vector(0.5f, 0.5f, -0.5f), new Intensity(0, 1, 0), material);
            Triangle trn4 = new Triangle(new Vector(0.0f, 0.5f, -0.5f), new Vector(0.0f, -0.5f, -0.5f), new Vector(0.5f, 0.5f, -0.5f), new Intensity(0, 1, 0), material);
            //czerwona:
            Triangle trn5 = new Triangle(new Vector(0.5f, -0.5f, 0.5f), new Vector(0.0f, -0.5f, 0.5f), new Vector(0.5f, 0.5f, 0.5f), new Intensity(1, 0, 0), material);
            Triangle trn6 = new Triangle(new Vector(0.0f, -0.5f, 0.5f), new Vector(0.0f, 0.5f, 0.5f), new Vector(0.5f, 0.5f, 0.5f), new Intensity(1, 0, 0), material);

            Triangle sufitDalszy = new Triangle(new Vector(0, 0.5f, -0.5f), new Vector(0.5f, 0.5f, 0.5f), new Vector(0, 0.5f, 0.5f), new Intensity(1, 1, 1), material);

            Triangle sufitBlizszy = new Triangle(new Vector(0, 0.5f, -0.5f), new Vector(0.5f, 0.5f, -0.5f), new Vector(0.5f, 0.5f, 0.5f), new Intensity(1, 1, 1), material);

            Triangle floorDalszy = new Triangle(new Vector(0, -0.5f, -0.5f), new Vector(0, -0.5f, 0.5f), new Vector(0.5f, -0.5f, 0.5f), new Intensity(1, 1, 1), material);

            Triangle floorBlizszy = new Triangle(new Vector(0, -0.5f, -0.5f), new Vector(0.5f, -0.5f, 0.5f), new Vector(0.5f, -0.5f, -0.5f), new Intensity(1, 1, 1), material);
            #endregion jebaneŚciany
            trn.setUvCoords(0, 0, 0, 32, 32, 0);
            Texture tex = new Texture(@"C:\lenatex.jpg");
            trn.material.texture = tex;
            List<Primitive> list = new List<Primitive>();

            list.Add(S);
            list.Add(S2);
            list.Add(kulena1);
            list.Add(zolta);
            list.Add(trn);
            //EX!:
            list.Add(trn2);
            list.Add(trn3);
            list.Add(trn4);
            list.Add(trn5);
            list.Add(trn6);
            list.Add(sufitDalszy);
            list.Add(sufitBlizszy);
            list.Add(floorBlizszy);
            list.Add(floorDalszy);

            PointLight light = new PointLight();
            light.Color = new Intensity(1.0, 1.0, 1.0);
            light.Position = new Vector(1, -1, 1);
            Ray ray1 = new Ray(origin, des1);
            Ray ray2 = new Ray(origin, des2);

            Image img = new Image(200, 200); //kamera orto powinna przyjąć rozmiary obrazu jako parametr w konstruktorze

            //float heightPixel = 0.01f;// / 100.0f;//ROZDZIELCZOSCPOZIOMAOBRAZU i ta druga to 100, 100
            //float widthPixel = 0.01f;/// 100.0f;
            //double srodekX = -1.0f + (x + 0.5f) * widthPixel;
            //double srodekY = 1.0f - (y + 0.5f) * heightPixel;
            Intensity color1 = new Intensity(1, 0, 0);// na czerwono, żeby było coś widać
            Intensity color2 = new Intensity(0, 0, 1);
            Intensity backgroundColor = new Intensity();

            //   System.Console.WriteLine("Obliczanie punktu przeciecia promienia ze sfera\n");

            //     System.Console.WriteLine("Sfera: "+S.ToString()); //można też WriteLine(""+S)

            System.Console.WriteLine("PROMIEN 1 (" + ray1.ToString() + ": ");
            System.Console.WriteLine("Najbliższa kolizja w kierunku patrzenia:");

            #region jakiśBełkot
            //   Vector res=S.findIntersection(ray1);

            //            if (!float.IsPositiveInfinity(res.X))//jest takie gdy brak kolizji
            //          {
            //            res = S.findIntersection(ray1);
            //          System.Console.WriteLine(res);
            //    }
            //  else
            //    System.Console.WriteLine("Brak kolizji :(");

            // Zadanie 2 - rednerowanie
            /*
            //ten shit trzeba będzie chyba przenieść do klas kamer (metoda Render() czy coś)
            for (int i = 0; i < img.XSize; i++) 
            { 
                for (int j = 0; j < img.YSize; j++) 
                    { 
                        float srodekX = -1.0f + ((float)i + 0.5f) * widthPixel; 
                        float srodekY = 1.0f - ((float)j + 0.5f) * heightPixel;
                        
                        Ray ray = new Ray(new Vector(srodekX, srodekY, 0), new Vector(srodekX, srodekY, -1));

                        Vector intersect;
                        img.setPixel(i, j, backgroundColor); //czarny, jeśli shit
                        
                    foreach (Primitive p in list)
                        {
                                intersect = p.findIntersection(ray);
                                if (intersect.X != float.PositiveInfinity)
                                {
                                    img.setPixel(i, j, p.color);
                                }
                            }   
                        //}
                    }
            }
            
            img.obraz.Save(@"C:\obrazkoncowy.jpg"); //obraz, nie Bitmap
 */
            #endregion

            Vector target = new Vector(0.0f, 0, 0);
            Vector up = new Vector(0, 1, 0);//jak nie, to (010,a od góry 100)

            Vector v2 = new Vector(1, 0, 0); //tak - 1 1 0 pod kątem od góry,0,1,0 to od góry (z centralną)
            Vector v3 = -up.cross(v2 - target);
            System.Console.WriteLine(v3);
            System.Console.WriteLine("...");
            //dlaczego ten cross zamiast up... bo wychodzi coś typu (-1,0,0)
            PerspectiveCamera pc = new PerspectiveCamera(1, 1, 400, v2, target, v3, 90, list, light, new Uri(@"C:\renderpers.png"));
            pc.renderScene();
            //  OrthographicCamera oc = new OrthographicCamera(1, 1, 400, v2, target, v3, list, new Uri(@"C:\rendermasakra.jpg"));
            //  oc.renderScene();

            //  System.Console.ReadLine();  
            //System.Console.ReadKey();
            Process p = new Process();
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName=@"C:\renderpers.png";
            p.Start();
        }
    }
}
