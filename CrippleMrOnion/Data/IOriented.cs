using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrippleMrOnion.Data
{
    public enum Orientation
    {
        Left,
        Right,
        Top,
        Bottom
    }
    public interface IOriented
    {
        public Orientation Orientation { get; set; }
    }
}
