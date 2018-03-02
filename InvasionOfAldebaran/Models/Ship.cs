using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvasionOfAldebaran.Models
{
    public abstract class Ship : AnimatedObject
    {
	    protected Ship(double x, double y, double vx, double vy) : base(x, y, vx, vy)
	    {
	    }
    }
}
