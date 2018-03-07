using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using InvasionOfAldebaran.Shared;

namespace InvasionOfAldebaran.Models
{
    public class Enemy : AnimatedObject
    {
	    public Enemy(Canvas canvas, double x, double y, double vx, double vy) : base(x, y, vx, vy)
	    {
            Frame.Points.Add(new Point(-10.0, -20.0));
            Frame.Points.Add(new Point(10.0, -20.0));
            Frame.Points.Add(new Point(10.0, 14.0));
            Frame.Points.Add(new Point(-10.0, 14.0));

            this.Frame.Fill = Brushes.Red;

            Vy = 50;
        }

		public override void Draw(Canvas canvas)
		{
            canvas.Children.Add(Frame);
            Canvas.SetLeft(Frame, X);
            Canvas.SetTop(Frame, Y);
        }

        public override void Move(Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}
