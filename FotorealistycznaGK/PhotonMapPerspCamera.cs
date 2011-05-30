using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    //stworzyłem nową klasę, bo w sumie tak będzie chyba czytelniej
    //to będzie. Rozumiem, że sama mapa fotonów = tablica fotonów
    public class PhotonMapPerspCamera: PerspectiveCamera
    {
        int photonCount; // ilość tegesów

        public void createMap(
        public PhotonMapPerspCamera(int photonCount)
        {
            this.photonCount = photonCount;

        }
        public override void renderScene()
        {
            //napierdalamy
        }
    }
}
