using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InvasionOfAldebaran.Models
{
    public abstract class AnimatedObject
    {
        public double x { get; protected set; }
        public double y { get; protected set; }

        public AnimatedObject(double _x, double _y, double _vx, double _vy)
        {
            x = _x;
            y = _y;
        }

	    public abstract void Zeichne(Canvas zeichenfläche);

	    public void Animiere(TimeSpan intervall, Canvas zeichenfläche)
	    {
		    x += vx * intervall.TotalSeconds;
		    y += vy * intervall.TotalSeconds;

		    if (x < 0.0)
		    {
			    x = zeichenfläche.ActualWidth;
		    }
		    else if (x > zeichenfläche.ActualWidth)
		    {
			    x = 0;
		    }

		    if (y < 0.0)
		    {
			    y = zeichenfläche.ActualHeight;
		    }
		    else if (y > zeichenfläche.ActualHeight)
		    {
			    y = 0;
		    }

	    }
	}
}
