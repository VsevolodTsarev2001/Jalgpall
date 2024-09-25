using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall
{
    public class Stadium
    {
        //размер стадиона
        public Stadium(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }

        public int Height { get; }
        //проверяем то что если находимся за пределами то false 
        public bool IsIn(double x, double y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
    }
}
