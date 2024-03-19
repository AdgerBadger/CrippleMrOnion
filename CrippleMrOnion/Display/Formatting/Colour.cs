namespace CrippleMrOnion.Display.Formatting

{
    public class Colour : Format
    {
        private int _R, _G, _B;
        public bool A;
        public bool Foreground;
        public int R
        {
            get { return _R;  }
            set { _R = Math.Clamp(value, 0, 6); }
        }

        public int G
        {
            get { return _G; }
            set { _G = Math.Clamp(value, 0, 5); }
        }

        public int B
        {
            get { return _B; }
            set { _B = Math.Clamp(value, 0, 5); }
        }

        public Colour(int r, int g, int b, bool a = true, bool foreground = false)
        {
            R = r; G = g; B = b; A = a; Foreground = foreground;
        }

        public static int LowerBound = 16;
        public override string Value
        {
            get
            {
                int val = LowerBound;
                val += R * 36;
                val += G * 6;
                val += B;
                return A ? (Foreground ? '3' : '4') + "8;5;" + val : "";
            }
            protected init
            {
                throw new Exception("Nuh uh uh");
            }
        }
    }
}
