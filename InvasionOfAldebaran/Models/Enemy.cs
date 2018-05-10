using InvasionOfAldebaran.Helper;
using System.Windows.Controls;

namespace InvasionOfAldebaran.Models
{
    public class Enemy : AnimatedObject
    {
		public static double CanvasHeight;
		private const double _slowSpeedFactor = 0.05555;
		private const double _mediumSpeedFactor = 0.09999;
		private const double _fastSpeedFactor = 0.17777;

		private readonly double _maxDeviation;
        private readonly double _initialPosX;
        private bool _movingLeft;

		private double _slowSpeed = _slowSpeedFactor * CanvasHeight;
		private double _mediumSpeed = _mediumSpeedFactor * CanvasHeight;
		private double _fastSpeed = _fastSpeedFactor * CanvasHeight;

		public bool MovesSideways { get; private set; }
        public string AlienName { get; private set; }

        public Enemy(string alien, string imagePath, Coords coords, Speed speed, double moveAmount) : base(imagePath, coords)
        {
            this.AlienName = alien;
	        _maxDeviation = moveAmount;

			switch (speed)
            {
                case Speed.Fast:
                    this.Vy = _slowSpeed;
                    break;

                case Speed.Medium:
                    this.Vy = _mediumSpeed;
                    break;

                case Speed.Slow:
                    this.Vy = _fastSpeed;
                    break;
            }
            this.MovesSideways = RandomBool.Get();
            _movingLeft = RandomBool.Get();
            _initialPosX = coords.X;
        }

        public override void Draw(Canvas canvas)
        {
            if (this.MovesSideways)
            {
                this.MoveSideways(canvas);
            }
            canvas.Children.Add(this.Image);
            Canvas.SetLeft(this.Image, this.Coords.X);
            Canvas.SetTop(this.Image, this.Coords.Y);
        }

        private void MoveSideways(Canvas canvas)
        {
            var pos = this.Coords.X - _initialPosX;

            if (_movingLeft)
            {
                this.Vx = -this.Vy;
                if (pos <= -_maxDeviation || this.Coords.X <= this.Image.ActualWidth / 2)
                    _movingLeft = false;
            }
            else
            {
                this.Vx = this.Vy;
                if (pos >= _maxDeviation || this.Coords.X >= canvas.ActualWidth - this.Image.ActualWidth)
                    _movingLeft = true;
            }
        }
    }
}