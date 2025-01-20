using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Linq;
using Color = System.Windows.Media.Color;


namespace Hw7Colors
{
    class ColorModel
    {
        private byte alpha;
        private byte red;
        private byte green;
        private byte blue;
        private Color _colorf;
        private string _colorC;





        public byte Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = value;

            }
        }
        public byte Red
        {
            get
            {
                return red;
            }
            set
            {
                red = value;

            }
        }
        public byte Green
        {
            get
            {
                return green;
            }
            set
            {
                green = value;

            }
        }

        public byte Blue
        {
            get
            {
                return blue;
            }
            set
            {
                blue = value;

            }
        }



        public ColorModel(byte a, byte r, byte g, byte b,string s,Color c)
        {
            Alpha = a;
            Red = r;
            Green = g;
            Blue = b;
            ColorC = s;
            ColorF = c;
        }


        public ColorModel()
        {
            Alpha = 0;
            Red = 0;
            Green = 0;
            Blue = 0;
        }


     
        public Color ColorF
        {
            get
            {
                return _colorf;
            }
            set
            {
                _colorf = value;
               
            }
        }



       
        public string ColorC
        {
            get
            {
                return _colorC;
            }
            set
            {
                _colorC = value;
              
            }
        }

    }
}
