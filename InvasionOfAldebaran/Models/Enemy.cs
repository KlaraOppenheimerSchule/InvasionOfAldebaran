using InvasionOfAldebaran.Helper;
using System.Windows.Controls;

namespace InvasionOfAldebaran.Models
{
    public class Enemy : AnimatedObject
    {
        private readonly double _maxDeviation;
        private readonly double _initialPosX;
        private bool _movingLeft;

        private const double slowSpeed = 60;
        private const double mediumSpeed = 140;
        private const double fastSpeed = 220;

        public bool MovesSideways { get; private set; }
        public string AlienName { get; private set; }

        public Enemy(string alien, string imagePath, Coords coords, Speed speed, bool movesSideways) : base(imagePath, coords)
        {
            AlienName = alien;

            switch (speed)
            {
                case Speed.Fast:
                    this.Vy = fastSpeed;
                    break;

                case Speed.Medium:
                    this.Vy = mediumSpeed;
                    break;

                case Speed.Slow:
                    this.Vy = slowSpeed;
                    break;
            }
            this.MovesSideways = movesSideways;
            _movingLeft = RandomBool.Get();
            _maxDeviation = 48;
            _initialPosX = coords.X;
        }

        public override void Draw(Canvas canvas)
        {
            if (this.MovesSideways)
            {
                this.MoveSideways();
            }
            canvas.Children.Add(this.Image);
            Canvas.SetLeft(this.Image, this.Coords.X);
            Canvas.SetTop(this.Image, this.Coords.Y);
        }

        private void MoveSideways()
        {
            var pos = this.Coords.X - _initialPosX;

            if (_movingLeft)
            {
                this.Vx = -this.Vy;
                if (pos <= -_maxDeviation)
                    _movingLeft = false;
            }
            else
            {
                this.Vx = this.Vy;
                if (pos >= _maxDeviation)
                    _movingLeft = true;
            }
        }
    }
}