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
        public  Image texture;
        public Color[,] colorMap; // chyba bedzie lepszy klasy kolor niz Intensity i tak pozniej to trza do setPixela dac


        public Texture(String source)
        {
            this.getTexture(source);
            //skasowałem poniższe, bo odczyt tych danych i tak następuje w funkcji getTexture()
            //this.texture.XSize = 512; // Width <-- tu chyba lepiej uzaleznic od pozniejszych i i j (jakos zliczac po prostu)
            //this.texture.YSize = 256; // Height

            
        }

        // wczytanie tekstury
        public void getTexture(String source)
        {
            Bitmap bmp = (Bitmap)Bitmap.FromFile(source);
            this.texture = new Image(bmp.Width, bmp.Height);//pic
            
            this.texture.obraz = bmp; //<-- tu sie cos pieprzy, moze ja juz sie w tym wszystkim zakrecilam, nie wiem...
            this.texture.XSize = this.texture.obraz.Width;
            this.texture.YSize = this.texture.obraz.Height;
            colorMap = new Color[this.texture.XSize, this.texture.YSize];
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
