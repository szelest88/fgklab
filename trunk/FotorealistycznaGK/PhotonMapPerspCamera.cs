﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    // stworzyłem nową klasę, bo w ten rendering gdzieś się musi odbyć, a w sumie
    // tak będzie chyba czytelniej to będzie. Rozumiem, że 
    // sama mapa fotonów = tablica fotonów
    public class PhotonMapPerspCamera: PerspectiveCamera
    {
        int photonCount; // ilość tegesów
        int radius; //promień wyszukiwania
        //każdy foton ma od 0 do 1 i średnia?
        Photon[] map;
        public void createMap(int photonCount, Light light, List<Primitive> scene, int bounces) 
                                                //po utworzeniu obiektu, wywoła się
                                                //to - funkcja zapełni tablicę
                                                //fotonami
        {
            //tutaj rzucamy w scenę fotonami ze śródła
            // i zapamiętujemy miejsca trafień, odbijając fotony max bounces razy
            // gdzie wywołać tą funkcję? chyba niżej
            Random random = new Random();
            int indexer = 0;
            for (int i = 0; i < photonCount; i++)
            {
                Ray photonDir = new Ray(light.Position,new Vector((float)random.NextDouble(),(float)random.NextDouble(),(float)random.NextDouble()));
                //tworzymy promień związany z fotonem
                foreach (Primitive pr in scene) //i sprawdzamy, w co trafia. Punkt trafienia zapisujemy w mapie (chwilowo olewamy rekursję)
                {
                    if (pr.findIntersection(photonDir).X != float.PositiveInfinity)
                    {
                        map[indexer] = new Photon(pr.findIntersection(photonDir), light.Color, photonDir.direction);
                    }
                }
            }
        }
        public PhotonMapPerspCamera(int photonCount, int radius, int bounces,
             
            //i parametry dla konstruktora klasy bazowej:
            float w, float h, int pixelsPerUnit, Vector position, Vector target, Vector up, float alpha, List<Primitive> scene, Light light, Uri renderTarget
            ):base(w,h,pixelsPerUnit,position,target,up,alpha,scene,light,renderTarget) //wywołanie konstruktora klasy bazowej
        {
            
            this.photonCount = photonCount;
            this.radius = radius;

            //tu wywołamy funkcję powyższą na rzecz parametru map
            createMap(photonCount, light, scene, bounces); //tzn. od razu w konstruktorze generujemy mapę

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
            Vector observer = this.Position;
            Vector srodek = this.Position + (this.Target - this.Position).normalizeProduct() * near;
            float s = near * (float)Math.Tan((alpha / 180.0 * Math.PI) / 2.0);
            Vector prostopadlyPrzes =
               ((this.Target - this.Position).cross(this.Position - this.Up)).normalizeProduct();//cross(this.Positon-this.Up)
            Vector pionPrzes = (this.Up).normalizeProduct();//; *1.5f;//(this.Up-this.Position).nor

            Vector poczatek = srodek - prostopadlyPrzes * s - pionPrzes * s;
           
            for(int i=0;i<400;i++)
                for (int j = 0; j < 400; j++)
                {
                    Ray napierdalacz = new Ray(this.Position, this.Target);//przepisać z perspective

                }
        }
    }
}