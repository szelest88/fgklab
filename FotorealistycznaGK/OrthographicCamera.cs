﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Text;

namespace FotorealistycznaGK
{
    class OrthographicCamera : Camera
    {
        /**
         <summary>
         szerokość i wysokość viewportu w jednostkach sceny
         </summary>
         **/
        float w, h;
        /**
         <summary>
         szerokość i wysokość viewportu w pikselach
         </summary>
         **/
        int wPix, hPix;
        /**
         <summary>
         szerokość i wysokość piksela w jednostkach sceny
         </summary>
         **/
        float widthOfPix, heightOfPix;
        List<Primitive> scene;
        string renderTarget; //dodałem, string na ścieżkę do pliku wynikowego
        public OrthographicCamera(float w, float h, int pixelsPerUnit, Vector position, Vector target, Vector up, List<Primitive> scene, Uri renderTarget)
        {
            this.w = w;
            this.h = h;
            this.wPix = (int)w * pixelsPerUnit;
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
            Ray napierdalacz;
            Vector prostopadlyPrzes =
                ((this.Target - this.Position).cross(this.Position -this.Up)).normalizeProduct();//cross(this.Positon-this.Up)
            Vector pionPrzes = (this.Up).normalizeProduct();
            System.Console.WriteLine(prostopadlyPrzes);
            Vector srodek = this.Position;
            Vector poczatek = srodek - prostopadlyPrzes * (w * 0.5f - widthOfPix * 0.5f);

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
                    napierdalacz = new Ray(poczatek, poczatek + (Target - Position));
                    foreach (Primitive p in scene)
                    {
                        if (p.findIntersection(napierdalacz).X != float.PositiveInfinity)
                            img.setPixel(i, j, p.color);
                    }
                    poczatek = iterator + new Vector(pionPrzes * i + prostopadlyPrzes * j);
                    // poczatek.add(prostopadlyPrzes + pionPrzes);
                    //System.Console.WriteLine("pocz:" + poczatek);
                }
            img.obraz.Save(renderTarget);
            System.Console.WriteLine(poczatek);

        }
    }
}



