using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace InvasionOfAldebaran.Models
{
	public abstract class AnimatedObject
	{
		public double X { get; protected set; }
		public double Y { get; protected set; }
		public double Vx { get; protected set; }
		public double Vy { get; protected set; }

		protected static Random Random = new Random();

		public Polygon Frame = new Polygon();

		public AnimatedObject(double x, double y, double vx, double vy)
		{
			X = x;
			Y = y;
			Vx = vx;
			Vy = vy;
		}

		public abstract void Draw(Canvas zeichenfläche);

		public void Animate(TimeSpan intervall, Canvas zeichenfläche)
		{
			X += Vx * intervall.TotalSeconds;
			Y += Vy * intervall.TotalSeconds;

			if (X < 0.0)
			{
				X = zeichenfläche.ActualWidth;
			}
			else if (X > zeichenfläche.ActualWidth)
			{
				X = 0;
			}

			if (Y < 0.0)
			{
				Y = zeichenfläche.ActualHeight;
			}
			else if (Y > zeichenfläche.ActualHeight)
			{
				Y = 0;
			}
		}

		public bool ContainsPoint(double x, double y)
		{
			return Frame.RenderedGeometry.FillContains(new Point(x - X, y - Y));
		}
	}
}
