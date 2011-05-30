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
        int radius; //promień wyszukiwania
        //każdy foton ma od 0 do 1 i średnia?

        public void createMap(ref Photon[] map) //po utworzeniu obiektu, wywoła się
                                                //to - funkcja zapełni tablicę
                                                //fotonami
        {

        }
        public PhotonMapPerspCamera(int photonCount, int radius)
        {
            this.photonCount = photonCount;
            this.radius = radius;

        }
        public override void renderScene()
        {
            //napierdalamy promieniami
            
        }
    }
}
