namespace CrippleMrOnion.Display
{
    public interface IDrawable
    {
        int Height { get; }
        int Width { get; }
        public void Draw(int x, int y, int maxWidth = -1, int maxHeight = -1);
    }
}
