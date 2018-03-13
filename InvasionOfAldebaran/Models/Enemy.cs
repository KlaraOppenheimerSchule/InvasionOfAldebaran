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
        private Brush _color;

        public Brush Color
        {
            get
            {
                return this._color;
            }

            private set
            {
                this._color = value;
                this.Frame.Fill = value;
            }
        }

        public Enemy(Brush color, Coords coords, double vx, double vy) : base(coords, vx, vy)
	    {
            Frame.Points.Add(new Point(-10.0, -20.0));
            Frame.Points.Add(new Point(10.0, -20.0));
            Frame.Points.Add(new Point(10.0, 14.0));
            Frame.Points.Add(new Point(-10.0, 14.0));

            this.Color = color;


            Vy = 200;
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
