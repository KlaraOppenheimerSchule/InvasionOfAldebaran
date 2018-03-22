using InvasionOfAldebaran.Helper;
using System;
using System.Windows.Controls;

namespace InvasionOfAldebaran.Models
{
    public class Player : AnimatedObject
    {
        private const double speed = 900;

        public Player(string imagePath, Coords coords) : base(imagePath, coords)
        {
 
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Children.Add(this.Image);
            Canvas.SetLeft(this.Image, this.Coords.X);
            Canvas.SetTop(this.Image, this.Coords.Y);
            this.ResetSpeed();
        }

        public override void Animate(TimeSpan interval, Canvas canvas)
        {
            this.Coords.X += this.Vx * interval.TotalSeconds;
            this.Coords.Y += this.Vy * interval.TotalSeconds;

            if ((this.Coords.X + (this.Image.ActualWidth)) > canvas.ActualWidth)
            {
                this.Coords.X = canvas.ActualWidth - (this.Image.ActualWidth);
            }
            else if ((this.Coords.X) < 0)
            {
                this.Coords.X = 0;
            }

            if (this.Coords.Y > canvas.ActualHeight)
            {
                this.Coords.Y = canvas.ActualHeight;
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
                    this.Vx = -speed;
                    break;

                case Direction.Right:
                    this.Vx = speed;
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