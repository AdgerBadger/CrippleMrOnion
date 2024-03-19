using CrippleMrOnion.Display;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrippleMrOnion.Data
{
    public struct PartialCardSet : IEquatable<PartialCardSet>, IGraphical, IOriented
    {
        public int Size;
        public Card[] VisibleCards;
        public Orientation Orientation { get; set; }

        public IDrawable ToGraphic()
        {
            DrawableCollection sprites = new DrawableCollection();
            switch(Orientation)
            {
                case Orientation.Bottom:
                    {
                        for(int i = 0; i < VisibleCards.Length; i++)
                        {
                            IDrawable cardGraphic = VisibleCards[i].ToGraphic();
                            sprites.Add(new Located<IDrawable>(cardGraphic, i * (cardGraphic.Width + 2), 1));
                        }
                        for(int i = 0; i < Size - VisibleCards.Length; i++)
                        {
                            IDrawable cardGraphic = Card.Null().ToGraphic();
                            sprites.Add(new Located<IDrawable>(cardGraphic, i * (cardGraphic.Width + 2), cardGraphic.Height + 2));
                        }
                        return sprites;
                    }
                case Orientation.Left:
                    {
                        for(int i = 0; i < VisibleCards.Length; i++)
                        {
                            IDrawable cardGraphic = VisibleCards[i].ToGraphic();
                            sprites.Add(new Located<IDrawable>(cardGraphic, cardGraphic.Width + 2, i * (cardGraphic.Height + 2)));
                        }
                        for(int i = 0; i < Size - VisibleCards.Length; i++)
                        {
                            IDrawable cardGraphic = Card.Null().ToGraphic();
                            sprites.Add(new Located<IDrawable>(cardGraphic, 1, i * (cardGraphic.Height + 2)));
                        }
                        return sprites;
                    }
                case Orientation.Top:
                    {
                        for(int i = 0; i < VisibleCards.Length; i++)
                        {
                            IDrawable cardGraphic = VisibleCards[i].ToGraphic();
                            sprites.Add(new Located<IDrawable>(cardGraphic, i * (cardGraphic.Width + 2), cardGraphic.Height + 2));
                        }
                        for(int i = 0; i < Size - VisibleCards.Length; i++)
                        {
                            IDrawable cardGraphic = Card.Null().ToGraphic();
                            sprites.Add(new Located<IDrawable>(cardGraphic, i * (cardGraphic.Width + 2), 1));
                        }
                        return sprites;
                    }
                case Orientation.Right:
                    {
                        for (int i = 0; i < VisibleCards.Length; i++)
                        {
                            IDrawable cardGraphic = VisibleCards[i].ToGraphic();
                            sprites.Add(new Located<IDrawable>(cardGraphic, 1, i * (cardGraphic.Height + 2)));
                        }
                        for (int i = 0; i < Size - VisibleCards.Length; i++)
                        {
                            IDrawable cardGraphic = Card.Null().ToGraphic();
                            sprites.Add(new Located<IDrawable>(cardGraphic, cardGraphic.Width + 2, i * (cardGraphic.Height + 2)));
                        }
                        return sprites;
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        public bool Equals(PartialCardSet that)
        {
            if(this.VisibleCards.Length != that.VisibleCards.Length || this.Size != that.Size) return false;
            for(int i = 0; i < this.Size; i++)
            {
                if (!that.VisibleCards.Contains(this.VisibleCards[i])) return false;
            }

            for (int i = 0; i < that.Size; i++)
            {
                if (!this.VisibleCards.Contains(that.VisibleCards[i])) return false;
            }

            return true;
        }
    }
}
