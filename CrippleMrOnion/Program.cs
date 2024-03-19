using CrippleMrOnion.Data;
using CrippleMrOnion.Display;
using CrippleMrOnion.Display.Formatting;

namespace CrippleMrOnion
{
    public class Program
    {
        public static DebugGraphic Debug = new("");
        static void Main(string[] args)
        {
            Console.Clear();
            Console.CursorVisible = false;

            Debug.WriteLine("Started Debug Logs");

            Debug.Draw(Console.BufferWidth - 25, 0, 25, -1);

            Hand hand = new();
            hand.Add(new Card(CardSuit.Swords, CardRank.Six));
            hand.ToGraphic().Draw(14, 20);

            Debug.WriteLine("Awaiting user confirmation to exit...");
            Debug.Draw(Console.BufferWidth - 25, 0, 25, -1);
            Console.ReadKey(true);
        }
    }
}