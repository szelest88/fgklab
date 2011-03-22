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
            Vector origin = new Vector(0, 0, -20);
            Vector dir1 = new Vector(0, 0, 1);
            Vector dir2 = new Vector(0, 1, 0);
            Sphere S = new Sphere(new Vector(0,0,0), 10);
            Ray ray1 = new Ray(origin, dir1);
            Ray ray2 = new Ray(origin, dir2);

            System.Console.WriteLine("Obliczanie punktu przeciecia promienia ze sfera\n");
            
            System.Console.WriteLine(S.ToString()); //można też WriteLine(""+S
            
            System.Console.WriteLine("PROMIEN 1: ");
            S.findIntersection(ray1);
            System.Console.WriteLine("\n");
            System.Console.WriteLine("PROMIEN 2: ");
            S.findIntersection(ray2);          
            System.Console.ReadLine();  
        }
    }
}
