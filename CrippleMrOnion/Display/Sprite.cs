using CrippleMrOnion.Data;
using CrippleMrOnion.Display.Formatting;
using System.Linq;
using System.Numerics;

namespace CrippleMrOnion.Display
{
    public class Sprite : IDrawable
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Colour BaseBackgroundColour { get; set; }
        public Colour BaseForegroundColour { get; set; }
        public List<string> CharContents { get; set; }
        public List<Located<Format>> FormatContents { get; set; }

        public Sprite(int width, int height, Colour baseBackgroundColour, Colour baseForegroundColour, IEnumerable<string> charContents = null!)
        {
            Width = width;
            Height = height;
            BaseBackgroundColour = baseBackgroundColour;
            BaseForegroundColour = baseForegroundColour;
            CharContents = charContents != null ? new List<string>(charContents) : new List<string>(new string[] { "" });
            FormatContents = new List<Located<Format>>();
        }

        public Sprite(Sprite oldSprite)
        {
            this.Width = oldSprite.Width;
            this.Height = oldSprite.Height;
            this.BaseBackgroundColour = oldSprite.BaseBackgroundColour;
            this.BaseForegroundColour = oldSprite.BaseForegroundColour;
            this.CharContents = oldSprite.CharContents;
            this.FormatContents = oldSprite.FormatContents;

        }

        public Format[] FlattenedFormatUpTo(int flattenTo)
        {
            List<Format> genericFormats = new();
            Format fgColour = BaseForegroundColour;
            Format bgColour = BaseBackgroundColour;
            bool lastWasClear = false;
            for (int i = 0; i < FormatContents.Count && i < flattenTo; i++)
            {
                lastWasClear = false;
                if (FormatContents[i].Value is Colour)
                {
                    if ((FormatContents[i].Value as Colour)!.Foreground)
                    {
                        fgColour = (FormatContents[i].Value as Colour)!;
                    }
                    else
                    {
                        bgColour = (FormatContents[i].Value as Colour)!;
                    }
                }
                else if (FormatContents[i].Value.Value == "0")
                {
                    lastWasClear = true;
                }
                else if (FormatContents[i].Value.Value == "___")
                {
                    fgColour = BaseForegroundColour;
                    bgColour = BaseBackgroundColour;
                }
                else
                {
                    genericFormats.Add(FormatContents[i].Value);
                }
            }
            if (!lastWasClear)
            {
                genericFormats.Add(fgColour);
                genericFormats.Add(bgColour);
            }
            else
            {
                _debugDisplay += "Last was clear\n";
            }
            return genericFormats.ToArray();
        }

        public char this[int x, int y]
        {
            get
            {
                return CharContents[y][x];
            }
            set
            {
                CharContents[y] = CharContents[y].Substring(0, x) + value + CharContents[y].Substring(x + 1);
            }
        }

        public static Sprite operator +(Sprite sprite, string contents)
        {
            for (int i = 0; i < contents.Length; i++)
            {
                if (sprite.CharContents.Last().Length == sprite.Width || contents[i] == '\n')
                {
                    if (sprite.CharContents.Count < sprite.Height)
                    {
                        sprite.CharContents.Add("");
                    }
                    else
                    {
                        return sprite;
                    }
                    if (contents[i] == '\n' && i < contents.Length - 1)
                    {
                        i++;
                    }
                }
                sprite.CharContents[sprite.CharContents.Count - 1] += contents[i];
            }
            return sprite;
        }

        public static Sprite operator +(Sprite sprite, Format format)
        {
            if (sprite.CharContents.Last().Length < sprite.Width)
            {
                sprite.FormatContents.Add(new Located<Format>
                {
                    X = Math.Clamp(sprite.CharContents.Last().Length, 0, sprite.Width),
                    Y = sprite.CharContents.Count - 1,
                    Value = format
                });
            } else
            {
                sprite.CharContents.Add("");
                sprite.FormatContents.Add(new Located<Format>
                {
                    X = Math.Clamp(sprite.CharContents.Last().Length, 0, sprite.Width),
                    Y = sprite.CharContents.Count - 1,
                    Value = format
                });
            }
            return sprite;
        }

        //public Sprite ConcatRight(Sprite nextSprite, char padding = ' ')
        //{
        //    Sprite sprite = new Sprite(this);
        //    for (int i = 0; i < Math.Max(sprite.Height, nextSprite.Height); i++)
        //    {
        //        if (i < sprite.CharContents.Count && i < nextSprite.CharContents.Count)
        //        {
        //            sprite.CharContents[i] = sprite.CharContents[i].PadRight(sprite.Width, padding) + nextSprite.CharContents[i];
        //        }
        //        else if (i >= sprite.CharContents.Count && i < nextSprite.CharContents.Count)
        //        {
        //            while (i >= sprite.CharContents.Count)
        //            {
        //                sprite.CharContents.Add("");
        //            }
        //            sprite.CharContents[i] = sprite.CharContents[i].PadRight(sprite.Width, padding) + nextSprite.CharContents[i];
        //        }
        //        else if (i < sprite.CharContents.Count && i >= nextSprite.CharContents.Count)
        //        {
        //            sprite.CharContents[i] = sprite.CharContents[i].PadRight(sprite.Width, padding) + new string(padding, nextSprite.Width);
        //        }
        //        else
        //        {
        //            while (i >= sprite.CharContents.Count)
        //            {
        //                sprite.CharContents.Add("");
        //            }
        //            sprite.CharContents[i] = new string(padding, sprite.Width + nextSprite.Width);
        //        }
        //    }

        //    for (int i = 0; i < nextSprite.FormatContents.Count; i++)
        //    {
        //        FormatLocation formatLoc = nextSprite.FormatContents[i];
        //        formatLoc.X += sprite.Width;
        //        sprite.FormatContents.Add(formatLoc);
        //    }

        //    sprite.FormatContents.Sort();

        //    sprite.Width += nextSprite.Width;
        //    sprite.Height = Math.Max(sprite.Height, nextSprite.Height);

        //    return sprite;
        //}

        //public Sprite ConcatLeft(Sprite nextSprite, char padding = ' ')
        //{
        //    return nextSprite.ConcatRight(this);
        //}

        //public Sprite ConcatBottom(Sprite nextSprite, char padding = ' ')
        //{
        //    throw new NotImplementedException();
        //}

        //public Sprite ConcatTop(Sprite nextSprite, char padding = ' ')
        //{
        //    return nextSprite.ConcatBottom(this);
        //}

        public override string ToString()
        {
            //string returner = "{\n";
            //for (int i = 0; i < CharContents.Count; i++)
            //{
            //    returner += "  " + CharContents[i] + "\n";
            //}
            //returner += "}\n";
            //returner += $"{FormatContents.Count} Formats loaded";
            string returner = $"{Width} x {Height}, {FormatContents.Count} F";
            return returner;
        }

        private DebugGraphic _debugDisplay = new(GeneralFormat.Clear().ToString());

        public void Draw(int x, int y, int maxWidth, int maxHeight)
        {
            Console.CursorLeft = x;
            Console.CursorTop = y;
            int nextFormat = 0;
            for (int i = 0; (i < maxHeight || maxHeight < 0) && i < Height; i++)
            {
                _debugDisplay.Message += $"{GeneralFormat.Underline()}{GeneralFormat.Bold()}New Line:{GeneralFormat.Clear()}\n";
                Console.CursorLeft = x;
                Console.CursorTop = y + i;
                Format[] flattenedFormats = FlattenedFormatUpTo(nextFormat);
                for (int j = 0; j < flattenedFormats.Length; j++)
                {
                    Console.Write(flattenedFormats[j]);
                    _debugDisplay.Message += $"{flattenedFormats[j]}Applied format no. {j}{GeneralFormat.Clear()}\n";
                }

                for (int ii = 0; (ii < maxWidth || maxWidth < 0) && ii < Width; ii++)
                {
                    if (nextFormat < FormatContents.Count && FormatContents[nextFormat].X == ii && FormatContents[nextFormat].Y == i)
                    {
                        Console.Write(FormatContents[nextFormat].Value);
                        _debugDisplay.Message += $"{FormatContents[nextFormat].Value}Applied new format {FormatContents[nextFormat].X}, {FormatContents[nextFormat].Y}{GeneralFormat.Clear()}\n";
                        if (FormatContents[nextFormat].Value.Value == "___")
                        {
                            Console.Write(GeneralFormat.Clear());
                            Console.Write(BaseBackgroundColour);
                            Console.Write(BaseForegroundColour);
                        }
                        nextFormat++;
                    }

                    if (i < CharContents.Count && ii < CharContents[i].Length)
                    {
                        Console.Write(CharContents[i][ii]);
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                //_debugDisplay.Draw(50, 0, 20, 400);
                Console.Write(GeneralFormat.Clear());
            }
        }
    }
}
