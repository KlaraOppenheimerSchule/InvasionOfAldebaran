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
        private double _speed = 400;

        private void ResetSpeed()
        {
            this.Vx = 0;
            this.Vy = 0;
        }

        public Player(Canvas canvas, double x, double y, double vx, double vy) : base(x, y, vx, vy)
		{
            Frame.Points.Add(new Point(0.0, -20.0));
            Frame.Points.Add(new Point(10.0, 14.0));
			Frame.Points.Add(new Point(-10.0, 14.0));

			this.Frame.Fill = Brushes.Blue;
		}

		public override void Draw(Canvas canvas)
		{
			canvas.Children.Add(Frame);
			Canvas.SetLeft(Frame, X);
			Canvas.SetTop(Frame, Y);
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
	}
}
