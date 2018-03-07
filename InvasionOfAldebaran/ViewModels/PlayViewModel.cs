using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Caliburn.Micro;
using InvasionOfAldebaran.Models;
using InvasionOfAldebaran.Shared;

namespace InvasionOfAldebaran.ViewModels
{
    public class PlayViewModel : Screen
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        public List<AnimatedObject> Objects { get; set; }

        public Player Player;

        public Canvas Canvas { get; set; }

        public TextBlock TextField { get; set; }

        public DateTime lastMissile { get; set; }

        public PlayViewModel()
        {
            Objects = new List<AnimatedObject>();
            Canvas = new Canvas()
            {
                Height = 800,
                Width = 500,
                Focusable = true,
                Background = Brushes.DarkGray
            };
            _timer.Interval = TimeSpan.FromSeconds(0.01);

            _timer.Tick += AnimateObjects;
            this.Canvas.PreviewKeyDown += this.WindowKeyDown;

            this.Player = new Player(Canvas, 250, 700, 0, 0);
            this.Objects.Add(Player);

            this.Objects.Add(new Enemy(Canvas, 250, 100, 0, 0));

            _timer.Start();
        }

        void AnimateObjects(object sender, EventArgs e)
        {
            if (!this.Canvas.IsFocused)
                this.Canvas.Focus();

            foreach (var item in Objects)
            {
                item.Animate(_timer.Interval, Canvas);
            }
            List<AnimatedObject> ObjectsToBeDeleted = new List<AnimatedObject>();

            foreach (var missile in Objects.OfType<Missile>())
            {
                foreach (var enemy in Objects.OfType<Enemy>())
                {
                    if (enemy.ContainsPoint(missile.X, missile.Y))
                    {
                        ObjectsToBeDeleted.Add(enemy);
                        ObjectsToBeDeleted.Add(missile);
                    }
                }
            }
            foreach (var obj in ObjectsToBeDeleted)
            {
                this.Objects.Remove(obj);
            }

            this.Canvas.Children.Clear();
            foreach (var item in Objects)
            {
                item.Draw(this.Canvas);
            }
        }

        private void EndGame(string text)
        {
            TextField.Text = text;
            TextField.Visibility = Visibility.Visible;
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (Objects.Contains(this.Player))
            {
                switch (e.Key)
                {
                    default:
                        break;
                    case Key.A:
                    case Key.Left:
                        this.Player.Move(Direction.Left);
                        break;
                    case Key.D:
                    case Key.Right:
                        this.Player.Move(Direction.Right);
                        break;
                    case Key.Space:
                        if (lastMissile.AddSeconds(0.5) <= DateTime.Now)
                        {
                            Objects.Add(new Missile(Canvas, Player, 0, 0));
                            lastMissile = DateTime.Now;
                        }
                        break;
                }
            }
        }
    }
}
