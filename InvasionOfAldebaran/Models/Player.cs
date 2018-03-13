using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using InvasionOfAldebaran.Shared;

namespace InvasionOfAldebaran.Models
{
	public class Player: AnimatedObject
	{
        private double _speed = 600;

        private void ResetSpeed()
        {
            this.Vx = 0;
            this.Vy = 0;
        }

        public Player(Coords coords, double vx, double vy) : base(coords, vx, vy)
		{
            Frame.Points.Add(new Point(0.0, -20.0));
            Frame.Points.Add(new Point(10.0, 14.0));
			Frame.Points.Add(new Point(-10.0, 14.0));

			this.Frame.Fill = Brushes.Blue;
		}

		public override void Draw(Canvas canvas)
		{
			canvas.Children.Add(Frame);
			Canvas.SetLeft(Frame, this.Coords.X);
			Canvas.SetTop(Frame, this.Coords.Y);
            this.ResetSpeed();
		}

        public override void Move(Direction direction)
		{
			switch (direction)
			{
				case Direction.Left:
                    this.Vx = -_speed;
					break;

				case Direction.Right:
                    this.Vx = _speed;
					break;
			}
		}

        public override void Animate(TimeSpan interval, Canvas canvas)
        {
            this.Coords.X += Vx * interval.TotalSeconds;
            this.Coords.Y += Vy * interval.TotalSeconds;

            if (this.Coords.X > canvas.ActualWidth)
            {
                this.Coords.X = canvas.ActualWidth;
            }
            else if (this.Coords.X < 0)
            {
                this.Coords.X = 0;
            }

            if (this.Coords.Y > canvas.ActualHeight)
            {
                this.Coords.Y = canvas.ActualHeight;
            }
            else if (this.Coords.Y < 0)
            {
                this.Coords.Y = 0;
            }
        }
    }
}
