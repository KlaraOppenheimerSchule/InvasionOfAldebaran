using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using InvasionOfAldebaran.Helper;

namespace InvasionOfAldebaran.Models
{
	public abstract class AnimatedObject
	{
		private Brush _color;

		public Coords Coords { get; protected set; }
		public double Vx { get; protected set; }
		public double Vy { get; protected set; }
        public bool ReachedEnd { get; protected set; }
		public Polygon Frame { get; protected set; }
		public Brush Color
		{
			get { return _color; }
			protected set
			{
				_color = value ?? Brushes.AntiqueWhite;
				this.Frame.Fill = _color;
			}
		}

		protected AnimatedObject(Brush color, Coords coords)
		{
			this.Frame = new Polygon();
			this.Color = color;
			this.Coords = coords;
		}

		public abstract void Draw(Canvas canvas);

		public virtual void Animate(TimeSpan interval, Canvas canvas)
		{
			this.Coords.X += this.Vx * interval.TotalSeconds;
			this.Coords.Y += this.Vy * interval.TotalSeconds;

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
			return this.Frame.RenderedGeometry.FillContains(new Point(x - this.Coords.X, y - this.Coords.Y));
		}
	}
}
