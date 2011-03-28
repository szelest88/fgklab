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
    class OrthographicCamera: Camera
    {
    }
}
