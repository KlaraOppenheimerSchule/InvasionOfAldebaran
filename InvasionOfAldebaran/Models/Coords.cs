using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvasionOfAldebaran.Models
{
    public class Coords
    {
        public double X { get; set; }
        
        public double Y { get; set; }

        public Coords( double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
