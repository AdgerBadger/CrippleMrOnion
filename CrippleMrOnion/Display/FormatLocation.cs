using CrippleMrOnion.Display.Formatting;

namespace CrippleMrOnion.Display
{
    public struct FormatLocation : IComparable<FormatLocation>
    {
        public int X;
        public int Y;
        public Format Format;

        public int CompareTo(FormatLocation that)
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
