using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InvasionOfAldebaran.Models
{
    public class Enemy : AnimatedObject
    {
	    public Enemy(double x, double y, double vx, double vy) : base(x, y, vx, vy)
	    {
	    }

		public override void Draw(Canvas zeichenfläche)
		{
			throw new NotImplementedException();
		}
	}
}
