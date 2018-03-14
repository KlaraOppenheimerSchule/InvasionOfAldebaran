using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InvasionOfAldebaran.Models
{
    public class Missile : AnimatedObject
    {
        public Missile(Coords player, double vx, double vy) : base(player, vx, vy)
        {
            Frame.Points.Add(new Point(-2.5, -5.5));
            Frame.Points.Add(new Point(2.5, -5.5));
            Frame.Points.Add(new Point(2.5, 3.5));
            Frame.Points.Add(new Point(-2.5, 3.5));

            this.Frame.Fill = Brushes.OrangeRed;

            this.Vy = -2000;
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Children.Add(Frame);
            Canvas.SetLeft(Frame, this.Coords.X);
            Canvas.SetTop(Frame, this.Coords.Y);
        }
    }
}
