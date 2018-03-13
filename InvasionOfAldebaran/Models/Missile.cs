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
    class Missile : AnimatedObject
    {
        public Missile(Coords player, double vx, double vy) : base(player, vx, vy)
        {
            Frame.Points.Add(new Point(-5.0, -10.0));
            Frame.Points.Add(new Point(5.0, -10.0));
            Frame.Points.Add(new Point(5.0, 7.0));
            Frame.Points.Add(new Point(-5.0, 7.0));

            this.Frame.Fill = Brushes.Green;

            this.Vy = -300;
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Children.Add(Frame);
            Canvas.SetLeft(Frame, this.Coords.X);
            Canvas.SetTop(Frame, this.Coords.Y);
        }

        public override void Move(Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}
