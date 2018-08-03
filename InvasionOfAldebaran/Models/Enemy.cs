using InvasionOfAldebaran.Helper;
using System.Windows.Controls;

namespace InvasionOfAldebaran.Models
{
    public class Enemy : AnimatedObject
    {
		private const double _slowSpeedFactor = 0.1;
		private const double _mediumSpeedFactor = 0.15;
		private const double _fastSpeedFactor = 0.2;

		private readonly double _maxDeviation;
        private readonly double _initialPosX;
        private bool _movingLeft;

		private double _slowSpeed = _slowSpeedFactor * CanvasHeight;
		private double _mediumSpeed = _mediumSpeedFactor * CanvasHeight;
		private double _fastSpeed = _fastSpeedFactor * CanvasHeight;
		
		/// <summary>
		/// Indicates whether the alien is moving sideways or not
		/// </summary>
		public bool MovesSideways { get; private set; }
		/// <summary>
		/// The file name of the associated alien picture under the resources folder
		/// </summary>
        public string AlienName { get; private set; }

        public Enemy(string alien, string imagePath, Coords coords, Speed speed, double speedMulti, double moveAmount) : base(imagePath, coords)
        {
            this.AlienName = alien;
	        _maxDeviation = moveAmount;

			switch (speed)
            {
                case Speed.Fast:
                    this.Vy = _slowSpeed * speedMulti;
                    break;

                case Speed.Medium:
                    this.Vy = _mediumSpeed * speedMulti;
                    break;

                case Speed.Slow:
                    this.Vy = _fastSpeed * speedMulti;
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