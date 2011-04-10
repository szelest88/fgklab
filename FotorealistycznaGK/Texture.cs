using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;


namespace FotorealistycznaGK
{
    class Texture
    {
        Image texture;
        public Intensity[,] colorMap;


        public Texture()
        {
            this.texture.XSize = 600; // Width
            this.texture.YSize = 800; // Height
        }

        // wczytanie tekstury
        public void getTexture(String source)
        {
            this.texture.obraz = Bitmap.FromFile(@ source); //<-- tu sie cos pieprzy, moze ja juz sie w tym wszystkim zakrecilam, nie wiem...
        }
    }
}
