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
        public Missile(Canvas canvas, Player player, double vx, double vy) : base(player.X, player.Y, vx, vy)
        {
            Frame.Points.Add(new Point(-5.0, -10.0));
            Frame.Points.Add(new Point(5.0, -10.0));
            Frame.Points.Add(new Point(5.0, 7.0));
            Frame.Points.Add(new Point(-5.0, 7.0));

            this.Frame.Fill = Brushes.Green;

            Vy = -100;
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
