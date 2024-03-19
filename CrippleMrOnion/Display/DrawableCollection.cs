using CrippleMrOnion.Data;
using CrippleMrOnion.Display.Formatting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrippleMrOnion.Display
{
    public class DrawableCollection : ICollection<Located<IDrawable>>, IDrawable
    {
        public DrawableCollection() { }
        private List<Located<IDrawable>> _drawables = new();
        public int Count { get { return _drawables.Count; } }
        public int Width
        {
            get
            {
                _drawables.Sort((a, b) => (b.X + b.Value.Width) - (a.X + a.Value.Width));
                Located<IDrawable> located = _drawables.FirstOrDefault(new Located<IDrawable>(new Sprite(0,0,new Colour(0,0,0), new Colour(0,0,0)), 0, 0));
                return (located.X + located.Value.Width);
            }
        }
        public int Height
        {
            get
            {
                _drawables.Sort((a, b) => (b.Y + b.Value.Height) - (a.Y + a.Value.Height));
                Located<IDrawable> located = _drawables.FirstOrDefault(new Located<IDrawable>(new Sprite(0, 0, new Colour(0, 0, 0), new Colour(0, 0, 0)), 0, 0));
                return (located.Y + located.Value.Height);
            }
        }
        public Located<IDrawable> this[int index] { get { return _drawables[index]; } }
        public bool IsReadOnly { get { return false; } }
        public void CopyTo(IDrawable[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("The array cannot be null.");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
            if (Count > array.Length - arrayIndex)
                throw new ArgumentException("The destination array has fewer elements than the collection.");

            for (int i = 0; i < Count; i++)
            {
                array[i + arrayIndex] = this[i].Value;
            }
        }

        public void CopyTo(Located<IDrawable>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("The array cannot be null.");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
            if (Count > array.Length - arrayIndex)
                throw new ArgumentException("The destination array has fewer elements than the collection.");

            for (int i = 0; i < Count; i++)
            {
                array[i + arrayIndex] = this[i];
            }
        }

        public void Add(Located<IDrawable> sprite)
        {
            _drawables.Add(sprite);
        }

        public void Add(IDrawable sprite)
        {
            _drawables.Add(new Located<IDrawable>(sprite, 0, 0));
        }

        public void Clear() { _drawables.Clear(); }

        public bool Contains(IDrawable sprite) { return _drawables.Select(x => x.Value).Contains(sprite); }
        public bool Contains(Located<IDrawable> locDrawable) { return _drawables.Contains(locDrawable); }

        public bool Remove(Located<IDrawable> locDrawable) { return _drawables.Remove(locDrawable); }
        public bool Remove(IDrawable sprite)
        {
            int index = _drawables.Select(x => x.Value).ToList().IndexOf(sprite);
            if (index < 0) return false;
            _drawables.RemoveAt(index);
            return true;
        }


        public IEnumerator GetEnumerator()
        {
            return _drawables.GetEnumerator();
        }
        IEnumerator<Located<IDrawable>> IEnumerable<Located<IDrawable>>.GetEnumerator()
        {
            return _drawables.GetEnumerator();
        }

        public static explicit operator List<Located<IDrawable>>(DrawableCollection collection)
        {
            Located<IDrawable>[] sprites = new Located<IDrawable>[collection.Count];
            collection.CopyTo(sprites, 0);
            return sprites.ToList();
        }   

        public static explicit operator List<IDrawable>(DrawableCollection collection)
        {
            IDrawable[] sprites = new IDrawable[collection.Count];
            collection.CopyTo(sprites, 0);
            return sprites.ToList();
        }

        public void Draw(int x, int y, int mw, int mh)
        {

            for (int i = 0; i < _drawables.Count; i++)
            {
                _drawables[i].Value.Draw(x + _drawables[i].X, y + _drawables[i].Y, mw < 0 ? -1 : Math.Max(mw - _drawables[i].X,0), mh < 0 ? -1 : Math.Max(mh - _drawables[i].Y, 0));
            }
        }

    }
}
