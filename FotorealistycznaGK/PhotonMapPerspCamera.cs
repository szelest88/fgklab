using System;
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

        public void createMap(Photon[])
        {
        }
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
