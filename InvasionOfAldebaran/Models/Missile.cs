using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Caliburn.Micro;
using InvasionOfAldebaran.Helper;

namespace InvasionOfAldebaran.Models
{
    public class Missile : AnimatedObject
    {
	    private const double speed = -2000;

        public Missile(Brush color, Coords player) : base(color, player)
        {
	        this.Frame.Points.Add(new Point(-2.5, -5.5));
	        this.Frame.Points.Add(new Point(2.5, -5.5));
	        this.Frame.Points.Add(new Point(2.5, 3.5));
	        this.Frame.Points.Add(new Point(-2.5, 3.5));

	        this.Vy = speed;
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Children.Add(this.Frame);
            Canvas.SetLeft(this.Frame, this.Coords.X);
            Canvas.SetTop(this.Frame, this.Coords.Y);
        }
    }
}
