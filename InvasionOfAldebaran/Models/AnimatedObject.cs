using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using InvasionOfAldebaran.Shared;

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

		public abstract void Draw(Canvas canvas);

		public abstract void Move(Direction direction);

		public void Animate(TimeSpan interval, Canvas canvas)
		{
			X += Vx * interval.TotalSeconds;
			Y += Vy * interval.TotalSeconds;

			if (X < 0.0)
			{
				X = canvas.ActualWidth;
			}
			else if (X > canvas.ActualWidth)
			{
				X = 0;
			}

			if (Y < 0.0)
			{
				Y = canvas.ActualHeight;
			}
			else if (Y > canvas.ActualHeight)
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
