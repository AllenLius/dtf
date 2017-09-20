using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dtf.Core
{
    public class Rect
    {
        public Rect()
            : this(0, 0, 0, 0)
        {
        }

        public Rect(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
