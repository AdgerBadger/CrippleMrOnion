namespace CrippleMrOnion.Display.Formatting
{
    public abstract class Format
    {
        public abstract string Value { get; protected init; }
        public static string AnsiPrefix = "\u001b[";
        public static string AnsifyFromVal(string val)
        {
            return AnsiPrefix + val + "m";
        }

        private static string[] _formatExceptions = new string[]
        {
            "___", // Reset
            "***", // Blank
        };

        public override string ToString()
        {
            return !_formatExceptions.Contains(Value) ? AnsifyFromVal(Value) : "";
        }
    }
}
