using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CrippleMrOnion.Display
{
    public class DebugGraphic : IDrawable
    {
        public string Message;
        public DebugGraphic(string msg)
        {
            Message = msg;
        }

        public int Width
        {
            get
            {
                return Message.Split('\n')[0].Length;
            }
        }

        public int Height
        {
            get
            {
                return Message.Split('\n').Length;
            }
        }

        public void Draw(int x, int y, int w, int h)
        {
            int prevCursorLeft = Console.CursorLeft;
            int prevCursorTop = Console.CursorTop;
            string[] lines = Message.Split('\n');
            int noOfLines = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                Console.CursorLeft = x;
                Console.CursorTop = y+noOfLines;
                for(int ci = 0; ci < lines[i].Length; ci++)
                {
                    if(ci % w == 0 && w >= 0 && ci > 1)
                    {
                        noOfLines++;
                        Console.CursorLeft = x + 2;
                        Console.CursorTop = y + noOfLines;
                    }
                    Console.Write(lines[i][ci]);
                }
                noOfLines++;
            }
            Console.CursorLeft = prevCursorLeft;
            Console.CursorTop = prevCursorTop;
        }

        public static DebugGraphic operator +(DebugGraphic d, string msg)
        {
            return new DebugGraphic(d.Message + msg);
        }

        public void Write(string msg)
        {
            Message += msg;
        }

        public void WriteLine(string msg)
        {
            Message += msg + "\n";
        }

        public void WriteLine(object o)
        {
            Message += o.ToString() + "\n";
        }
    }
}
