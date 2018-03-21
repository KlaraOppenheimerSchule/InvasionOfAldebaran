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

        public Missile(string imagePath, Coords player) : base( imagePath, player)
        {
	        //this.Image.Points.Add(new Point(-2.5, -5.5));
	        //this.Image.Points.Add(new Point(2.5, -5.5));
	        //this.Image.Points.Add(new Point(2.5, 3.5));
	        //this.Image.Points.Add(new Point(-2.5, 3.5));

	        this.Vy = speed;
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Children.Add(this.Image);
            Canvas.SetLeft(this.Image, this.Coords.X);
            Canvas.SetTop(this.Image, this.Coords.Y);
        }
    }
}
