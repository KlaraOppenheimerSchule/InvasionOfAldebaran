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
		public Coords Coords { get; protected set; }
		public double Vx { get; protected set; }
		public double Vy { get; protected set; }

        public bool ReachedEnd { get; protected set; }
        

		protected static Random Random = new Random();

		public Polygon Frame = new Polygon();

		protected AnimatedObject(Coords coords, double vx, double vy)
		{
            this.Coords = coords;
			Vx = vx;
			Vy = vy;
		}

		public abstract void Draw(Canvas canvas);

		public virtual void Animate(TimeSpan interval, Canvas canvas)
		{
			this.Coords.X += Vx * interval.TotalSeconds;
			this.Coords.Y += Vy * interval.TotalSeconds;

			if (this.Coords.X < 0)
			{
                this.Coords.X = canvas.ActualWidth;
			}
			else if (this.Coords.X > canvas.ActualWidth)
			{
                this.Coords.X = 0;
			}

			if (this.Coords.Y < 0)
			{
                this.Coords.Y = canvas.ActualHeight;
                this.ReachedEnd = true;
			}
			else if (this.Coords.Y > canvas.ActualHeight)
			{
                this.Coords.Y = 0;
                this.ReachedEnd = true;
			}
		}

		public bool ContainsPoint(double x, double y)
		{
			return Frame.RenderedGeometry.FillContains(new Point(x - this.Coords.X, y - this.Coords.Y));
		}
	}
}
