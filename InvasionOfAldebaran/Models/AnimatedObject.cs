using InvasionOfAldebaran.Helper;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InvasionOfAldebaran.Models
{
    public abstract class AnimatedObject
    {
        //private Brush _color;

        public Coords Coords { get; protected set; }
        public double Vx { get; protected set; }
        public double Vy { get; protected set; }
        public bool ReachedEnd { get; protected set; }
        public Image Image { get; protected set; }
        public Uri ImagePath { get; protected set; }

        protected AnimatedObject(string ImagePath, Coords coords)
        {
            var imageBitmap = new BitmapImage(new Uri(ImagePath, UriKind.Relative));
            this.Image = new Image();
            this.Image.Source = imageBitmap;
            this.Image.Width = imageBitmap.Width;
            this.Image.Height = imageBitmap.Height;
            this.Coords = coords;
        }

        public abstract void Draw(Canvas canvas);

        public virtual void Animate(TimeSpan interval, Canvas canvas)
        {
            this.Coords.X += this.Vx * interval.TotalSeconds;
            this.Coords.Y += this.Vy * interval.TotalSeconds;

            if (this.Coords.X < 0)
            {
                this.Coords.X = canvas.ActualWidth;
            }
            else if (this.Coords.X > canvas.ActualWidth)
            {
                this.Coords.X = 0;
            }

            if ((this.Coords.Y + this.Image.ActualHeight) < 0)
            {
                this.Coords.Y = canvas.ActualHeight;
                this.ReachedEnd = true;
            }
            else if ((this.Coords.Y + this.Image.ActualHeight) > canvas.ActualHeight)
            {
                this.Coords.Y = 0;
                this.ReachedEnd = true;
            }
        }

        public bool IntersectsWith(double x, double y, Image alien, Image missile)
        {
            var x1 = Canvas.GetLeft(alien);
            var y1 = Canvas.GetTop(alien);
            Rect r1 = new Rect(x1, y1, alien.ActualWidth, alien.ActualHeight);

            var x2 = Canvas.GetLeft(missile);
            var y2 = Canvas.GetTop(missile);
            Rect r2 = new Rect(x2, y2, missile.ActualWidth, missile.ActualHeight);

            return r1.IntersectsWith(r2);
        }
    }
}