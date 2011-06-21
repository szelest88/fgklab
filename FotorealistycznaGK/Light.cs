
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotorealistycznaGK
{
    abstract class Light
    {
        Intensity color; // kolor swiatla
        Vector position; // pozycja swiatla w ukladzie xyz
        public float energy;

        #region Properties

        public Intensity Color
        {
            get { return color; }
            set { color = value; }
        }

        public Vector Position
        {
            get { return position; }
            set { position = value; }
        }

        #endregion Properties

        public Light()
        {
            this.Color = new Intensity(1, 1, 1);
            this.Position = new Vector(0, 0, 0);
            this.energy = 1000;
        }

        // nie wiem czy pozycji nie trza jakos powiazac z kamera - ale tak glosno mysle :>
        //może dodawać światła do sceny (ewentualnie rozszerzywszy zakres typów kontenera)
        //a następnie podawać je razem ze sceną do kamery - AM. Też głośno myślę ;)

        public Light(Intensity I, Vector v)
        {
            this.Color = I;
            this.Position = v;
            this.energy = 1000;
        }

    }
}
