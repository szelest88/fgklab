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
    class OrthographicCamera: Camera
    {
    }
}
