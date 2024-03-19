using CrippleMrOnion.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrippleMrOnion.Data
{
    public struct Located<T> : IComparable<Located<T>>
    {
        public T Value;
        public int X;
        public int Y;
        public Located(T val, int x, int y)
        {
            Value = val;
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return Value.ToString() + $" @ ({X}, {Y})";
        }

        public int CompareTo(Located<T> that)
        {
            if (this.Y < that.Y) return -1;
            else if (this.Y > that.Y) return 1;
            else
            {
                if (this.X < that.X) return -1;
                else if (this.X > that.X) return 1;
                else return 0;
            }
        }
    }
}
