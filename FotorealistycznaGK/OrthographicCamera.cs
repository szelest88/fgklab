using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    //tak na oko... Chociaż nawet nie wiem, czy to ma sens. Tzn. może klasa
    //Camera powinna być jakoś ogólniejsza - jaki jest sens dziedziczenie fov
    //po kamerze przez kamerę Orto? Można to chyba jednak wywalić z Camera
    //i umieszczać tylko w kamerach, które tego potrzebują (naczy się
    //w kamerze perspektywicznej...?)

    //tak. Teraz doczytałem, że Camera powinna być ogólniejsza - trzeba by przenieść jej
    //treść (tzn. te rzeczy, które nie pasują do kamery Orto do klasy 
    //PerspectiveCamera (czy jakoś tak), i ewentualnie tutaj coś dodać 
    //ogólnie, chyba każda kamera będzie miała swoją pozycję, wektor Up i punkt,na który
    //patrzy (albo wektor kierunku). Orto powinna mieć chyba do tego wymiary prostokąta,
    //na który "rzutowany jest obraz", natomiast perspektywiczna parametry bryły widznia
    //  fov i płaszczyzny obcinania (czyli tak jak jest teraz w klasie Camera).

    //Myślę, że ma to ręce i nogi, acz trzeba by jeszcze pomyśleć.
    class OrthographicCamera: Camera
    {
        private float width; //kamera orto powinna mieć wymiary płaszczyzny rzutowania...
        //(prostokątnej)
        //position, up i target dziedziczymy po Camera

        public float Width
        {
            get { return width; }
            set { width = value; }
        }
        private float height;

        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        //do tej metody spróbuję wrzucić rendering i zapis wynikowego obrazu do pliku
        public override void renderScene()
        {
        }
    }
}
