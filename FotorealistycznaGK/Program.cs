using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector origin = new Vector(0, 0, 0);
            Vector dir1 = new Vector(0, 0, 1);
            Vector dir2 = new Vector(0, 1, 0);
            Sphere S = new Sphere(new Vector(0,0,10), 1);
            Ray ray1 = new Ray(origin, dir1);
            Ray ray2 = new Ray(origin, dir2);

            // Zadanie 2
            Image img = new Image(100, 100);
            float heightPixel = 2.0f; // ROZDZIELCZOSCPIONOWAOBRAZU;
            float widthPixel = 2.0f;  // ROZDZIELCZOSCPOZIOMAOBRAZU;
            //double srodekX = -1.0f + (x + 0.5f) * widthPixel;
            //double srodekY = 1.0f - (y + 0.5f) * heightPixel;
            Intensity color = new Intensity ();
            Intensity backgroundColor = new Intensity();

            System.Console.WriteLine("Obliczanie punktu przeciecia promienia ze sfera\n");
         
            System.Console.WriteLine("Sfera: "+S.ToString()); //można też WriteLine(""+S)
            
            System.Console.WriteLine("PROMIEN 1 ("+ray1.ToString()+": ");
            System.Console.WriteLine("Najbliższa kolizja w kierunku patrzenia:");
            Vector res=S.findIntersection(ray1);

            if (!float.IsPositiveInfinity(res.X))//jest takie gdy brak kolizji
            {
                res = S.findIntersection(ray1);
                System.Console.WriteLine(res);
            }
            else
                System.Console.WriteLine("Brak kolizji :(");

            // Zadanie 2 - rednerowanie
 
            for (int i = 0; i < img.XSize; i++) 
            { 
                for (int j = 0; j < img.YSize; j++) 
                    { 
                        float srodekX = -1.0f + (i + 0.5f) * widthPixel; 
                        float srodekY = 1.0f - (j + 0.5f) * heightPixel;
                        
                        Ray ray = new Ray(new Vector(srodekX, srodekY, 0), new Vector(srodekX, srodekY, -1));
                        Vector intersetion = S.findIntersection(ray);

                        if (intersetion != null)
                        {
                            img.setPixel(i, j, color);
                        }
                        else img.setPixel(i, j, backgroundColor);
                    }
            }
            
            img.Bitmap.Save("obrazkoncowy.jpg"); 

           // System.Console.WriteLine("\n");
           // System.Console.WriteLine("PROMIEN 2( "+ray2.ToString()+": ");
           // S.findIntersection(ray2);          
            System.Console.ReadLine();  
        }
    }
}
