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

namespace InvasionOfAldebaran.Models
{
	public class Player: AnimatedObject
	{

		public Player(Canvas canvas, double x, double y, double vx, double vy) : base(x, y, vx, vy)
		{
			Frame.Points.Add(new Point(0.0, -10.0));
			Frame.Points.Add(new Point(5.0, 7.0));
			Frame.Points.Add(new Point(-5.0, 7.0));

			this.Frame.Fill = Brushes.Blue;
		}

		public override void Draw(Canvas canvas)
		{
			canvas.Children.Add(Frame);
			Canvas.SetLeft(Frame, X);
			Canvas.SetTop(Frame, Y);
		}
	}
}
