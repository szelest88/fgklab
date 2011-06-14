using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;

namespace FotorealistycznaGK
{
    class Loader{

        public List<Primitive> listaZajebista;

        public void read(float scale, float x, float y, float z)
        {

            /*przyjąłem format pliku:
v 0.000000 2.000000 2.000000
v 0.000000 0.000000 2.000000
v 2.000000 0.000000 2.000000
v 2.000000 2.000000 2.000000
v 0.000000 2.000000 0.000000
v 0.000000 0.000000 0.000000
v 2.000000 0.000000 0.000000
v 2.000000 2.000000 0.000000
f 1 2 3
f 2 3 4
f 8 7 6
f 7 6 5
f 4 3 7
f 3 7 8
f 5 1 4
f 1 4 8
f 5 6 2
f 6 2 1
f 2 6 7
f 6 7 3

    ...czyli chwilowo jeno werteksy i face'y, i każdy face jest
    trójkątem.
             * */

            //myk do uznania kropki za separator dziesiętny
            listaZajebista = new List<Primitive>();

            #region myk
            string currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            CultureInfo ci = new CultureInfo(currentCulture);
            //Ustawiamy nowy format 
            ci.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            #endregion myk

            StreamReader sr = new StreamReader(@"C:\teapot.obj");
            string s;
            // stringi opisujące współrzędne verteksów:
            List<string> verticesStrings = new List<string>();
            while ((s = sr.ReadLine()) != null)
            {
                if (s != "" && s[0] == 'v' && s[1] != 'n')
                    verticesStrings.Add(s);
            }

            // współrzędne verteksów:
            List<ObjVertex> verticesNums = new List<ObjVertex>();
            foreach (string s2 in verticesStrings)
            {
                char[] splitter = { ' ' };
                string[] temp = s2.Split(splitter, StringSplitOptions.RemoveEmptyEntries);//spearatorem jest x spacji - i co kurwa?


                ObjVertex ov = new ObjVertex(
                    double.Parse(temp[1]),
                    double.Parse(temp[2]),
                    double.Parse(temp[3]));
                verticesNums.Add(ov);
            }
            sr.Close();

            sr = new StreamReader(@"C:\teapot.obj");

            // stringi opisujące indeksy werteksów w face'ach
            List<string> facesStrings = new List<string>();
            while ((s = sr.ReadLine()) != null)
            {
                if (s != "" && s[0] == 'f' && s[1] != 'n')
                    facesStrings.Add(s);
            }

            //zamiana ich na liczby i wczytanie dla każdego face'a
            //odpowiednich współrzędnych
            foreach (string s2 in facesStrings)
            {
                System.Console.WriteLine("FACE:");
                string[] podzielony = Regex.Split(s2, " +");
                //  string[] podzielony = s2.Split({' '},StringSplitOptions.RemoveEmptyEntries);

                int index1 = int.Parse(podzielony[1]) - 1;
                int index2 = int.Parse(podzielony[2]) - 1;
                int index3 = int.Parse(podzielony[3]) - 1;
                System.Console.WriteLine(verticesNums[index1]);
                System.Console.WriteLine(verticesNums[index2]);
                System.Console.WriteLine(verticesNums[index3]);
                listaZajebista.Add(
                    new Triangle(
                    
                    new Vector(x + scale * (float)verticesNums[index2].x, y + scale * (float)verticesNums[index2].y, z + scale * (float)verticesNums[index2].z),
                    new Vector(x + scale * (float)verticesNums[index1].x, y + scale * (float)verticesNums[index1].y, z + scale * (float)verticesNums[index1].z),
                    new Vector(x + scale * (float)verticesNums[index3].x, y + scale * (float)verticesNums[index3].y, z + scale * (float)verticesNums[index3].z),
                    new Intensity(1, 0, 0),
                    new Material())
                    );
            }
        }
    }
}
