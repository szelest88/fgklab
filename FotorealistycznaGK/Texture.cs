using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;


namespace FotorealistycznaGK
{
    class Texture
    {
        Image texture;
        public Color[,] colorMap; // chyba bedzie lepszy klasy kolor niz Intensity i tak pozniej to trza do setPixela dac


        public Texture(String source)
        {
            this.texture.XSize = 400; // Width <-- tu chyba lepiej uzaleznic od pozniejszych i i j (jakos zliczac po prostu)
            this.texture.YSize = 600; // Height

            this.getTexture(source);
        }

        // wczytanie tekstury
        public void getTexture(String source)
        {
            this.texture.obraz = (Bitmap)Bitmap.FromFile(source); //<-- tu sie cos pieprzy, moze ja juz sie w tym wszystkim zakrecilam, nie wiem...

            for (int i = 0; i < this.texture.XSize; i++)
            {
                for (int j = 0; j < this.texture.YSize; j++)
                {
                    colorMap[i,j] = texture.obraz.GetPixel(i, j);// czy to o to kaman z colorMap? :>
                }
            }
        }
    }
}
