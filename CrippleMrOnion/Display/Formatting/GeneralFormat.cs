namespace CrippleMrOnion.Display.Formatting
{
    public class GeneralFormat : Format
    {
        public override string Value { get; protected init; }

        public GeneralFormat(string value)
        {
            Value = value;
        }

        public static GeneralFormat Clear()
        {
            return new GeneralFormat("0");
        }

        public static GeneralFormat Reset()
        {
            return new GeneralFormat("___");
        }

        public static GeneralFormat Bold()
        {
            return new GeneralFormat("1");
        }

        public static GeneralFormat Underline()
        {
            return new GeneralFormat("4");
        }

        public static GeneralFormat Invert()
        {
            return new GeneralFormat("7");
        }
    }
}
