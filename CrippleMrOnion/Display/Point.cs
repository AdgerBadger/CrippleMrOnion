using CrippleMrOnion.Display.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrippleMrOnion.Display
{
    public class Point : IDrawable
    {
        public int Height { get { return 1; } }
        public int Width { get { return 1; } }
        public Colour Colour = new(5, 5, 5);
        public void Draw(int x, int y, int _ = -1, int __ = -1)
        {
            Console.CursorTop = y;
            Console.CursorLeft = x;
            Console.Write($"{Colour} {GeneralFormat.Clear()}");
        }
    }
}
