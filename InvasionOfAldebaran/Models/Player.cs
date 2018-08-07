using InvasionOfAldebaran.Helper;
using System;
using System.Windows.Controls;

namespace InvasionOfAldebaran.Models
{
    public class Player : AnimatedObject
    {
		private const double speedFactor = 3.2;

		private double _speed;	

        public Player(string imagePath, Coords coords) : base(imagePath, coords)
        {
			this._speed = speedFactor * CanvasHeight;
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Children.Add(this.Image);
            Canvas.SetLeft(this.Image, this.Coords.X);
            Canvas.SetTop(this.Image, this.Coords.Y);
			this.Vx = 0;
			this.Vy = 0;
		}

        public override void Animate(float interval, Canvas canvas)
        {
            this.Coords.X += this.Vx * interval;
            this.Coords.Y += this.Vy * interval;

            if ((this.Coords.X + (this.Image.ActualWidth)) > canvas.Width)
            {
                this.Coords.X = canvas.Width - (this.Image.ActualWidth) -3;
            }
            else if ((this.Coords.X) < 0)
            {
                this.Coords.X = 3;
            }

            if (this.Coords.Y > canvas.Height)
            {
                this.Coords.Y = canvas.Height;
            }
            else if (this.Coords.Y < 0)
            {
                this.Coords.Y = 0;
            }
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    this.Vx = -_speed;
                    break;

                case Direction.Right:
                    this.Vx = _speed;
                    break;

                default:
                    this.Vx = 0;
                    break;
            }
        }

        private void ResetSpeed()
        {
            this.Vx = 0;
            this.Vy = 0;
        }
    }
}